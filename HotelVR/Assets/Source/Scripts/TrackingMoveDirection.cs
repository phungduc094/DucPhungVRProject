using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingMoveDirection : MonoBehaviour, IDestination
{
    [Header("1: Cung Chieu, -1: Nguoc Chieu")]
    [SerializeField] private float direction;
    [SerializeField] private Transform originDirection;
    [SerializeField] private AroundPosition[] positions;
    [SerializeField] private GameObject[] destinations;
    [SerializeField] private float deviation;

    Transform target;
    Vector3 trackingDir;

    private void Start()
    {
        foreach (GameObject obj in destinations) obj.SetActive(false);
    }

    // auto setup when player put first item in lesson 2 on table, using in reality
    public void SetUp()
    {
        this.target = Player.instance.transform;
        trackingDir = (target.position - transform.position).normalized;
        trackingDir.y = 0f;
    }

    // check when grab item, using in reality
    public void CheckDirectionUpdate(int index)
    {
        Vector3 currentDir = (target.position - transform.position).normalized;
        currentDir.y = 0f;
        float cross = Vector3.Cross(trackingDir, currentDir).y;

        trackingDir = currentDir;
        if (cross * direction > 0f)
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
        }
        else
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
        }
    }

    // when practice with oringin hard, using in practice
    public void SetUpHard()
    {
        this.target = Player.instance.transform;
        trackingDir = (originDirection.position - transform.position).normalized;
    }

    // using in practice
    public void CheckDirection(int index)
    {
        Vector3 currentDir = (target.position - transform.position).normalized;
        float cross = Vector3.Cross(trackingDir, currentDir).y;

        if (cross * direction > 0f)
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
        }
        else
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = false;
        }
    }

    float angle;
    public void CheckEnoughAngle(int index)
    {
        Vector3 currentDir = (target.position - transform.position).normalized;
        currentDir.y = 0f;
        angle = Vector3.Angle(trackingDir, currentDir);

        if (angle >= positions[positionIndex].angle - deviation && angle <= positions[positionIndex].angle + deviation)
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
            if (isShow)
                HideDestination();
        }
        else
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = false;
        }
    }

    int positionIndex;
    public void SelectAroundPosition(int index)
    {
        positionIndex = index;
    }

    bool isShow = false;
    public void ShowDestination()
    {
        isShow = true;
        destinations[positionIndex].SetActive(true);
    }

    public void HideDestination()
    {
        isShow = false;
        destinations[positionIndex].SetActive(false);
    }
}

[System.Serializable]
public class AroundPosition
{
    public float dir;
    public float angle;
}
