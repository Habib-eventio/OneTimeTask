using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service.Ollama
{

		public sealed record OllamaOptions
		{
			public string BaseUrl { get; init; } = "http://localhost:11434";
			public string Model { get; init; } = "llama3";
			public string SystemPrompt { get; init; } = "You are a helpful assistant.";
		}
}

