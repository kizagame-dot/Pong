using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Entities;
using Pong.Core;


namespace Pong;

public class Game1 : Game
{

    // Handle window and talk to the graphic card
    private GraphicsDeviceManager _graphics;
    // Tool for draw (sprite , text , shape)
    private SpriteBatch _spriteBatch;
    // Texture who will be used for draw all rectangle
    private Texture2D _pixel;
    
    private Paddle _player1;
    private Paddle _player2;
    private Ball _ball;
    private GameState _gameState;



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

        // Initialize Paddle Object
        _player1 = new Paddle(20, 260, Keys.Z, Keys.S);
        _player2 = new Paddle(765, 260, Keys.Up, Keys.Down);

        _ball = new Ball(_graphics.PreferredBackBufferWidth,
                        _graphics.PreferredBackBufferHeight);

        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //Create texture 1x1 pixel
        _pixel = new Texture2D(GraphicsDevice,1 ,1);

        //Give a array of color 
        _pixel.SetData(new[] {Color.White});

    }

    protected override void Update(GameTime gameTime)
    {
        
        _ball.Update(gameTime,_player1.Bounds,_player2.Bounds);

        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(15, 15, 50));
        
        // Start the batch draw.
        // SpriteSortMode.Deffered :Order of draw's call
        // BlendState.AlphaBlend : Handle the transparency
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        for (int y = 0; y < 600; y += 30)
        {
            _spriteBatch.Draw(_pixel, new Rectangle(398, y, 4, 18), Color.White * 0.3f);
        }
        
        _player1.Draw(_spriteBatch, _pixel);
        _player2.Draw(_spriteBatch, _pixel);

        _ball.Draw(_spriteBatch,_pixel);

    
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
