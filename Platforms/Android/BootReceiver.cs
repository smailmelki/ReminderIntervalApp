using Android.App;
using Android.Content;

namespace ReminderIntervalApp.Platforms.Android;

[BroadcastReceiver(Enabled = true, Exported = true)]
[Android.App.IntentFilter(new[] { Intent.ActionBootCompleted })]
public class BootReceiver : BroadcastReceiver
{
    public override void OnReceive(Context? context, Intent? intent)
    {
        if (context is null)
            return;

        var preferences = context.GetSharedPreferences("Microsoft.Maui.Essentials", FileCreationMode.Private);
        if (preferences?.GetBoolean("reminder_active", false) != true)
            return;

        int minutes = Math.Max(1, preferences.GetInt("interval_minutes", 15));
        long ticks = preferences.GetLong("next_alarm_ticks", 0);

        var next = ticks > 0 ? new DateTime(ticks) : DateTime.Now.AddMinutes(minutes);
        if (next <= DateTime.Now)
            next = DateTime.Now.AddMinutes(minutes);

        ReminderAlarm.Schedule(context, new DateTimeOffset(next));
    }
}
