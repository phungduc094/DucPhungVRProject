using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IObject
{
    public static Player instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    [SerializeField] private ObjectType objType;

    public ObjectType GetObjectType()
    {
        return objType;
    }
}
