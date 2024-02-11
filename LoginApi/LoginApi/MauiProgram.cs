//using Microsoft.Maui;
//using Microsoft.Maui.Controls.Hosting;
//using Microsoft.Maui.Hosting;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Components.WebView.Maui;
//using LoginApi.Services; // Add the namespace for your authentication service
//using Microsoft.AspNetCore.Authentication;

//namespace LoginApi
//{
//    public class MauiProgram
//    {
//        public static MauiApp CreateMauiApp()
//        {
//            var builder = MauiApp.CreateBuilder();

//            builder
//                .ConfigureMauiHandlers((handlers) =>
//                {
//                    // Configure handlers if needed
//                })
//                .ConfigureFonts(fonts =>
//                {
//                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//                });

//            builder.Services.AddMauiControlsHostingLayer();
//            builder.Services.AddBlazorWebView();

//            // Add authentication service (replace AuthenticationService with your actual service)
//            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

//            // Register your main page or initial page here
//            builder.Services.AddSingleton<MainPage>();

//            return builder.Build();
//        }
//    }
//}
