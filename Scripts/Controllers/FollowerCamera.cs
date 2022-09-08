using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    private Vector3 firstPos;

    private Vector3 offset;


    private void Start()
    {
        SetFirstPos(transform.position);

        Actions.act_replay += MoveFirstPos;
        Actions.act_nextLevel += MoveFirstPos;

        SetOffset(target.position - transform.position);
    }

    private void MoveFirstPos()
    {
        transform.position = firstPos;
    }


    private void Update()
    {
        MoveCamera();
        
    }

    private void MoveCamera()
    {
        Vector3 targetPointOnScreen = Camera.main.WorldToViewportPoint(target.transform.position);

        if (targetPointOnScreen.y < 0.6)
        {
            transform.position = Vector3.Slerp(transform.position, target.position - offset, 0.1f);
        }
    }

    private void SetFirstPos(Vector3 pos)
    {
        firstPos = pos;
    }

    private void SetOffset(Vector3 offsetValue)
    {
        offset = offsetValue;
    }
}
