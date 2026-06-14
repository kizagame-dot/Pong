using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Entities;
using Pong.Core;
using Pong.Managers;
using Pong.Interface;
using Pong.Screens;


namespace Pong;

public class Game1 : Game
{

    // Handle window and talk to the graphic card
    private GraphicsDeviceManager _graphics;
    // Tool for draw (sprite , text , shape)
    private SpriteBatch _spriteBatch;
    // Texture who will be used for draw all rectangle
    private Texture2D _pixel;
    private SpriteFont _font;
    private GameState _gameState;
    private ScoreManager _scoreManager;
    private IScreen _currentScreen;
    

    



    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    public void SetScreen()
    {
        
    }

    protected override void Initialize()
    {
        _graphics.PreferredBackBufferWidth = 800;
        _graphics.PreferredBackBufferHeight = 600;
        _graphics.ApplyChanges();

                
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        //Create texture 1x1 pixel
        _pixel = new Texture2D(GraphicsDevice,1 ,1);

        //Give a array of color 
        _pixel.SetData(new[] {Color.White});

        _font = Content.Load<SpriteFont>("Score");

        _scoreManager = new ScoreManager();

        _currentScreen = new GameScreen(_spriteBatch,
                                        _pixel,
                                        _font,
                                        _graphics.PreferredBackBufferWidth,
                                        _graphics.PreferredBackBufferHeight,
                                        _scoreManager,
                                        Keys.Z, Keys.S,
                                        Keys.Up, Keys.Down);

    }

    protected override void Update(GameTime gameTime)
    {
        
        _currentScreen.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(new Color(15, 15, 50));
        
        // Start the batch draw.
        // SpriteSortMode.Deffered :Order of draw's call
        // BlendState.AlphaBlend : Handle the transparency
        _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

        _currentScreen.Draw();
 
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
