using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IObject, IDestination
{
    [SerializeField] private ObjectType objType;
    [SerializeField] private OVRGrabbable grabbable;
    [SerializeField] private GameObject target;
    [SerializeField] private CheckOnSomething checkOnSomething;
    [SerializeField] private Transform myForward;
    [SerializeField] private Transform checkPoint;
    [SerializeField] private Transform[] edgePoints;
    [SerializeField] private SphereCollider radiusCollider;

    private void Start()
    {
        if (target != null)
        target.SetActive(false);
    }

    public void SetGrabbale(bool isActive)
    {
        if (grabbable == null || grabbable.isGrabbed) return;

        grabbable.enabled = isActive;
    }

    public void isGrabbed(int index)
    {
        if (grabbable.isGrabbed)
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

    public bool isGrabble()
    {
        return grabbable.isGrabbed;
    }

    public ObjectType GetObjectType()
    {
        return objType;
    }

    [SerializeField] private LayerMask positionLayer;

    bool isTrigger = false;
    SortController sortController = null;
    private void OnTriggerEnter(Collider other)
    {
        if (0 != (positionLayer.value & 1<<other.gameObject.layer) && !isTrigger && grabbable.isGrabbed)
        {
            isTrigger = true;

            sortController = other.GetComponent<SortController>();
            sortController.SelectPosition(objType);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (0 != (positionLayer.value & 1 << other.gameObject.layer) && isTrigger && grabbable.isGrabbed)
        {
            isTrigger = false;

            sortController.LeavePosition(this);
        }
    }

    public void PickUp()
    {
        // reset state
    }

    public void Release()
    {
        // check in correct position
        if (isTrigger)
            sortController.CorrectPosition(this);

        CallBackWhenRelease();
    }

    private void CallBackWhenRelease()
    {
        if (checkOnSomething == null) return;

        // check be constraintY
        if (checkOnSomething.CheckObjectContrainst())
            grabbable.constraintY = true;
        else
            StartCoroutine(TryCheckConstraint());

        // check is child of other object
        if (checkOnSomething.CheckIsChild())
            checkOnSomething.CheckIsChild();
        else
            StartCoroutine(TryCheckChild());
    }

    float timeDelay = 2f;
    private IEnumerator TryCheckConstraint()
    {
        yield return new WaitForSeconds(timeDelay);
        if (checkOnSomething.CheckObjectContrainst()) grabbable.constraintY = true;
    }

    private IEnumerator TryCheckChild()
    {
        yield return new WaitForSeconds(timeDelay);
        checkOnSomething.CheckIsChild();
    }

    [SerializeField] private bool isRelease;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && isRelease)
        {
            isRelease = false;
            Release();
        }
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

    public bool CheckOnObj(ObjectType objectType)
    {
        if (checkOnSomething != null && checkOnSomething.CheckOnObj(objectType)) return true;

        return false;
    }

    public Vector3 GetTransformForward()
    {
        return myForward.forward;
    }

    public Vector3 GetCheckPoint()
    {
        return checkPoint.position;
    }

    public Transform[] GetEdgePoints()
    {
        return edgePoints;
    }

    public float GetRadius()
    {
        return radiusCollider.radius * transform.localScale.x;
    }
}

[System.Serializable]
public enum ObjectType
{
    Player,
    Khay,
    LoMuoi,
    LoTieu,
    LoHoa,
    DiaBB,
    DaoAnChinh,
    DiaAnChinh,
    LyVang,
    Khan,
    DaoBB,
    Ban,
    Nothing,
    LyRuou,
    Ghe,
    ChanBan
}

public interface IObject
{
    ObjectType GetObjectType();
}

public interface IDestination
{
    void ShowDestination();
    void HideDestination();
}