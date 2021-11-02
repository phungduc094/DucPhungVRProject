using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacable : MonoBehaviour
{
    public bool isHolder;
    
    public void Holding()
    {
        isHolder = true;
    }

    public void Release()
    {
        isHolder = false;
    }

}