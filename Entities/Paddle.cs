using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong.Entities;

public class Paddle
{
    public Rectangle Bounds {get; private set;}
    public float Speed { get;} = 350f;
    private float _y;
    private int _x;

    private Keys _upKey, _downKey;


    public Paddle(int x, int startY, Keys upKey, Keys downKey)
    {
        _x = x;
        _y = startY;
        _upKey = upKey;
        _downKey = downKey;
        Bounds = new Rectangle(x, startY,15,80);


    }

    public void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        KeyboardState kb = Keyboard.GetState();

        if(kb.IsKeyDown(_upKey)) _y -= Speed * dt;
        if(kb.IsKeyDown(_downKey)) _y += Speed * dt;

        _y = MathHelper.Clamp(_y,10,600-90);

        Bounds = new Rectangle (_x,(int)_y,15,80);


    }

    public void Draw(SpriteBatch spriteBatch, Texture2D pixel)
    {
        spriteBatch.Draw(pixel, Bounds, Color.White);
    }
}
