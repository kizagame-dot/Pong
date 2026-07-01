using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Core;
using Pong.Entities;
using Pong.Interface;
using Pong.Managers;

namespace Pong.Screens;

public class GameScreen : IScreen
{
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private Dictionary<string, SpriteFont> _fonts;
    private ScoreManager _scoreManager;
    private SoundManager _soundManager;
    private Paddle _player1;
    private Paddle _player2;
    private Ball _ball;
    private GameState _gameState = GameState.WaitingToStart;
    private KeyboardState _previousKb;



    public GameScreen(SpriteBatch spriteBatch, Texture2D pixel, Dictionary<string, SpriteFont> fonts,
                      int widthSize, int heightSize,
                      ScoreManager scoreManager,
                      SoundManager soundManager,
                      Keys player1Up, Keys player1Down, Keys player2Up, Keys player2Down)
    {

        _spriteBatch = spriteBatch;
        _pixel = pixel;
        
        _fonts = fonts;

        _scoreManager = scoreManager;
        _soundManager = soundManager;

        _player1 = new Paddle(20, 260, player1Up, player1Down);
        _player2 = new Paddle(765, 260, player2Up, player2Down);

        _ball = new Ball(widthSize, heightSize, soundManager);


    }

    public void Update(GameTime gameTime)
    {
        KeyboardState currentKb = Keyboard.GetState();

        switch(_gameState)
        {
            case GameState.WaitingToStart:

                if(currentKb.IsKeyDown(Keys.Space) && _previousKb.IsKeyUp(Keys.Space))
                    _gameState = GameState.Playing;
                
                break;
                    
            case GameState.Playing:

                _player1.Update(gameTime);
                _player2.Update(gameTime);

                int point = _ball.Update(gameTime, _player1.Bounds, _player2.Bounds);

                if (point == 1)                
                    _scoreManager.AddPoint(2);

                if (point == -1)                
                    _scoreManager.AddPoint(1);

                if(_scoreManager.IsGameOver())
                {
                    _soundManager.play("GameOver");
                    _gameState = GameState.GameOver;
                }

                break;

            case GameState.GameOver:

                if(currentKb.IsKeyDown(Keys.Space) && _previousKb.IsKeyUp(Keys.Space))
                {
                    _scoreManager.Reset();
                    _ball.Reset();
                    _gameState = GameState.WaitingToStart;
                }

                break;


        }
        _previousKb = currentKb;


    }

    public void Draw()
    {

        //Field
        for (int y = 0; y < 600; y += 30)
        {
            _spriteBatch.Draw(_pixel, new Rectangle(398, y, 4, 18), Color.White * 0.3f);
        }

        //Score
        _spriteBatch.DrawString(_fonts["Score"], $"{_scoreManager.TextDisplay}", new Vector2(318, 20), Color.WhiteSmoke);

        _player1.Draw(_spriteBatch, _pixel);
        _player2.Draw(_spriteBatch, _pixel);

        _ball.Draw(_spriteBatch, _pixel);

        //Message State

        switch (_gameState)
        {
            case GameState.WaitingToStart:
                _spriteBatch.DrawString(_fonts["Message"],"SPACE FOR PLAYING", new Vector2(320,315), Color.LightGoldenrodYellow);
                break;

            case GameState.GameOver:
            
                _spriteBatch.DrawString(_fonts["Message"],
                _scoreManager.ScorePlayer1 > _scoreManager.ScorePlayer2 ?
                "GAME OVER : PLAYER 1 WIN" :
                "GAME OVER : PLAYER 2 WIN",
                  new Vector2(270, 250), Color.LightGoldenrodYellow);

                _spriteBatch.DrawString(_fonts["Message"], "SPACE FOR PLAYING", new Vector2(190, 320), Color.LightGoldenrodYellow);

                break;        

        }

    }
}
