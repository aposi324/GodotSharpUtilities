using Aposi.GodotSharp.Utilities.Exceptions;
using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class AnimationPlayerExtensions
{
    /// <summary>
    /// Asynchronously waits for the animation to finish playing.
    /// <param name="animationPlayer">The AnimationPlayer node.</param>
    /// <param name="forceWait">If set to true, the method will wait even if the animation is set to loop.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the name of the finished animation.</returns>
    /// <exception cref="NodeStateException">Thrown when the animation is set to loop and forceWait is false.</exception>
    /// </summary>
    public static async Task<string> WaitForAnimationFinishAsync(this AnimationPlayer animationPlayer,
        bool forceWait = false)
    {
        var animation = animationPlayer.GetCurrentAnimationObject();
        if (animation == null)
            return "";

        if (animation.LoopMode != Animation.LoopModeEnum.None && !forceWait)
        {
            throw new NodeStateException("Animation is set to loop. Awaiting may result in deadlocking. Aborting.");
        }

        await animationPlayer.WaitForNextPhysicsFrameAsync();
        return (await animationPlayer.ToSignal(animationPlayer, AnimationMixer.SignalName.AnimationFinished)).First().AsString();
    }

    /// <summary>
    /// Plays the full animation asynchronously and returns a completed task when complete.
    /// </summary>
    /// <param name="animationPlayer">The AnimationPlayer node.</param>
    /// <param name="animationName">The name of the animation to play.</param>
    /// <exception cref="NodeStateException">Thrown when the animation is set to loop and forceWait is false.</exception>
    public static async Task PlayFullAnimationAsync(this AnimationPlayer animationPlayer, string animationName)
    {
        animationPlayer.CallThreadSafe("play", Variant.CreateFrom(animationName));
        await animationPlayer.WaitForAnimationFinishAsync();
    }

    /// <summary>
    /// Retrieves the currently playing Animation object of the AnimationPlayer.
    /// </summary>
    /// <param name="animationPlayer">The AnimationPlayer node.</param>
    /// <returns>The Animation object representing the currently playing animation, or null if no animation is currently playing.</returns>
    public static Animation? GetCurrentAnimationObject(this AnimationPlayer animationPlayer)
    {
        var animationName = animationPlayer.CurrentAnimation;

        return animationName == null ? null : animationPlayer.GetAnimation(animationPlayer.CurrentAnimation);
    }

    #region Signal Wrappers
    // todo: rest of signals
    public static async Task<(string oldName, string newName)> AnimationChanged(this AnimationPlayer animationPlayer)
    {
        var results = await animationPlayer.ToSignal(animationPlayer, AnimationPlayer.SignalName.AnimationChanged);
        return (results[0].AsString(), results[1].AsString());
    }
    #endregion
}
