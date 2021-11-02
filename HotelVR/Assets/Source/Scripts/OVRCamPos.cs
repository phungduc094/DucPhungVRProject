using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRCamPos : MonoBehaviour
{
    [SerializeField] private Transform ovrCam;
    [SerializeField] private Transform[] camPositions;

    public static OVRCamPos instance;

    private void Awake()
    {
        if (instance = null)
        {
            instance = this;
        }

        SetCamPosition(0);
    }

    public void SetCamPosition(int index)
    {
        ovrCam.position = camPositions[index].position;
        ovrCam.rotation = camPositions[index].rotation;
    }
}