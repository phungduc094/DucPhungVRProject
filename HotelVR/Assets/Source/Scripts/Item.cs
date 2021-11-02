using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int[] id;
    [SerializeField] private bool isCheckOn;
    [SerializeField] private OVRGrabbable grabbable;

    public void SetGrabbale(bool isActive)
    {
        grabbable.enabled = isActive;
    }

    public void isGrabbed(int index)
    {
        if (grabbable.isGrabbed)
            PracticeController.instance.checkConditions[index] = true;
        else
            PracticeController.instance.checkConditions[index] = false;
    }
}
