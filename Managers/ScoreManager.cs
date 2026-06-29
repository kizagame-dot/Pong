using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Managers;

public class ScoreManager
{
    private const int WIN_SCORE = 7;
    public int _scorePlayer1 { get; private set;}
    public int _scorePlayer2 {get; private set;}
    public string _textDisplay => $"{_scorePlayer1}     {_scorePlayer2}";

    public ScoreManager()
    {
        _scorePlayer1 = 0;
        _scorePlayer2 = 0;
        
    }

    
    public void AddPoint(int currentPlayer)
    {

        if(currentPlayer == 1)
        {
            _scorePlayer1 = _scorePlayer1 + 1;
        }

        if(currentPlayer == 2)
        {
            _scorePlayer2 += 1;
        }
    }

    public void Reset()
    {
        _scorePlayer1 = 0;
        _scorePlayer2 = 0;
    }

    public bool IsGameOver()
    {
        return _scorePlayer1 >= WIN_SCORE || _scorePlayer2 >= WIN_SCORE;
    }

}
