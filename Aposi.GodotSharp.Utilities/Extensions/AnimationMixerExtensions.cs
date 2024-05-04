using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class AnimationMixerExtensions
{
    //todo: rest of signals
    public static async Task<StringName> AnimationFinishedAsync(this AnimationMixer animationMixer)
    {
        var results = await animationMixer.ToSignal(animationMixer, AnimationMixer.SignalName.AnimationFinished);
        return results[0].AsStringName();
    }
}
