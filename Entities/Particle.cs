using System;
using System.Data;
using Microsoft.Xna.Framework;

namespace Pong;

public class Particle
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Color;
    public float Life;
    public float MaxLife;
    public float Size;
    public bool IsAlive => Life > 0;

    public Particle()
    {
        
    }

    public void Update(float dt)
    {
        // Move with speed, 
        Position += Velocity * dt;

        // Slowing 8% by frame
        Velocity *= 0.92f;

        //ex : dt : 0.016   MaxLife: 0.5 ---> reduce of 3.2% per frame
        Life -= dt / MaxLife;
    }
}
