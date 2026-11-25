using Android.App; using Android.Content; using Android.OS;
namespace ReminderIntervalApp.Platforms.Android;
internal static class ReminderAlarm {
 const int Id=7731;
 public static void Schedule(Context c,DateTimeOffset when){var am=(AlarmManager?)c.GetSystemService(Context.AlarmService);if(am==null)return;var i=new Intent(c,typeof(AlarmReceiver));var pi=PendingIntent.GetBroadcast(c,Id,i,PendingIntentFlags.UpdateCurrent|PendingIntentFlags.Immutable);if(Build.VERSION.SdkInt>=BuildVersionCodes.M)am.SetExactAndAllowWhileIdle(AlarmType.RtcWakeup,when.ToUnixTimeMilliseconds(),pi);else am.SetExact(AlarmType.RtcWakeup,when.ToUnixTimeMilliseconds(),pi);}
 public static void Cancel(Context c){var am=(AlarmManager?)c.GetSystemService(Context.AlarmService);var i=new Intent(c,typeof(AlarmReceiver));var pi=PendingIntent.GetBroadcast(c,Id,i,PendingIntentFlags.UpdateCurrent|PendingIntentFlags.Immutable);am?.Cancel(pi);pi.Cancel();}
}