using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public class GameManager : MonoSingleton<GameManager>
{  
    private int score;

    private int bestScore;

    private int currentLevel;

    private GameStates state;

    public int Score
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefss.score.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefss.score.ToString(), value);
        }
    }

    

    public int CurrentLevel
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefss.currentLevel.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefss.currentLevel.ToString(), value);
        }
    }


    public int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefss.bestScore.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefss.bestScore.ToString(), value);
        }
    }

    public int Point
    {
        get
        {
            return PlayerPrefs.GetInt(PlayerPrefss.point.ToString());
        }
        set
        {
            PlayerPrefs.SetInt(PlayerPrefss.point.ToString(), value);
        }
    }

    public GameStates State { get => state; set => state = value; }

    private Level level;

    private void Awake()
    {
        FindLevel();
    }

    void Start()
    {
        
        Score = 0;
        Point = 0;
        State = GameStates.inGame;

        Actions.act_lose += CheckBestScore;
        Actions.act_win += CheckBestScore;

        Actions.act_replay += RestartGame;

    }


    private void RestartGame()
    {
        State = GameStates.inGame;
        Score = 0;
        Point = 0;
        Time.timeScale = 1;
    }

    private void CheckBestScore()
    {
        if (Point > BestScore)
        {
            BestScore = Point;
            Actions.InvokeAction(Actions.act_newRecord);
        }
    }

    public void IncreaseScore()
    {
        Score++;
    }

    public void IncreasePoint(int addeddPoint)
    {
        Point += addeddPoint;
    }

    public void NextLevel()
    {
        CurrentLevel += 1;
        FindLevel();
        MaterialManager.instance.SetColorsOfMaterials(GetGoodLineColor(), GetBadLineColor());
        Actions.InvokeAction(Actions.act_nextLevel);
        RestartGame();
    }


    public void FindLevel()
    {
        level = (Level)Resources.Load("Level" + (CurrentLevel % GetTotalLevelCount()));
    }

    private int GetTotalLevelCount()
    {
        int count = 0;

        for (count = 0; ; count++)
        {
            if (Resources.Load("Level" + count))
            {
            }
            else
            {
                break;
            }

        }

        return count;

    }

    public int GetCircleCount()
    {
        return level.circleCount;
    }

    public Color GetBallColor()
    {
        return level.levelTheme.ballColor;
    }

    public Color GetTowerColor()
    {
        return level.levelTheme.towerColor;
    }

    public Color GetSplashColor()
    {
        return level.levelTheme.splashColor;
    }

    public Color GetGoodLineColor()
    {
        return level.levelTheme.goodLineColor;
    }

    public Color GetBadLineColor()
    {
        return level.levelTheme.badLine;
    }
}



public enum PlayerPrefss
{
    score,
    bestScore,
    currentLevel,
    point
}

public enum LineCategory
{
    good,
    bad
}

public enum GameStates
{
    inGame,
    losePanel,
    winPanel
}
