using Android.Content;
namespace ReminderIntervalApp.Platforms.Android;
[BroadcastReceiver(Enabled=true,Exported=true)]
[IntentFilter(new[]{Intent.ActionBootCompleted})]
public class BootReceiver:BroadcastReceiver {
 public override void OnReceive(Context? c,Intent? i){if(c==null)return;var p=c.GetSharedPreferences("Microsoft.Maui.Essentials",FileCreationMode.Private);if(p?.GetBoolean("reminder_active",false)!=true)return;int m=Math.Max(1,p.GetInt("interval_minutes",15));long ticks=p.GetLong("next_alarm_ticks",0);var next=ticks>0?new DateTime(ticks):DateTime.Now.AddMinutes(m);if(next<=DateTime.Now)next=DateTime.Now.AddMinutes(m);ReminderAlarm.Schedule(c,new DateTimeOffset(next));}
}