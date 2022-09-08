using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoSingleton<Tower>
{
    [SerializeField] private GameObject circlePreab;

    [SerializeField] private GameObject finishCircle;

    [SerializeField] private float mouseSensitivity;

    [SerializeField] private float rangeHeigt;

    [SerializeField] private float firstCircleY;

    private List<Transform> circles = new List<Transform>();

    private Quaternion startRotation;

    private Vector3 firstMousePos, lastMousePos;

    private float sceneWidth;

    private Material mat1, mat2;

    GameManager gameManager;

    Quaternion firstRotation;

    Vector3 firstEulerAngles;
    

   

    void Start()
    {
        gameManager = GameManager.instance;

        firstRotation = transform.rotation;
        firstEulerAngles = transform.eulerAngles;

        sceneWidth = Screen.width;

        CreateCircles();
        Actions.act_replay += CreateMapsForNewGame;
        Actions.act_nextLevel += CreateMapsForNewGame;

    }

    public void RemoveDestroyedCircleFromCircleList(Transform destroyedCircle)
    {
        circles.Remove(destroyedCircle);
    }

    void CreateMapsForNewGame()
    {

        ResetRotate();
        DestroyCircles();
        ClearCircleList();
        CreateCircles();
    }

    private void ClearCircleList()
    {
        circles.Clear();
    }

    private void DestroyCircles()
    {
        foreach (Transform item in circles)
        {
            item.GetComponent<IDestroyable>()?.DestroyObject();
        }
    }


    public void CreateCircles()
    {
        int level = gameManager.CurrentLevel;
        int i;


        for (i = 0; i < gameManager.GetCircleCount(); i++)
        {
            GameObject createdCircle = Instantiate(circlePreab, Vector3.zero, Quaternion.Euler(0, Random.Range(0, 360), 0));
            createdCircle.GetComponent<Circle>().SetId(i);
            createdCircle.GetComponent<Circle>().CreateRandomLine();
        
            createdCircle.transform.parent = transform;
            createdCircle.transform.localPosition = new Vector3(0, firstCircleY - i * rangeHeigt, 0);

            circles.Add(createdCircle.transform);
        }

        GameObject createdFinishCircle = Instantiate(finishCircle, Vector3.zero, Quaternion.identity);
        createdFinishCircle.transform.parent = transform;
        createdFinishCircle.transform.localPosition = new Vector3(0, firstCircleY - i * rangeHeigt, 0);
        circles.Add(createdFinishCircle.transform);
    }

    void Update()
    {
        if (gameManager.State == GameStates.inGame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstMousePos = Input.mousePosition;
                startRotation = transform.rotation;
            }

            if (Input.GetMouseButton(0))
            {
                lastMousePos = Input.mousePosition;
                RotateTower((lastMousePos.x - firstMousePos.x) * mouseSensitivity);
            }

        }
    }

    private void ResetRotate()
    {
        firstMousePos = Vector3.zero;
        lastMousePos = Vector3.zero;

        transform.rotation = startRotation * Quaternion.Euler(-Vector3.up * (0 / sceneWidth) * 360);

        firstMousePos = Input.mousePosition;
    }

    private void RotateTower(float difX)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, startRotation * Quaternion.Euler(-Vector3.up * (difX / sceneWidth) * 360), 0.1f);

    }
}
