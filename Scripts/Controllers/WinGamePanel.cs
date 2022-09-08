using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGamePanel : MonoBehaviour
{
    private GameManager gameManager;
    private void Start()
    {
        gameManager = GameManager.instance;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            gameManager.NextLevel();
        }
    }
}
