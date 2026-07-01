using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Managers;

namespace Pong.Entities;

public class Ball
{
    
    public const int BALL_SIZE = 12;
    public Rectangle Bounds => new Rectangle((int)_position.X, (int)_position.Y, BALL_SIZE, BALL_SIZE);

    private Vector2 _position;
    private Vector2 _velocity;
    private int _screenWidth;
    private int _screenHeight;
    private Random _rng = new Random();
    private SoundManager _soundManager;
    private ParticleManager _particleManager;

    public Ball(int screenWidth, int screenHeight, SoundManager soundManager, ParticleManager particleManager)
    {
        _screenWidth = screenWidth;
        _screenHeight = screenHeight;
        _soundManager = soundManager;
        _particleManager = particleManager;
        Reset();

    }

    public void Reset()
    {
        //Center the ball
        _position = new Vector2(_screenWidth / 2f - BALL_SIZE / 2f,_screenHeight / 2f - BALL_SIZE / 2f);

        // Random Speed Initialize
        float angle = (float)(_rng.NextDouble() * 60 - 30);
        float speed = 300f;
        int direction = _rng.Next(2) == 0 ? 1 : -1;

        _velocity = new Vector2(
            direction * speed * (float)Math.Cos(MathHelper.ToRadians(angle)),
            speed * (float)Math.Sin(MathHelper.ToRadians(angle))
        );
        
    }

    public int Update(GameTime gameTime, Rectangle paddleLeft, Rectangle paddleRight)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

        _position += _velocity * dt;


        // Rebound UP / DOWN
        if(_position.Y < 0)
        {
            _position.Y = 0;
            _velocity.Y = Math.Abs(_velocity.Y);
            _soundManager.play("HitWall");
        }
        if(_position.Y + BALL_SIZE > _screenHeight)
        {
            _position.Y = _screenHeight - BALL_SIZE;
            _velocity.Y = -Math.Abs(_velocity.Y);
            _soundManager.play("HitWall");

        }

        // Collision paddle 
        if(Bounds.Intersects(paddleLeft) && _velocity.X < 0)
        {
            ApplyAngle(paddleLeft);
            _soundManager.play("HitPaddle");
            _particleManager.Emit(
                new Vector2(_position.X, _position.Y + BALL_SIZE/2f),
                12,
                Color.Yellow
            );

        }

        if(Bounds.Intersects(paddleRight) && _velocity.X > 0)
        {
            ApplyAngle(paddleRight);
            _soundManager.play("HitPaddle");
            _particleManager.Emit(
                new Vector2(_position.X, _position.Y + BALL_SIZE/2f),
                12,
                Color.Yellow
            );


        }

        //Ball quit the screen
        if(_position.X < 0)
        {
            _soundManager.play("Point");
            _particleManager.Emit(
                new Vector2(_position.X, _position.Y),
                30,
                Color.OrangeRed
            );
            Reset();
            return 1;
        }

        if(_position.X > _screenWidth)
        {
            _soundManager.play("Point");
            _particleManager.Emit(
                new Vector2(_position.X, _position.Y),
                30,
                Color.OrangeRed
            );
            Reset();
            return -1;
        }
        
        return 0;
    }

    public void ApplyAngle(Rectangle paddle)
    {
        float paddleCenter = paddle.Y + paddle.Height / 2f;
        float ballCenter = _position.Y + BALL_SIZE / 2f;
        float relativeHit = (ballCenter - paddleCenter) / (paddle.Height / 2f);
        relativeHit = MathHelper.Clamp(relativeHit, -1f, 1f);

        float bounceAngle = relativeHit * 55f;
        float speed = Math.Min(_velocity.Length() * 1.05f, 600f);

        _velocity.X = -Math.Sign(_velocity.X) * speed * (float)Math.Cos(MathHelper.ToRadians(bounceAngle));
        _velocity.Y = speed * (float)Math.Sin(MathHelper.ToRadians(bounceAngle));

    }
    public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
    {
        spriteBatch.Draw(pixel,Bounds,Color.Yellow);
    }
}
