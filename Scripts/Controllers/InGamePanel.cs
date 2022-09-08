using UnityEngine;
using UnityEngine.UI;

public class InGamePanel : MonoBehaviour
{
    [SerializeField] private Text scoreText;
        
    [SerializeField] private Text bestScoreText;

    [SerializeField] private Slider slider;

    [SerializeField] private Text leftLevel, rightLevel;

    private GameManager gameManager;


    private void OnEnable()
    {
        gameManager = GameManager.instance;

        WriteBestScore();
        SetLeftAndRightLevelSign();
        
    }

    private void Start()
    {
        Actions.act_PointChanged += WriteScore;
        Actions.act_TouchedScoreTrigger += SetSlider;

        Actions.act_replay += SetSliderZero;
        Actions.act_nextLevel += SetSliderZero;

        Actions.act_newRecord += WriteBestScore;

        Actions.act_win += SetSlider;

        Actions.act_replay += WriteScoreZero;
        Actions.act_nextLevel += WriteScoreZero;

        Actions.act_replay += SetLeftAndRightLevelSign; 
        Actions.act_nextLevel += SetLeftAndRightLevelSign;
    }

    private void SetSliderZero()
    {
        slider.value = 0;
    }

    private void SetLeftAndRightLevelSign()
    {
        leftLevel.text = (gameManager.CurrentLevel + 1).ToString();
        rightLevel.text = (gameManager.CurrentLevel + 2).ToString();
    }

    private void SetSlider()
    {
       slider.value = (float)gameManager.Score / (float)(gameManager.GetCircleCount());
    }


    private void WriteScoreZero()
    {
        scoreText.text = "0";
    }

    private void WriteScore()
    {
        scoreText.text = gameManager.Point.ToString();
    }

    private void WriteBestScore()
    {
        bestScoreText.text = gameManager.BestScore.ToString();
    }
}
