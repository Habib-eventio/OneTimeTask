using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamcoTasks.Service.Service.Ollama
{
		public record OllamaChatMessage(string role, string content);
		public record OllamaChatRequest(string model, /*string system,*/ List<OllamaChatMessage> messages);
	    public record OllamaChatResponse(string id,string model,DateTime created_at,OllamaChatMessage message,bool done);
}
