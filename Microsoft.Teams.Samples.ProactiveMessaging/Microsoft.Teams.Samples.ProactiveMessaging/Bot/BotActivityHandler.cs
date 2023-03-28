// <copyright file="BotActivityHandler.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Microsoft.Teams.Samples.ProactiveMessaging.Bot
{
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Teams;
    using Microsoft.Bot.Schema;
    using Microsoft.Bot.Schema.Teams;
    using System.Collections.Generic;

    /// <summary>
    /// Teams Bot Activity Handler.
    /// </summary>
    public class BotActivityHandler : TeamsActivityHandler
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="BotActivityHandler"/> class.
        /// </summary>
        /// <param name="cardFactory">Card factory.</param>
        public BotActivityHandler()
        {
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            // Echo back
            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);
        }

        protected override Task OnTeamsMembersAddedAsync(IList<TeamsChannelAccount> teamsMembersAdded, TeamInfo teamInfo, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            return base.OnTeamsMembersAddedAsync(teamsMembersAdded, teamInfo, turnContext, cancellationToken);
        }

        protected override Task OnConversationUpdateActivityAsync(ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            return base.OnConversationUpdateActivityAsync(turnContext, cancellationToken);
        }
    }
}
