using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPosition : MonoBehaviour, IDestination
{
    [SerializeField] private ObjectType objTarget;
    [SerializeField] private LayerMask interactable = 1 << 8;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private GameObject target;

    float radius;
    private void Start()
    {
        radius = sphereCollider.radius;
        if (target != null)
        target.SetActive(false);
    }

    bool isCameHere;
    private void Update()
    {
        Collider[] cols = Physics.OverlapSphere(this.transform.position, radius, interactable);
        if (cols.Length > 0)
        {
            foreach(Collider c in cols)
            {
                IObject obj = c.GetComponent<IObject>();
                if (obj != null && obj.GetObjectType() == objTarget)
                {
                    isCameHere = true;
                    return;
                }
            }

            isCameHere = false;
        }
        else
        {
            isCameHere = false;
        }
    }

    public void CheckCame(int index)
    {
        if (isCameHere)
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
            if (isShow)
                HideDestination();
        }
        else
            LessonController.instance.practiceCurrent.checkConditions[index] = false;
    }

    bool isShow = false;
    public void ShowDestination()
    {
        isShow = true;
        target.SetActive(true);
    }

    public void HideDestination()
    {
        isShow = false;
        target.SetActive(false);
    }
}