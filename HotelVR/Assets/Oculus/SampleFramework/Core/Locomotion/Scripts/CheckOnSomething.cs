using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnSomething : MonoBehaviour
{
    [SerializeField] private OnSomething[] onSomethings;

    public bool isOnSomething { get; set; }

    private void Start()
    {
        foreach(OnSomething os in onSomethings)
        {
            os.radius = os.sphereCollider.radius;
        }

        SelectSomething(0);
    }

    private void Update()
    {
        if (Physics.OverlapSphere(checkPoint.position, radius, layer).Length > 0)
        {
            isOnSomething = true;
        }
        else
        {
            isOnSomething = false;
        }
    }

    Transform checkPoint;
    float radius;
    LayerMask layer;
    public void SelectSomething(int index)
    {
        checkPoint = onSomethings[index].checkPoint;
        radius = onSomethings[index].radius;
        layer = onSomethings[index].checkLayer;
    }

    public void OnSomething(int index)
    {
        if (isOnSomething)
            PracticeController.instance.checkConditions[index] = true;
        else
            PracticeController.instance.checkConditions[index] = false;
    }
}

[System.Serializable]
public class OnSomething
{
    public Transform checkPoint;
    public LayerMask checkLayer;
    public SphereCollider sphereCollider;
    public float radius;
}