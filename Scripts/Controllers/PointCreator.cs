using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointCreator : MonoSingleton<PointCreator>
{
    [SerializeField] private GameObject textPrefab;

    [SerializeField] private Transform ReachPoint;
    

    public void CreateIncreaseText(int increasePoint)
    {
        GameObject createdPointText= Instantiate(textPrefab,transform);
        createdPointText.GetComponent<Text>().text = "+" + increasePoint.ToString();

        RectTransform createdPointTextRectTransform = createdPointText.GetComponent<RectTransform>();
        createdPointTextRectTransform.DOMove(ReachPoint.position,0.8f).OnComplete(() => {
            Destroy(createdPointText);
        });
    }
}
