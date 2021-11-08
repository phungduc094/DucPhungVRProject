using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOnSomething : MonoBehaviour, IDestination
{
    [Header("Commom References")]
    [SerializeField] private Transform checkPoint;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private LayerMask layer;

    [Space(10)]
    [Header("Objects that can choose on it")]
    [SerializeField] private ObjectType[] objectToOnIts;

    [Space(10)]
    [Header("Object that can be constraint")]
    [SerializeField] private ObjectType objectConstraint;

    [Space(10)]
    [Header("Object condition before is child of other object")]
    [SerializeField] private ObjectType objectCondition;
    [SerializeField] private ObjectType objectParent;

    [Header("Check be Dropped")]
    [SerializeField] private LayerMask groundLayer;

    public bool isOnObj { get; set; }

    float radius;
    private void Start()
    {
        radius = sphereCollider.radius;
    }

    private void Update()
    {
        if (!isSetup) return;

        if (Physics.OverlapSphere(checkPoint.position, radius, groundLayer).Length > 0)
        {
            LessonController.instance.LessonFail();
            this.enabled = false;
            return;
        }
        
        Collider[] cols = Physics.OverlapSphere(checkPoint.position, radius, layer);
        if (cols.Length > 0)
        {
            foreach(Collider c in cols)
            {
                IObject obj = c.GetComponent<IObject>();
                if (obj != null && obj.GetObjectType() == objTarget)
                {
                    isOnObj = true;
                    return;
                }
            }
            isOnObj = false;
        }
        else
        {
            isOnObj = false;
        }
    }

    bool isSetup = false;
    ObjectType objTarget;
    public void SelectObj(int index)
    {
        if (!isSetup) isSetup = true;
        objTarget = objectToOnIts[index];
    }

    public void CheckOnObj(int index)
    {
        if (isOnObj)
        {
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
            if (isShow)
            {
                HideDestination();
            }
        }
        else
            LessonController.instance.practiceCurrent.checkConditions[index] = false;
    }

    public bool CheckObjectContrainst()
    {
        if (objectConstraint == ObjectType.Nothing) return false;

        Collider[] cols = Physics.OverlapSphere(checkPoint.position, radius, layer);
        if (cols.Length > 0)
        {
            foreach(Collider c in cols)
            {
                IObject obj = c.GetComponent<IObject>();
                if (obj != null && obj.GetObjectType() == objectConstraint) return true;
            }
        }

        return false;
    }

    public bool CheckOnObj(ObjectType objectType)
    {
        Collider[] cols = Physics.OverlapSphere(checkPoint.position, radius, layer);
        if (cols.Length > 0)
        {
            foreach (Collider c in cols)
            {
                IObject obj = c.GetComponent<IObject>();
                if (obj != null && obj.GetObjectType() == objectType) return true;
            }
        }

        return false;
    }

    public bool CheckIsChild()
    {
        if (objectCondition == ObjectType.Nothing) return false;

        Collider[] cols = Physics.OverlapSphere(checkPoint.position, radius, layer);
        if (cols.Length > 0)
        {
            foreach(Collider c in cols)
            {
                Item item = c.GetComponent<Item>();
                // select object parent and check object parent is satisfy condition
                if (item != null && item.GetObjectType() == objectParent && item.CheckOnObj(objectCondition))
                {
                    this.transform.parent = item.transform;
                    return true;
                }
            }
        }
        return false;
    }

    bool isShow;
    [SerializeField] private GameObject target;
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