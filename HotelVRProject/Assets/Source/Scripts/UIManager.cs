using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SetUp()
    {
        foreach(Transform child in this.transform)
        {
            Panel p = child.GetComponent<Panel>();
            if (p != null)
            {
                p.Init();
            }
        }
    }

    public void ShowLoginScreen()
    {
        RegisterUI.instance.Deactive();
        LoginUI.instance.Active();
    }

    public void ShowRegisterScreen()
    {
        LoginUI.instance.Deactive();
        RegisterUI.instance.Active();
    }
}
