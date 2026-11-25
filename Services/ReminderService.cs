#if ANDROID
using Android.Content; using Android.OS; using Android.Provider; using ReminderIntervalApp.Platforms.Android;
#endif
namespace ReminderIntervalApp.Services;
public class ReminderService {
 public async Task<bool> EnsurePermissionsAsync(){
 #if ANDROID
  if(Build.VERSION.SdkInt>=BuildVersionCodes.Tiramisu){var p=await Permissions.RequestAsync<Permissions.PostNotifications>();if(p!=PermissionStatus.Granted)return false;}
  if(Build.VERSION.SdkInt>=BuildVersionCodes.S){var am=(Android.App.AlarmManager?)Android.App.Application.Context.GetSystemService(Context.AlarmService);if(am?.CanScheduleExactAlarms()!=true){var i=new Intent(Settings.ActionRequestScheduleExactAlarm);i.SetData(Android.Net.Uri.Parse("package:"+Android.App.Application.Context.PackageName));i.AddFlags(ActivityFlags.NewTask);Android.App.Application.Context.StartActivity(i);return false;}}
 #endif
  return true;
 }
 public void Schedule(int mins,DateTime at){
 #if ANDROID
  ReminderAlarm.Schedule(Android.App.Application.Context,new DateTimeOffset(at));
 #endif
 }
 public void Cancel(){
 #if ANDROID
  ReminderAlarm.Cancel(Android.App.Application.Context);
 #endif
 }
}