using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObjectOn : MonoBehaviour
{
    [SerializeField] private ObjectOn[] objectOns;
    [SerializeField] private SortController sortController;

    int objIndex;
    public void SelectObjOn(int index)
    {
        objIndex = index;
    }

    public void CheckFullRequire(int index)
    {
        if (sortController.CheckObjectRequire(objectOns[objIndex].objType, objectOns[objIndex].amount))
            LessonController.instance.practiceCurrent.checkConditions[index] = true;
        else
            LessonController.instance.practiceCurrent.checkConditions[index] = false;
    }
}

[System.Serializable]
public class ObjectOn
{
    public ObjectType objType;
    public int amount;
}