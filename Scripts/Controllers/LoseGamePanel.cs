using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseGamePanel : MonoBehaviour
{
    [SerializeField] private Text rateText;


    private void OnEnable()
    {
        int brokenCircle = GameManager.instance.Score;
        int totalCircle = GameManager.instance.CurrentLevel + 10;
        int ratePercent = (int) (((float)brokenCircle / (float)totalCircle) * 100);
        SetRateText(ratePercent);
    }

    

    private void SetRateText(int ratePercent)
    {
        rateText.text = ratePercent.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Actions.InvokeAction(Actions.act_replay);
            gameObject.SetActive(false);
        }
    }

}
