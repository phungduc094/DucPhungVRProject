using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabberManager : MonoBehaviour
{
    public static GrabberManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject objectRelease { get; set; }

    public void OnPickUp(Item item)
    {

    }

    public void OnRelease(Item item)
    {

    }
}
