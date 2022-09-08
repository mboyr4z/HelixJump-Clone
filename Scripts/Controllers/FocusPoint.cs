using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusPoint : MonoBehaviour
{

    [SerializeField] private Line line;

    private void Start()
    {
        line = transform.GetChild(0).GetComponent<Line>();
    }

    public void AddForce(float force)
    {
        if (TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.AddForce(transform.right * force + new Vector3(0, force / 5, 0));
        }
        else
        {
            gameObject.AddComponent<Rigidbody>().AddForce(transform.right * force);
        }

    }

    public void SetMaterialOfLine(Material mat)
    {
        line.SetMaterial(mat);
    }

    public void SetCategoryOfLine(LineCategory lineCategory)
    {
        line.SetCategory(lineCategory);
    }

    public void CloseColliderOfLine()
    {
        line.CloseCollider();
    }

    private bool quitting = false;

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void OnDestroy()
    {
        if(!quitting)
            foreach (Transform item in transform)
            {
                item.GetComponent<IPoolObject>()?.BackToPool();
            }
    }
}
