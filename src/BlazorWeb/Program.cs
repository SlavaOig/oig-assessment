using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorWeb;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using BlazorWeb.Sevices;
using BlazorWeb.Shared.AuthHelpers;
using System.Text.Json;
using static BlazorWeb.Shared.Models.CreateSurveyDto;
using FluentValidation;
using static BlazorWeb.Shared.Models.Survey.UpdateSurveyDetailsDto;
using static BlazorWeb.Shared.Models.Survey.PublicSurveyQuestionsDto;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:5001/")
});


builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddTransient<AuthorizationMessageHandler>();
builder.Services.AddHttpClient("AuthorizedClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:5001/");
})
.AddHttpMessageHandler<AuthorizationMessageHandler>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<SurveyService>();

builder.Services.AddScoped<JWTService>();

builder.Services.Configure<JsonSerializerOptions>(options =>
{
    options.Converters.Add(new UserIdJsonConverter());
});


builder.Services.AddBlazorBootstrap();

builder.Services.AddValidatorsFromAssemblyContaining<SurveyValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateSurveyDetailsDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<SurveyAnswersValidator>();



await builder.Build().RunAsync();
