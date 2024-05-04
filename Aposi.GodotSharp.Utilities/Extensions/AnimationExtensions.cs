using Godot;

namespace Aposi.GodotSharp.Utilities.Extensions;

public static class AnimationExtensions
{
    /// <summary>
    /// Gets the most recent key played on a given track index at a given playback time.
    /// </summary>
    /// <param name="animation">Animation whose key is being searched for</param>
    /// <param name="trackIndex">Index of the track whose key is being searched for</param>
    /// <param name="time">Playback time</param>
    /// <returns>The index of the identified key, or -1 if no such key is found.</returns>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static int GetLastKeyAtTime(this Animation animation, int trackIndex, double time)
    {
        var keyCount = animation.TrackGetKeyCount(trackIndex);

        if (keyCount == 0)
        {
            throw new ArgumentException("The animation does not contain any keys in the given track", nameof(trackIndex));
        }

        if (time < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(time), "Time must be positive");
        }

        for (var keyIdx = 0; keyIdx < keyCount; keyIdx++)
        {
            var keyTime = animation.TrackGetKeyTime(trackIndex, keyIdx);
            if (keyTime >= time)
                return Math.Max(keyIdx - 1, 0);
        }

        return 0;
    }

    /// <summary>
    /// Attempts to get the most recent key played on a given track index at a given playback time.
    /// </summary>
    /// <param name="animation">Animation whose key is being searched for</param>
    /// <param name="trackIndex">Index of the track whose key is being searched for</param>
    /// <param name="time">Playback time</param>
    /// <param name="index">Contains the index of the key upon success, or 0 by default</param>
    /// <returns>Whether the key index was successfully found</returns>
    public static bool TryGetLastKeyAtTime(this Animation animation, int trackIndex, double time, out int index)
    {
        try
        {
            index = animation.GetLastKeyAtTime(trackIndex, time);
            return true;
        }
        catch (Exception)
        {
            index = 0;
            return false;
        }
    }

    /// <summary>
    /// Gets the value of the most recent key played on a given track index at a given playback time.
    /// </summary>
    /// <param name="animation">The Animation instance whose key is being searched for.</param>
    /// <param name="trackIndex">The index of the track whose key is being searched for.</param>
    /// <param name="time">The playback time at which the value is desired.</param>
    /// <returns>The value of the identified key</returns>
    /// <exception cref="ArgumentException">Thrown when the track index is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the playback time is negative.</exception>
    public static Variant GetLastKeyValueAtTime(this Animation animation, int trackIndex, double time)
    {
        var keyIdx = animation.GetLastKeyAtTime(trackIndex, time);
        return animation.TrackGetKeyValue(trackIndex, keyIdx);
    }

    /// <summary>
    /// Gets the value of the most recent key frame on the given track index at the given playback time.
    /// </summary>
    /// <param name="animation">Animation object whose keyframes are being searched for.</param>
    /// <param name="trackPath">The path of the track whose key frames are being searched for.</param>
    /// <param name="time">The playback time at which the value is desired.</param>
    /// <param name="trackType">The type of the track.</param>
    /// <returns>The value of the most recent key frame on the given track index at the given playback time.</returns>
    /// <exception cref="ArgumentException">Thrown when the track index is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the playback time is negative.</exception>
    public static Variant GetLastKeyValueAtTime(this Animation animation, string trackPath, double time,
        Animation.TrackType trackType = Animation.TrackType.Value)
    {
        var trackIndex = animation.FindTrack(trackPath, trackType);
        var keyIdx = animation.GetLastKeyAtTime(trackIndex, time);
        return animation.TrackGetKeyValue(trackIndex, keyIdx);
    }

    /// <summary>
    /// Gets the value of the most recent key played on a given track index at a given playback time.
    /// </summary>
    /// <param name="animation">The animation whose key is being searched for.</param>
    /// <param name="trackPath">The path of the track whose key frames are being searched for.</param>
    /// <param name="time">The playback time.</param>
    /// <param name="trackType">The type of the track.</param>
    /// <returns>
    /// The value of the identified key, or a default value if no such key is found.
    /// </returns>
    /// <exception cref="ArgumentException">Thrown when the track index is invalid.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the playback time is negative.</exception>
    public static T GetLastKeyValueAtTime<T>(this Animation animation, string trackPath, double time,
        Animation.TrackType trackType = Animation.TrackType.Value)
    {
        var trackIndex = animation.FindTrack(trackPath, trackType);
        var keyIdx = animation.GetLastKeyAtTime(trackIndex, time);
        var variantValue = animation.TrackGetKeyValue(trackIndex, keyIdx);
        return variantValue.As<T>();
    }
}
