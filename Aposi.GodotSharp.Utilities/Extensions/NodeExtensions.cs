using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class NodeExtensions
{
    /// <summary>
    /// Retrieves all the children nodes of the specified type from the given node.
    /// </summary>
    /// <typeparam name="T">The type of nodes to retrieve.</typeparam>
    /// <param name="node">The node from which to retrieve the children nodes.</param>
    /// <param name="recursive">If set to <c>true</c>, retrieves the children nodes recursively.</param>
    /// <returns>A collection of children nodes of the specified type.</returns>
    public static IEnumerable<T> GetChildren<T>(this Node node, bool recursive = false) where T : Node
    {
        List<T> list = [];

        foreach (var child in node.GetChildren())
        {
            if (child is T childOfDesiredType)
                list.Add(childOfDesiredType);
            if (recursive)
                list.AddRange(child.GetChildren<T>(recursive));
        }

        return list;
    }

    /// <summary>
    /// Waits for the next physics frame in the Godot scene.
    /// </summary>
    /// <param name="node">The node that the method is called on.</param>
    public static async Task WaitForNextPhysicsFrameAsync(this Node node)
    {
        await node.ToSignal(node.GetTree(), "physics_frame");
    }

    /// <summary>
    /// Waits for the specified number of seconds in the Godot scene.
    /// </summary>
    /// <param name="node">The node that the method is called on.</param>
    /// <param name="seconds">The number of seconds to wait.</param>
    public static async Task Wait(this Node node, float seconds)
    {
        await node.GetTree().CreateTimer(seconds).TimeoutAsync();
    }

    /// <summary>
    /// Retrieves the node of the specified type from the given node with strict checking.
    /// </summary>
    /// <typeparam name="T">The type of node to retrieve.</typeparam>
    /// <param name="node">The node from which to retrieve the target node.</param>
    /// <param name="path">The path to the target node.</param>
    /// <returns>The node of the specified type at the given path.</returns>
    public static T GetNodeStrict<T>(this Node node, NodePath path) where T : Node =>
        node.GetNode<T>(path) ?? throw new NullReferenceException($"Node at {path} not found");


    #region Signal Wrappers
    public static async Task<Node> ChildEnteredTreeAsync(this Node node)
    {
        var results = await node.ToSignal(node, Node.SignalName.ChildEnteredTree);
        return results.First().As<Node>();
    }

    public static async Task<Node> ChildExitingTreeAsync(this Node node)
    {
        var results = await node.ToSignal(node, Node.SignalName.ChildExitingTree);
        return results.First().As<Node>();
    }

    public static async Task ReadyAsync(this Node node)
    {
        await node.ToSignal(node, Node.SignalName.Ready);
    }

    public static async Task<Node> RenamedAsync(this Node node)
    {
        return await node.SignalAsync<Node>(Node.SignalName.Renamed);
    }

    public static async Task<Node> ReplacingByAsync(this Node node)
    {
        return await node.SignalAsync<Node>(Node.SignalName.ReplacingBy);
    }

    public static async Task TreeEnteredAsync(this Node node)
    {
        await node.ToSignal(node, Node.SignalName.TreeEntered);
    }

    public static async Task TreeExitedAsync(this Node node)
    {
        await node.ToSignal(node, Node.SignalName.TreeExited);
    }

    public static async Task TreeExitingAsync(this Node node)
    {
        await node.ToSignal(node, Node.SignalName.TreeExiting);
    }
    #endregion
}
