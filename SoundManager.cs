using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameProject
{
    public static class SoundManager
    {
        public static void PlaySFX(string filePath)
        {
            try
            {
                var reader = new AudioFileReader(filePath);
                var sfx = new WaveOutEvent();
                sfx.Init(reader);
                sfx.Play();

                sfx.PlaybackStopped += (s, a) =>
                {
                    sfx.Dispose();
                    reader.Dispose();
                };
            }
            catch { }
        }
    }
}
