using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Entities;
using Pong.Interface;
using Pong.Managers;

namespace Pong.Screens;

public class GameScreen : IScreen
{
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;
    private SpriteFont _font;
    private ScoreManager _scoreManager;
    private Paddle _player1;
    private Paddle _player2;
    private Ball _ball;
    

    public GameScreen(SpriteBatch spriteBatch, Texture2D pixel, SpriteFont font,
                      int widthSize, int heightSize,
                      ScoreManager scoreManager,
                      Keys player1Up, Keys player1Down, Keys player2Up, Keys player2Down)
    {

        _spriteBatch = spriteBatch;
        _pixel = pixel;
        _font = font;

        _scoreManager = scoreManager;

        _player1 = new Paddle(20, 260, player1Up, player1Down);
        _player2 = new Paddle(765, 260, player2Up, player2Down);

        _ball = new Ball(widthSize, heightSize);

        
    }

    public void Update(GameTime gameTime)
    {
        _player1.Update(gameTime);
        _player2.Update(gameTime);
        _ball.Update(gameTime,_player1.Bounds,_player2.Bounds);

    }

    public void Draw()
    {
        for (int y = 0; y < 600; y += 30)
        {
            _spriteBatch.Draw(_pixel, new Rectangle(398, y, 4, 18), Color.White * 0.3f);
        }
        
        _spriteBatch.DrawString(_font,$"{_scoreManager._textDisplay}", new Vector2(318,20),Color.WhiteSmoke);

        _player1.Draw(_spriteBatch, _pixel);
        _player2.Draw(_spriteBatch, _pixel);

        _ball.Draw(_spriteBatch,_pixel);
    }
}
