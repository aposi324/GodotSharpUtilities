using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class SceneTreeTimeSignalExtensions
{
    public static async Task TimeoutAsync(this SceneTreeTimer timer)
    {
        await timer.ToSignal(timer, SceneTreeTimer.SignalName.Timeout);
    }
}
