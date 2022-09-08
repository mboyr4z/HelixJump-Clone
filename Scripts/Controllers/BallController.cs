using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallController : MonoSingleton<BallController>
{

    [SerializeField] BallData ballData;

    [SerializeField] private GameObject splashPrefab;
    [SerializeField] private Transform tower;

    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip deathSound;

    
    private SoundManager soundManager;
    private GameManager gameManager;

    private ComboModeController comboMode;
    private MeshRenderer meshRenderer;

    private Rigidbody rigidbody;

    private Vector3 firstPos;
    private float lastJumpTime = -3f;

    void Start()
    {

        
        soundManager = SoundManager.instance;
        gameManager = GameManager.instance;

        comboMode = GetComponent<ComboModeController>();
        rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();

        SetMaterialColorByLevel();

        firstPos = transform.position;

        Actions.act_replay += MoveFirstPos;
        Actions.act_nextLevel += MoveFirstPos;

        Actions.act_nextLevel += SetMaterialColorByLevel;
    }

    private void SetMaterialColorByLevel()
    {
        meshRenderer.material.color = gameManager.GetBallColor();
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = new Vector3(0,Mathf.Clamp(rigidbody.velocity.y, -7,5),0) ;
    }



    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IFinishLine>()?.Finish(Win);
        other.GetComponent<IInteractable>()?.Interact(RunFunctionAsLineCategoryType, comboMode.InComboMode);
    }


    private void RunFunctionAsLineCategoryType(LineCategory lineCategory)
    {
        if (lineCategory == LineCategory.good)
        {
            Jump();
            //soundManager?.PlayVoice(jumpSound);
            Actions.act_bounce?.Invoke();
        }
        else
        {
            soundManager?.PlayVoice(deathSound);
            Death();
            Actions.act_lose?.Invoke();
        }
    }

  
    private void Vibrate()
    {
        transform.DOShakeScale(0.2f,0.1f,3,40,true);
    }


    public void Jump()
    {
        if (Time.time - lastJumpTime > ballData.deltaJumpTime)
        {
            ParticleSystemManager.instance.RunParticleSystem(transform.position) ;
            lastJumpTime = Time.time;
            rigidbody.velocity = Vector3.zero;
            rigidbody.AddForce(transform.up * ballData.bounceForce);
            Vibrate();
        }

    }
    private void MoveFirstPos()
    {
        transform.position = firstPos;
        rigidbody.velocity = Vector3.zero;
    }

    public void Win()
    {
        Time.timeScale = 0;
        Actions.act_win?.Invoke();
        GameManager.instance.State = GameStates.winPanel;
    }

    private void Death()
    {
        GameManager.instance.State = GameStates.losePanel;
        Time.timeScale = 0;
        Actions.act_lose?.Invoke();
    }


}
