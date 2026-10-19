using System;
using System.IO;
using System.Media;

namespace CybersecurityAwarenessBotPart2
{
    public static class AudioPlayer
    {
        public static void PlayAudio(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using (SoundPlayer player = new SoundPlayer(filePath))
                    {
                        player.Play();
                    }
                }
            }
            catch (Exception)
            {
                // Suppress audio errors if file path is missing or locked
            }
        }
    }
}