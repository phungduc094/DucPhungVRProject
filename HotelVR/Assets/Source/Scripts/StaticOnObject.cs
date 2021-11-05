using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticOnObject : MonoBehaviour
{
    [SerializeField] private LayerMask objLayer;
    [SerializeField] private ObjectType objType;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private OVRGrabbable grabbable;
    [SerializeField] private Rigidbody rb;

    float radius;
    private void Start()
    {
        radius = sphereCollider.radius;
    }

    private void FixedUpdate()
    {
        Collider[] cols = Physics.OverlapSphere(checkPoint.position, radius, objLayer);
        if (cols.Length > 0 && !grabbable.isGrabbed)
        {
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
        }
    }
}