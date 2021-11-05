using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStatic : MonoBehaviour, IObject
{
    [SerializeField] private ObjectType objType;

    public ObjectType GetObjectType()
    {
        return objType;
    }
}