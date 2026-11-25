using Android.App; using Android.Content; using Android.Media; using Android.OS; using AndroidX.Core.App;
namespace ReminderIntervalApp.Platforms.Android;
[BroadcastReceiver(Enabled=true,Exported=false)]
public class AlarmReceiver:BroadcastReceiver {
 public override void OnReceive(Context? c,Intent? i){if(c==null)return;var nm=NotificationManagerCompat.From(c);if(Build.VERSION.SdkInt>=BuildVersionCodes.O){var channel=new NotificationChannel("interval_reminders","تذكيرات متكررة",NotificationImportance.High);((NotificationManager)c.GetSystemService(Context.NotificationService)!).CreateNotificationChannel(channel);}
 var sound=RingtoneManager.GetDefaultUri(RingtoneType.Notification);var n=new NotificationCompat.Builder(c,"interval_reminders").SetSmallIcon(Android.Resource.Drawable.IcLockIdleAlarm).SetContentTitle("حان وقت التذكير").SetContentText("هذا هو موعد التذكير الذي ضبطته.").SetPriority((int)NotificationPriority.High).SetAutoCancel(true).SetSound(sound).Build();nm.Notify(4101,n);
 var prefs=c.GetSharedPreferences("Microsoft.Maui.Essentials",FileCreationMode.Private);if(prefs?.GetBoolean("reminder_active",false)==true){int m=Math.Max(1,prefs.GetInt("interval_minutes",15));var next=DateTime.Now.AddMinutes(m);prefs.Edit()?.PutLong("next_alarm_ticks",next.Ticks)?.Apply();ReminderAlarm.Schedule(c,new DateTimeOffset(next));}
 }
}