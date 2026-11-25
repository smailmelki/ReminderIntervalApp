using ReminderIntervalApp.Services;
namespace ReminderIntervalApp;
public static class MauiProgram {
 public static MauiApp CreateMauiApp() {
  var builder=MauiApp.CreateBuilder();
  builder.UseMauiApp<App>();
  builder.Services.AddSingleton<ReminderService>();
  return builder.Build();
 }
}