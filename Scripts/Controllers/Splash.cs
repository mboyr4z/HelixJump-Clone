using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Splash : MonoBehaviour,  IPoolObject 
{
    [SerializeField] private float liveTime;

    private ObjectPoolManager objectPoolManager;

    private SpriteRenderer spriteRenderer;

    private GameManager gameManager;

    Color firstColor;

    Color transparentColor;

    private void Awake()
    {
        gameManager = GameManager.instance;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

        objectPoolManager = ObjectPoolManager.instance;

    }

    public void Run()
    {
        spriteRenderer.color = gameManager.GetSplashColor();
        gameObject.SetActive(true);
        Color newColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
        spriteRenderer.DOColor(newColor, liveTime).OnComplete(() => BackToPool()).SetId(gameObject.GetInstanceID());
    }

    public void BackToPool()
    {
        DOTween.Kill(gameObject.GetInstanceID());
        objectPoolManager?.CollectToPool("splash", gameObject);
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetRotation(Quaternion rot)
    {
        transform.rotation = rot;
    }

   
}
