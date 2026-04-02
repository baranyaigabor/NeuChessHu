using System.IO;
using System.Media;

namespace NeuChessHu.Services.SoundServices;

internal static class Sounds
{
    static readonly Dictionary<string, byte[]> sounds = [];

    internal static string[] SoundFiles { get; }
    internal static bool IsMuted { get; set; }

    static Sounds() =>
        SoundFiles = Directory.GetFiles(
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Sounds"), "*.wav");

    internal static void LoadToMemory()
    {
        foreach (string file in SoundFiles)
        {
            string name = Path.GetFileNameWithoutExtension(file);
            sounds[name] = File.ReadAllBytes(file);
        }
    }

    internal static void Play(string name)
    {
        if (IsMuted) return;

        if (!sounds.TryGetValue(name, out byte[]? bytes))
            throw new ArgumentException($"Sound '{name}' has not been found in the memory!");

        Task.Run(() =>
        {
            using MemoryStream stream = new(bytes);
            using SoundPlayer player = new(stream);
            player.PlaySync();
        });
    }
}