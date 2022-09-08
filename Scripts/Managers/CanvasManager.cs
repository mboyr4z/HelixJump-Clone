using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    
  

    [SerializeField] private GameObject inGamePanel, loseGamePanel, winGamePanel;

    void Start()
    {
      
        Actions.act_win += ShowWinPanel;
        Actions.act_lose += ShowLoseGamePanel;
    }

 
    private void ShowWinPanel()
    {
        winGamePanel.SetActive(true);
    }


    private void ShowLoseGamePanel()
    {
        loseGamePanel.SetActive(true);
    }
 

   

}
