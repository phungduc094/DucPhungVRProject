using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkController : MonoBehaviour
{
    private Dictionary<ObjectType, List<Item>> allObjectInRoom = new Dictionary<ObjectType, List<Item>>();

    public static MarkController instance;

    private void Awake()
    {
        instance = this;
        Init();
    }

    public void Init()
    {
        foreach(ObjectType type in System.Enum.GetValues(typeof(ObjectType)))
        {
            List<Item> newList = new List<Item>();
            allObjectInRoom.Add(type, newList);
        }
    }

    private void AddItem(Item item)
    {
        allObjectInRoom[item.GetObjectType()].Add(item);
    }

    [Header("References to Get Items on Table")]
    [SerializeField] private Transform tableCenter;
    [SerializeField] private SphereCollider sphereCollider;
    [SerializeField] private LayerMask interactable;

    [Header("References to mark")]
    [SerializeField] private float deviationAngle;
    [SerializeField] private float deviationPosition;
    [SerializeField] private float distanceStandard;
    [SerializeField] private float distanceDeviation = 0.05f;

    [Header("References to check full Items")]
    [SerializeField] private ObjectOn[] objectOns;

    // Get All Items on Table
    public void GetItemsOnTable()
    {
        Collider[] cols = Physics.OverlapSphere(tableCenter.position, sphereCollider.radius, interactable);
        if (cols.Length > 0)
        {
            foreach(Collider c in cols)
            {
                Item item = c.GetComponent<Item>();
                if (item != null) AddItem(item);
            }
        }
    }

    // DONE
    // check amount item require
    public bool CheckFullItems()
    {
        foreach(ObjectOn obj in objectOns)
        {
            int amountFact = allObjectInRoom[obj.objType].Count;

            if (amountFact < obj.amount)
            {
                Debug.Log("Miss Item");
                return false;
            }
            else if (amountFact > obj.amount)
            {
                Debug.Log("Over amount Item");
                return false;
            }
        }

        return true;
    }

    // DONE
    public bool ChairDirectionMark()
    {
        // Get Table item
        Item table = allObjectInRoom[ObjectType.Ban][0];

        // check angle each chair with table
        foreach(Item chair in allObjectInRoom[ObjectType.Ghe])
        {
            Vector3 chairForward = chair.GetTransformForward();
            chairForward.y = 0f;

            Vector3 towardTable = (table.transform.position - chair.transform.position).normalized;
            towardTable.y = 0f;

            float angle = Vector3.Angle(chairForward, towardTable);
            if (angle > deviationAngle) return false;
        }

        return true;
    }
    
    // DONE
    public bool KnifeMainDirectionMark()
    {
        foreach(Item knife in allObjectInRoom[ObjectType.DaoAnChinh])
        {
            // find chair: distance min with chair
            Item closestChair = GetItemClosest(allObjectInRoom[ObjectType.Ghe], knife);

            // angle with chair
            Vector3 knifeForward = knife.GetTransformForward();
            knifeForward.y = 0f;

            Vector3 chairForward = closestChair.GetTransformForward();
            chairForward.y = 0f;

            float angle = Vector3.Angle(knifeForward, chairForward);
            if (angle < -deviationAngle || angle > deviationAngle) return false;
        }

        return true;
    }

    // DONE
    public bool CoverPositionMark()
    {
        Vector3 coverPos = allObjectInRoom[ObjectType.Ban][0].transform.position;
        coverPos.y = 0f;

        Vector3 footTable = allObjectInRoom[ObjectType.ChanBan][0].transform.position;
        footTable.y = 0f;

        float distance = Vector3.Distance(coverPos, footTable);

        if (distance >= -deviationPosition && distance <= deviationPosition) return true;

        return false;
    }

    private Item GetItemClosest(List<Item> items, Item originItem)
    {
        float distance = Mathf.Infinity;
        Item result = null;
        foreach(Item item in items)
        {
            float dis = Vector3.Distance(originItem.transform.position, item.transform.position);
            if (dis < distance)
            {
                distance = dis;
                result = item;
            }
        }

        return result;
    }

    // DONE
    public bool GobletDirectionMark()
    {
        foreach(Item goblet in allObjectInRoom[ObjectType.LyRuou])
        {
            // find: knifeMain closest
            Item knifeClosest = GetItemClosest(allObjectInRoom[ObjectType.DaoAnChinh], goblet);

            // angle with knifeMain
            Vector3 knifeForward = knifeClosest.GetTransformForward();
            knifeForward.y = 0f;

            Vector3 towardGoblet = (goblet.transform.position - knifeClosest.transform.position).normalized;
            towardGoblet.y = 0f;

            float angle = Vector3.Angle(knifeForward, towardGoblet);
            if (angle < -deviationAngle || angle > deviationAngle) return false;
        }

        return true;
    }

    private bool CheckDistanceWithAEdge(List<Item> checkItems, Item originItem)
    {
        Transform[] edgePoints = originItem.GetEdgePoints();

        foreach(Item item in checkItems)
        {
            float distanceWithEdge = GetMinDistance(edgePoints, item.GetCheckPoint());
            if (distanceWithEdge > distanceStandard + distanceStandard * distanceDeviation) return false;
        }

        return true;
    }

    // DONE
    public bool KnifeMainDistanceMark()
    {
        List<Item> knifes = allObjectInRoom[ObjectType.DaoAnChinh];
        Item table = allObjectInRoom[ObjectType.Ban][0];

        if (CheckDistanceWithAEdge(knifes, table)) return true;

        return false;
    }

    // DONE
    public bool DiskDistanceMark()
    {
        List<Item> disks = allObjectInRoom[ObjectType.DiaBB];
        float diskRadius = disks[0].GetRadius();

        Item table = allObjectInRoom[ObjectType.Ban][0];
        foreach(Item d in disks)
        {
            float distance = GetMinDistance(table.GetEdgePoints(), d.transform.position);
            if (distance - diskRadius > distanceStandard + distanceStandard * distanceDeviation) return false;
        }

        return true;
    }

    //DONE
    public bool PlateMainDirectionMark()
    {
        foreach (Item plate in allObjectInRoom[ObjectType.DiaAnChinh])
        {
            // find chair: distance min with chair
            Item closestChair = GetItemClosest(allObjectInRoom[ObjectType.Ghe], plate);

            // angle with chair
            Vector3 plateForward = plate.GetTransformForward();
            plateForward.y = 0f;

            Vector3 chairForward = closestChair.GetTransformForward();
            chairForward.y = 0f;

            float angle = Vector3.Angle(plateForward, chairForward);
            Debug.Log(angle);
            if (angle < -deviationAngle || angle > deviationAngle) return false;
        }

        return true;
    }

    private float GetMinDistance(Transform[] edgePoints, Vector3 checkPoint)
    {
        Vector3 point = checkPoint;
        point.y = 0f;

        float minDistance = Mathf.Infinity;
        foreach(Transform target in edgePoints)
        {
            Vector3 lineStart = target.position;
            lineStart.y = 0f;

            Vector3 lineEnd = target.position + target.right;
            lineEnd.y = 0f;

            float distance = HandleUtility.DistancePointLine(point, lineStart, lineEnd);

            if (distance < minDistance) minDistance = distance;
        }

        return minDistance;
    }

    //DONE
    public bool KnifeBBDistanceMark()
    {
        List<Item> knifeBBs = allObjectInRoom[ObjectType.DaoBB];
        foreach(Item k in knifeBBs)
        {
            Item diskClosest = GetItemClosest(allObjectInRoom[ObjectType.DiaBB], k);

            Vector3 disk = diskClosest.transform.position;
            disk.y = 0f;

            Vector3 knife = k.GetCheckPoint();
            knife.y = 0f;

            float distance = diskClosest.GetRadius() - Vector3.Distance(knife, disk);
            if (distance > distanceStandard + distanceStandard * distanceDeviation) return false;
        }

        return true;
    }

    // DONE
    public bool GobletDistanceMark()
    {
        List<Item> knifes = allObjectInRoom[ObjectType.DaoAnChinh];
        float gobletRadius = allObjectInRoom[ObjectType.LyRuou][0].GetRadius();

        foreach (Item goblet in allObjectInRoom[ObjectType.LyRuou])
        {
            float distance = MinDistanceWithPoints(knifes, goblet) - gobletRadius;

            if (distance > distanceStandard + distanceStandard * distanceDeviation) return false;
        }

        return true;
    }

    private float MinDistanceWithPoints(List<Item> checkItem, Item origin)
    {
        float minDistance = Mathf.Infinity;

        Vector3 originPoint = origin.transform.position;
        originPoint.y = 0f;

        foreach(Item item in checkItem)
        {
            foreach(Transform t in item.GetEdgePoints())
            {
                Vector3 target = t.transform.position;
                target.y = 0f;

                float distance = Vector3.Distance(originPoint, target);
                if (distance < minDistance) minDistance = distance;
            }
        }

        return minDistance;
    }

    const float straightAngle = 180f;
    public bool GobletAndVaseStraight()
    {
        List<Item> goblets = allObjectInRoom[ObjectType.LyRuou];

        if (goblets.Count % 2 != 0)
        {
            return false;
        }

        bool[] gobletPairs = new bool[goblets.Count / 2];
        int index = 0;
        for (int i = 0; i < gobletPairs.Length; i++) gobletPairs[i] = false;

        Item vase = allObjectInRoom[ObjectType.LoHoa][0];
        for(int i = 0; i < goblets.Count - 1; i++)
        {
            Vector3 towardGoblet1 = (vase.transform.position - goblets[i].transform.position).normalized;
            towardGoblet1.y = 0f;

            for(int j = i + 1; j < goblets.Count; j++)
            {
                Vector3 towardGoblet2 = (vase.transform.position - goblets[j].transform.position).normalized;
                towardGoblet2.y = 0f;

                float angle = Vector3.Angle(towardGoblet1, towardGoblet2);
                if (angle >= straightAngle - deviationAngle)
                {
                    gobletPairs[index] = true;
                    index++;
                }
            }
        }

        for(int i = 0; i < gobletPairs.Length; i++)
        {
            if (gobletPairs[i] == false) return false;
        }

        return true;
    }
}