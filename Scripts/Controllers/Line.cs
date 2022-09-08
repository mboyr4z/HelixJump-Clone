using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour, IInteractable
{

    private GameManager gameManager;

    private LineCategory lineCategory;

    private ObjectPoolManager objectPoolManager;

    private BallController ball;

    private void Start()
    {
        gameManager = GameManager.instance;
        objectPoolManager = ObjectPoolManager.instance;
        ball = BallController.instance;
    }


    public void SetMaterial(Material mat)
    {
        GetComponent<MeshRenderer>().material = mat;
    }

    public void SendHeightAndCreateSplash()
    {
        GameObject newSplash = objectPoolManager.SpawnFromPool("splash", Vector3.zero, Quaternion.identity);
        newSplash.GetComponent<Splash>().SetPosition(new Vector3(ball.transform.position.x, transform.position.y + 0.11f, ball.transform.position.z));
        newSplash.GetComponent<Splash>().SetRotation(Quaternion.Euler(-90, 0, 0));
        newSplash.transform.SetParent(transform.parent);
        newSplash.GetComponent<Splash>().Run();
    }

    public void SetCategory(LineCategory lineCategory)
    {
        this.lineCategory = lineCategory;
    }

    public void Interact(Action<LineCategory> action, bool isBallInComboMode)
    {
        if (isBallInComboMode)
        {
            gameManager.IncreaseScore();
            Actions.InvokeAction(Actions.act_TouchedScoreTrigger);
            SmashCircle();
            action.Invoke(LineCategory.good);
        }
        else
        {
            if (lineCategory == LineCategory.good)
            {
                SendHeightAndCreateSplash();
            }
            action.Invoke(lineCategory);     // send category as parameter on Ball Class
        }
    }

    public void SmashCircle()
    {
        transform.parent.parent.GetComponent<Circle>().Smash();
    }

    public void CloseCollider()
    {
        GetComponent<MeshCollider>().enabled = false;
    }
}

