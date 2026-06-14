using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Managers;

public class ScoreManager
{
    private int _scorePlayer1;
    private int _scorePlayer2;
    public string _textDisplay {get; private set;}

    public ScoreManager()
    {
        _scorePlayer1 = 0;
        _scorePlayer2 = 0;
        
        _textDisplay = $"{_scorePlayer1}     {_scorePlayer2}";
    }

    
    public void AddPoint(int currentPlayer)
    {
        _scorePlayer1 += 1;

        if(currentPlayer == 1)
        {
            _scorePlayer1 += 1;
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
        return true;
    }

}
