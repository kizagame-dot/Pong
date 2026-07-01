using System;
using System.Data;
using Microsoft.Xna.Framework;

namespace Pong;

public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Color;
    public float life;
    public float max_Life;
    public float Size;
    public bool isAlive;

    public Particle()
    {
        
    }

    public void Update(float dt)
    {

    }
}
