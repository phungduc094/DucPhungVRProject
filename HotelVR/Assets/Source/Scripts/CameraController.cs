using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            instance.transform.position = this.transform.position;
            instance.transform.rotation = this.transform.rotation;

            Debug.Log("Instance already exists, destroying object!");
            Destroy(this.gameObject);
        }
    }

    public void DeactiveCamUI()
    {
        this.gameObject.SetActive(false);
    }
}
