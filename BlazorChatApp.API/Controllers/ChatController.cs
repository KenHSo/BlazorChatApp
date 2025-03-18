using BlazorChatApp.Shared.Models; // Use the shared model
using Microsoft.AspNetCore.Mvc;

namespace BlazorChatApp.API.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        private static List<ChatMessage> messages = new List<ChatMessage>();

        [HttpGet]
        public IEnumerable<ChatMessage> GetMessages()
        {
            return messages.OrderBy(m => m.Timestamp);
        }

        [HttpPost]
        public IActionResult PostMessage([FromBody] ChatMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.User) || string.IsNullOrWhiteSpace(message.Message))
            {
                return BadRequest("User and message cannot be empty.");
            }

            message.Timestamp = DateTime.UtcNow;
            messages.Add(message);
            return Ok();
        }
    }
}
