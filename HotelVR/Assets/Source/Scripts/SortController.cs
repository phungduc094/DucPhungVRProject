using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortController : MonoBehaviour, IDestination
{
    [SerializeField] float parentScale;
    [SerializeField] private ObjectSort[] objectSorts;
    [SerializeField] private ObjectType[] destinationTypes;

    private Dictionary<ObjectType, List<Transform>> allObject = new Dictionary<ObjectType, List<Transform>>();
    private void Start()
    {
        foreach(ObjectSort obj in objectSorts)
        {
            List<Transform> newList = new List<Transform>();
            allObject.Add(obj.objType, newList);

            obj.heightWorld = parentScale  * obj.heightLocal * obj.transform.localScale.y;
            obj.transform.gameObject.SetActive(false);
        }
    }

    public void SelectPosition(Item item)
    {
        if (allObject.ContainsKey(item.GetObjectType()) == false) return;

        if (allObject[item.GetObjectType()].Contains(item.transform)) return;

        Debug.Log("Select");
        // check type object sort
        ObjectSort objectSort = GetObjectSort(item.GetObjectType());

        // spawn new position
        if (objectSort.dependence == ObjectDependence.Dependence)
        {
            Vector3 newPosition = objectSort.dependenceTransform.position;

            objectSort.transform.position = newPosition;
            objectSort.transform.gameObject.SetActive(true);
        }
        else
        {
            // newposition = oldPosition + height of object
            Vector3 newPosition = objectSort.transform.position;
            objectSort.transform.position = newPosition;
            objectSort.transform.gameObject.SetActive(true);
        }
    }

    public void LeavePosition(Item item)
    {
        if (allObject.ContainsKey(item.GetObjectType()) == false) return;

        Debug.Log("Leave");
        Debug.Log(item.name);
        ObjectSort obj = GetObjectSort(item.GetObjectType());
        obj.transform.gameObject.SetActive(false);

        List<Transform> objSames = allObject[item.GetObjectType()];
        if (objSames.Contains(item.transform))
        {
            objSames.Remove(item.transform);
            ObjectSort objectSort = GetObjectSort(item.GetObjectType());
            objectSort.transform.position = objectSort.transform.position - objectSort.heightWorld * transform.up;
        }  
    }

    public void CorrectPosition(Item item)
    {
        if (allObject.ContainsKey(item.GetObjectType()) == false) return;

        allObject[item.GetObjectType()].Add(item.transform);

        ObjectSort objectSort = GetObjectSort(item.GetObjectType());
        item.transform.position = objectSort.transform.position;
        item.transform.rotation = objectSort.transform.rotation;

        objectSort.transform.gameObject.SetActive(false);
        objectSort.transform.position = objectSort.transform.position + objectSort.heightWorld * transform.up;

        Debug.Log("call");
    }

    private ObjectSort GetObjectSort(ObjectType type)
    {
        foreach (ObjectSort obj in objectSorts)
        {
            if (obj.objType == type)
            {
                return obj;
            }
        }

        return null;
    }

    public bool CheckObjectRequire(ObjectType objType, int amount)
    {
        if (allObject[objType].Count >= amount) return true;

        return false;
    }

    ObjectType destinationType;
    public void SelectDestination(int destinationIndex)
    {
        destinationType = destinationTypes[destinationIndex];
    }

    public void ShowDestination()
    {
        List<Transform> objectSameType = allObject[destinationType];
        objectSameType[objectSameType.Count - 1].GetComponent<Item>().ShowDestination();
    }

    public void HideDestination()
    {
        Debug.Log("Dont object to hide");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SelectDestination(0);
            ShowDestination();
        }
    }
}

[System.Serializable]
public class ObjectSort
{
    public ObjectType objType;
    public Transform transform;
    public float heightLocal;
    public ObjectDependence dependence;
    public Transform dependenceTransform;
    public float heightWorld;
}

public enum ObjectDependence
{
    Dependence,
    Independence
}