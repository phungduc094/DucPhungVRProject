using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    /*[SerializeField] private Transform center;
    [SerializeField] private Transform[] checkPoints;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach(Transform t in checkPoints)
            {
                Vector3 point = center.position;
                Vector3 startLine = t.transform.position;
                Vector3 endLine = t.transform.position + t.right;

                Debug.Log(HandleUtility.DistancePointLine(point, startLine, endLine));
            }
        }
    }*/

    private void Calculate()
    {
        /*Vector3 startingPoint = center.position;
        foreach (Transform t in checkPoints)
        {
            Vector3 direction = t.right;


            Ray ray = new Ray(startingPoint, direction);
            float distance = Vector3.Cross(ray.direction, point - ray.origin).magnitude;
        }*/

        
    }
}

