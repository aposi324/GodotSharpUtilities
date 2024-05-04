using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class GodotObjectExtensions
{
    public static async Task<Variant[]?> SignalAsync(this GodotObject obj, StringName signal)
    {
        return await obj.ToSignal(obj, signal);
    }

    public static async Task<T> SignalAsync<T>(this GodotObject obj, StringName signal)
    {
        var result = (await obj.SignalAsync(signal)) ?? throw new NullReferenceException("Signal did not return desired value");

        return result.First().As<T>();
    }

    #region Signals with specific return types
    public static async Task<Vector2> SignalAsVector2Async(this GodotObject obj, StringName signal)
    {
        var result = (await obj.SignalAsync(signal)) ?? throw new NullReferenceException("Signal did not return desired value");
        return result.First().AsVector2();
    }

    public static async Task<Vector3> SignalAsVector3Async(this GodotObject obj, StringName signal)
    {
        var result = (await obj.SignalAsync(signal)) ?? throw new NullReferenceException("Signal did not return desired value");
        return result.First().AsVector3();
    }

    public static async Task<Vector2[]> SignalAsVector2ArrayAsync(this GodotObject obj, StringName signal)
    {
        var result = (await obj.SignalAsync(signal)) ?? throw new NullReferenceException("Signal did not return desired value");
        return result.First().AsVector2Array();
    }

    public static async Task<Vector3[]> SignalAsVector3ArrayAsync(this GodotObject obj, StringName signal)
    {
        var result = (await obj.SignalAsync(signal)) ?? throw new NullReferenceException("Signal did not return desired value");
        return result.First().AsVector3Array();
    }
    #endregion


    #region Signal Wrappers
    public static async Task PropertyListChanged(this GodotObject obj)
    {
        await obj.SignalAsync(GodotObject.SignalName.PropertyListChanged);
    }

    public static async Task ScriptChanged(this GodotObject obj)
    {
        await obj.SignalAsync(GodotObject.SignalName.ScriptChanged);
    }
    #endregion
}
