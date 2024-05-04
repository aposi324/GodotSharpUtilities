using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class AnimatedSprite3dExtensions
{
    #region Signal Wrappers
    public static async Task AnimationChangedAsync(this AnimatedSprite3D animatedSprite3D)
    {
        await animatedSprite3D.SignalAsync(AnimatedSprite3D.SignalName.AnimationChanged);
    }

    public static async Task AnimationFinishedAsync(this AnimatedSprite3D animatedSprite3D)
    {
        await animatedSprite3D.SignalAsync( AnimatedSprite3D.SignalName.AnimationFinished);
    }

    public static async Task SpriteFramesChangedAsync(this AnimatedSprite3D animatedSprite3D)
    {
        await animatedSprite3D.SignalAsync(AnimatedSprite3D.SignalName.SpriteFramesChanged);
    }

    public static async Task AnimationLoopedAsync(this AnimatedSprite3D animatedSprite3D)
    {
        await animatedSprite3D.SignalAsync( AnimatedSprite3D.SignalName.AnimationLooped);
    }

    public static async Task FrameChangedAsync(this AnimatedSprite3D animatedSprite3D)
    {
        await animatedSprite3D.SignalAsync(AnimatedSprite3D.SignalName.FrameChanged);
    }
    #endregion
}
