using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class SceneTreeExtensions
{
    public static async Task<Node> NodeAddedAsync(this SceneTree sceneTree)
    {
        return await sceneTree.SignalAsync<Node>(SceneTree.SignalName.NodeAdded);
    }

    public static async Task<Node> NodeConfigurationWarningChangedAsync(this SceneTree sceneTree)
    {
        return await sceneTree.SignalAsync<Node>(SceneTree.SignalName.NodeConfigurationWarningChanged);
    }

    public static async Task<Node> NodeRemovedAsync(this SceneTree sceneTree)
    {
        return await sceneTree.SignalAsync<Node>(SceneTree.SignalName.NodeRemoved);
    }

    public static async Task<Node> NodeRenamedAsync(this SceneTree sceneTree)
    {
        return await sceneTree.SignalAsync<Node>(SceneTree.SignalName.NodeRenamed);
    }

    public static async Task PhysicsFrameAsync(this SceneTree sceneTree)
    {
        await sceneTree.SignalAsync(SceneTree.SignalName.PhysicsFrame); 
    }

    public static async Task ProcessFrameAsync(this SceneTree sceneTree)
    {
        await sceneTree.SignalAsync(SceneTree.SignalName.ProcessFrame);
    }

    public static async Task TreeChangedAsync(this SceneTree sceneTree)
    {
        await sceneTree.SignalAsync(SceneTree.SignalName.TreeChanged);
    }

    public static async Task TreeProcessModeChangedAsync(this SceneTree sceneTree)
    {
        await sceneTree.SignalAsync(SceneTree.SignalName.TreeProcessModeChanged);
    }
}
