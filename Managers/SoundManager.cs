using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Managers;

public class SoundManager
{
    private const int _SAMPLE_RATE = 44100;
    private Dictionary<string, SoundEffect> _sounds;
    
    public SoundManager()
    {
        _sounds = new Dictionary<string, SoundEffect>()
        {
            {"HitPaddle", generateBeep(440,0.05)},
            {"HitWall",generateBeep(300,0.03)},
            {"Point",generateBeep(600,0.25)},
            {"GameOver",generateBeep(200,0.80)},
        };
    }



    public SoundEffect generateBeep(int frequency, double durationSec)
    {
        int sampleCount = (int)(_SAMPLE_RATE * durationSec); 

        short[] data = new short[sampleCount];

        for(int i = 0; i < sampleCount; i++)
        {
             double t = (double)i  / _SAMPLE_RATE;

             data[i] = (short)(short.MaxValue * 0.5 * Math.Sin(2 * Math.PI * frequency * t));
        }

        byte[] bytes = new byte[sampleCount * 2];

        Buffer.BlockCopy(data, 0, bytes, 0 , bytes.Length);

        return new SoundEffect(bytes, _SAMPLE_RATE, AudioChannels.Mono);
    }

    public void play(string sound)
    {
        _sounds[sound].Play();
    }

    
}
