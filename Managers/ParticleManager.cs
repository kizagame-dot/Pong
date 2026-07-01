using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong;

public class ParticleManager
{
    private List<Particle> _particles = new List<Particle>() ;

    private Random _rng = new Random();


    public Particle CreateParticle(Vector2 position, Color color)
    {
        float angle = (float)(_rng.NextDouble() * Math.PI * 2 );
        float speed = (float)(_rng.NextDouble() * 200 + 50 );

        return new Particle
        {
            Position = position,
            Velocity = new Vector2(
                (float)Math.Cos(angle),
                (float)Math.Sin(angle)
            ) * speed,
            Color = color,
            Life = 1.0f,
            MaxLife =  (float)(_rng.NextDouble() * 0.3 + 0.3),
            Size = (float)(_rng.NextDouble() * 4 + 2),

        };
    }

    public void Emit(Vector2 position, int count, Color color)
    {
        for(int i=0; i < count ; i++)
        {
            _particles.Add(CreateParticle(position,color));
        }
    }


    public void Update(float dt)
    {
        foreach(var p in _particles)
        {
            p.Update(dt);
        }

        _particles.RemoveAll(p => !p.IsAlive );
    }


    public void Draw(SpriteBatch _spriteBatch, Texture2D pixel)

    {
        foreach(var p in _particles)
        {
            // Opacité = durée de vie restante
            // Life=1.0 → Color * 1.0 = couleur pleine
            // Life=0.5 → Color * 0.5 = semi-transparent
            // Life=0.0 → Color * 0.0 = invisible
            Color drawColor = p.Color * p.Life;

            // Taille proportionnelle à la vie restante
            // Particule rétrécit en mourant
            // (int) + 1 pour éviter taille 0 (invisible mais dessiné quand même)
            int size = (int)(p.Size * p.Life) +1 ;

            _spriteBatch.Draw(
                pixel,
                new Rectangle(
                    (int)p.Position.X,
                    (int)p.Position.Y,
                    size,
                    size
                ),
                drawColor
            );
   
        }
    }
}
