using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Microsoft.AspNetCore.Authorization;

namespace Microsoft.Teams.Samples.ProactiveMessaging.Controllers
{
    [Route("/api/sendmessage")]
    [ApiController]
    [AllowAnonymous]
    public class MessagingController : ControllerBase
    {
        private readonly AppSettings appSettings;
        private readonly IBotFrameworkHttpAdapter adapter;

        public class Input
        {
            /// <summary>
            /// 1:1 / Group conversation id.
            /// </summary>
            public string? chatId { get; set; }

            /// <summary>
            /// Teams channel id.
            /// </summary>
            public string? channelId { get; set; }

            /// <summary>
            /// Text message.
            /// </summary>
            public string? message { get; set; }
        }

        public MessagingController(AppSettings appSettings, IBotFrameworkHttpAdapter adapter)
        {
            this.appSettings = appSettings;
            this.adapter = adapter;
        }

        [HttpPost]
        public async Task PostAsync([FromBody] Input input)
        {
            var conversationRef = new ConversationReference
            {
                ServiceUrl = appSettings.ServiceUrl,
                Conversation = new ConversationAccount
                {
                    Id = string.IsNullOrEmpty(input.chatId) ? input.channelId : input.chatId,
                }
            };

            await ((CloudAdapter)this.adapter).ContinueConversationAsync(appSettings.MicrosoftAppId, conversationRef, 
                callback: async (turnContext, cancellationtoken) => {
                    await turnContext.SendActivityAsync(MessageFactory.Text(input.message), cancellationtoken);
            }, CancellationToken.None);
        }
    }
}
