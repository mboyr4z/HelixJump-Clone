using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorTower : MonoBehaviour
{

    private MeshRenderer midTowerMeshRenderer;

    private GameManager gameManager;
    
    void Start()
    {

        gameManager = GameManager.instance;

        midTowerMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();

        SetAnchorScale();
        SetMidTowerColor();

        Actions.act_replay += SetAnchorScale;
        Actions.act_nextLevel += SetAnchorScale;

        Actions.act_nextLevel += SetMidTowerColor;
    }

    private void SetMidTowerColor()
    {
        midTowerMeshRenderer.material.color = gameManager.GetTowerColor();
    }

    private void SetAnchorScale()
    {
        int level = GameManager.instance.CurrentLevel;
        transform.localScale = new Vector3(1,10 + (level * 0.8f),1);
    }
}
