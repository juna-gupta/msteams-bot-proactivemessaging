using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Teams.Samples.ProactiveMessaging;
using Microsoft.Teams.Samples.ProactiveMessaging.Bot;

var builder = WebApplication.CreateBuilder(args);

// App Settings.
var appSettings = new AppSettings();
appSettings.ServiceUrl = builder.Configuration.GetValue<string>("ServuceUrl");
appSettings.MicrosoftAppId = builder.Configuration.GetValue<string>("MicrosoftAppid");
appSettings.MicrosoftAppSecret = builder.Configuration.GetValue<string>("MicrosoftAppPassword");
builder.Services.AddSingleton(appSettings);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create the Bot Framework Authentication to be used with the Bot Adapter.
builder.Services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();
builder.Services.AddSingleton<IBot, BotActivityHandler>();
builder.Services.AddSingleton<IBotFrameworkHttpAdapter, BotHttpAdapter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



