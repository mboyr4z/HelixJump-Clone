using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboModeController : MonoBehaviour
{
   

    [SerializeField] private BallData ballData;

    [SerializeField] private Gradient comboModeGradient;

    [SerializeField] private Gradient normalGradient;

    [SerializeField] private AudioClip comboSound;

    private SoundManager soundManager;

    private GameManager gameManager;

    private PointCreator pointCreator;

    private bool inComboMode = false;

    private int comboCount = 0;

    private TrailRenderer trailRenderer;

    

    public bool InComboMode { get => inComboMode; set => inComboMode = value; }

    void Start()
    {


        gameManager = GameManager.instance;
        pointCreator = PointCreator.instance;
        soundManager = SoundManager.instance;

        trailRenderer = GetComponent<TrailRenderer>();

        Actions.act_replay += CloseTrailRenderer;
        Actions.act_nextLevel += CloseTrailRenderer;

        Actions.act_replay += ResetComboMode;
        Actions.act_nextLevel += ResetComboMode;
        Actions.act_bounce += ResetComboMode;

        Actions.act_TouchedScoreTrigger += TryingToGetComboMode;

    }

    private void CloseTrailRenderer()
    {
        trailRenderer.time = 0;
        trailRenderer.time = 0.3f;
    }

    


    IEnumerator ResetComboMode(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ResetComboMode();
    }

    public void ResetComboMode()
    {
        StopAllCoroutines();
        InComboMode = false;
        ResetComboCount();
        SetGradientOfTrailRenderer(normalGradient);
    }
    

    private void TryingToGetComboMode()
    {
        IncreaseComboCount();

        if (comboCount >= ballData.ComboModeCount )
        {
            if (!InComboMode)
            {
                InComboMode = true;
                StartCoroutine(ResetComboMode(ballData.ComboModeTime));
                SetGradientOfTrailRenderer(comboModeGradient);
            }     
        }
    }

    public void CheckComboModeAndIncreatePoint()
    {
        
        int pointsToBeIncreased;
        if (InComboMode)
        {
            pointsToBeIncreased = comboCount * (GameManager.instance.CurrentLevel + 1) ;
        }
        else
        {
            pointsToBeIncreased = GameManager.instance.CurrentLevel + 1;
        }

        gameManager.IncreasePoint(pointsToBeIncreased);
        Actions.InvokeAction(Actions.act_PointChanged);
        pointCreator.CreateIncreaseText(pointsToBeIncreased);
        
    }

    private void ResetComboCount()
    {
        comboCount = 0;
    }

    private void IncreaseComboCount()
    {
        comboCount++;
    }

    private void SetGradientOfTrailRenderer(Gradient gradient)
    {
        trailRenderer.colorGradient = gradient;
    }

    
}
