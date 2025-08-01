using CamcoTasks.Service.IService;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service.Ollama
{
	public sealed class OllamaBotService:IBotService
	{
		private readonly OllamaOptions _options;
		private readonly HttpClient _httpClient;
		private readonly List<OllamaChatMessage> ollamaChatMessages = new();
		private string botResponse;
		public OllamaBotService(HttpClient httpClient, IOptions<OllamaOptions> options)
		{
			
			_options = options.Value ?? throw new ArgumentNullException(nameof(options), "OllamaOptions cannot be null.");
			_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient), "HttpClient cannot be null.");
			_httpClient.BaseAddress = new Uri(_options.BaseUrl);
			ollamaChatMessages.Add(new OllamaChatMessage("system", _options.SystemPrompt));
		}
		public async Task<string> SendMessageAsync(string userMessage, CancellationToken cancellationToken = default)
		{
			if (string.IsNullOrWhiteSpace(userMessage))
			{
				throw new ArgumentException("User message cannot be null or empty.", nameof(userMessage));
			}
			ollamaChatMessages.Add(new OllamaChatMessage("user", userMessage));

			var request = new OllamaChatRequest(_options.Model, ollamaChatMessages);
			var response = await _httpClient.PostAsJsonAsync("/api/chat", request, cancellationToken);

			if (!response.IsSuccessStatusCode)
			{
				var errorContent = await response.Content.ReadAsStringAsync();
				throw new HttpRequestException($"Request failed with status {response.StatusCode}: {errorContent}");
			}
			botResponse = await ProcessStreamingResponse(response, cancellationToken);
			ollamaChatMessages.Add(new OllamaChatMessage("assistant", botResponse));
			
			return botResponse;
		}
		private async Task<string> ProcessStreamingResponse(HttpResponseMessage response, CancellationToken cancellationToken)
		{
			var content = response.Content;
			using var stream = await content.ReadAsStreamAsync(cancellationToken);
			using var reader = new StreamReader(stream);
			string? line;
			var fullResponse = new StringBuilder();

			while ((line = await reader.ReadLineAsync()) != null)
			{
				cancellationToken.ThrowIfCancellationRequested();
				if (string.IsNullOrWhiteSpace(line)) continue;
				var chatResponse = System.Text.Json.JsonSerializer.Deserialize<OllamaChatResponse>(line);
				if (chatResponse?.message?.content != null)
				{
					fullResponse.Append(chatResponse.message.content);
				}
				if (chatResponse?.done == true)
				{
					break;
				}
			}

			return fullResponse.ToString().Trim();
		}
	}
}

