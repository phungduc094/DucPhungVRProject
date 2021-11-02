using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCameMyPosition : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private SphereCollider sphereCollider;

    float radius;
    private void Start()
    {
        radius = sphereCollider.radius;
    }

    bool isCameHere;
    private void Update()
    {
        if (Physics.OverlapSphere(this.transform.position, radius, targetLayer).Length > 0)
        {
            isCameHere = true;
        }
        else
        {
            isCameHere = false;
        }
    }

    public void CheckCame(int index)
    {
        if (isCameHere)
            PracticeController.instance.checkConditions[index] = true;
        else
            PracticeController.instance.checkConditions[index] = false;
    }
}