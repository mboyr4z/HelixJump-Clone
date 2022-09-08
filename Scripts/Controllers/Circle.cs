using System.Collections;
using UnityEngine;
using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;

public class Circle : MonoBehaviour, IDestroyable
{



    [SerializeField] private CircleData circleData;

    [SerializeField] private Material brokenMaterial;

    [SerializeField] private List<FocusPoint> focusPoints;

    [SerializeField] private ScoreTrigger scoreTrigger;



    private MaterialManager materialManager;

    private Tower tower;

    private int id;

    private void Awake()
    {
        materialManager = MaterialManager.instance;
        tower = Tower.instance;
    }

    public void SetId(int id)
    {
        this.id = id;
    }


    public void CreateRandomLine()
    {
        int badLineCount = id == 0 ? 0 : Random.Range((int)circleData.badLineCount.x, (int)circleData.badLineCount.y);
        int emptyLineCount = id == 0 ? 1 : Random.Range((int)circleData.emptyLineCount.x, (int)circleData.emptyLineCount.y);



        for (int i = 0; i < emptyLineCount; i++)
        {
            int focusID = Random.Range(1, focusPoints.Count);
            GameObject willDestroyObject1 = focusPoints[focusID].gameObject;
            GameObject willDestroyObject2 = focusPoints[focusID - 1].gameObject;

            Destroy(willDestroyObject1);
            Destroy(willDestroyObject2);

            focusPoints.RemoveAt(focusID);
            focusPoints.RemoveAt(focusID - 1);

        }

        List<FocusPoint> tempFocusPoints = new List<FocusPoint>(focusPoints);

        for (int i = 0; i < badLineCount; i++)
        {
            int focusID = Random.Range(0, tempFocusPoints.Count);
            tempFocusPoints[focusID].SetMaterialOfLine(materialManager.GetBadMaterial());
            tempFocusPoints[focusID].SetCategoryOfLine(LineCategory.bad);
            tempFocusPoints.RemoveAt(focusID);
        }

        for (int i = 0; i < tempFocusPoints.Count; i++)
        {
            tempFocusPoints[i].SetMaterialOfLine(materialManager.GetGoodMaterial());
            tempFocusPoints[i].SetCategoryOfLine(LineCategory.good);
        }
    }

    public void Smash()
    {
        transform.SetParent(null);
        SetMaterialOfLines();
        scoreTrigger.CloseCollider();

        foreach (FocusPoint focusObject in focusPoints)
        {
            focusObject?.CloseColliderOfLine();
            focusObject?.AddForce(Random.Range(circleData.addForcePower.x, circleData.addForcePower.y));
        }

        StartCoroutine(DestroyCircle(1f));

    }

    private void SetMaterialOfLines()
    {
        foreach (FocusPoint focusObject in focusPoints)
        {
            focusObject?.SetMaterialOfLine(brokenMaterial);
        }
    }

    IEnumerator DestroyCircle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        tower.RemoveDestroyedCircleFromCircleList(transform);
        DestroyObject();
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
