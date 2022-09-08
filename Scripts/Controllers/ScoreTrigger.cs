using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class ScoreTrigger : MonoBehaviour, IInteractable
{

    [SerializeField] private AudioClip increaseScoreSound;

    private GameManager gameManager;

    private Circle parentCircle;

    private ComboModeController ballComboModeController;
    private void Start()
    {
        gameManager = GameManager.instance;
        parentCircle = transform.parent.GetComponent<Circle>();
        ballComboModeController = BallController.instance.GetComponent<ComboModeController>();
    }

    public void DashedScoreTrigger()
    {
        gameManager.IncreaseScore();
        SoundManager.instance.PlayVoice(increaseScoreSound);
        Actions.InvokeAction(Actions.act_TouchedScoreTrigger);

        this.CloseCollider();
        parentCircle.Smash();

        ballComboModeController.CheckComboModeAndIncreatePoint();  // print point
    }

    public void Interact(Action<LineCategory> action, bool isBallInComboMode)
    {
        DashedScoreTrigger();
    }

    public void CloseCollider()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
