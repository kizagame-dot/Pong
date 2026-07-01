using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong.Managers;

public class ScoreManager
{
    private const int _WIN_SCORE = 7;
    public int ScorePlayer1 { get; private set;}
    public int ScorePlayer2 {get; private set;}
    public string TextDisplay => $"{ScorePlayer1}     {ScorePlayer2}";

    public ScoreManager()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
        
    }

    
    public void AddPoint(int currentPlayer)
    {

        if(currentPlayer == 1)
        {
            ScorePlayer1 = ScorePlayer1 + 1;
        }

        if(currentPlayer == 2)
        {
            ScorePlayer2 += 1;
        }
    }

    public void Reset()
    {
        ScorePlayer1 = 0;
        ScorePlayer2 = 0;
    }

    public bool IsGameOver()
    {
        return ScorePlayer1 >= _WIN_SCORE || ScorePlayer2 >= _WIN_SCORE;
    }

}
