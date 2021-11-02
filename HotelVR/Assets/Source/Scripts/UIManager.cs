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
            SetUp();
        }
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    [SerializeField] private Transform menuCanvas;
    [SerializeField] private Transform homeCanvas;

    private void SetUp()
    {
        foreach(Transform child in menuCanvas)
        {
            Panel p = child.GetComponent<Panel>();
            if (p != null)
            {
                p.Init();
            }
        }
    }

    private void ClearUI()
    {
        LoadingUI.instance.Deactive();
        LoginUI.instance.Deactive();
        RegisterUI.instance.Deactive();
    }

    public void ShowLoadingScreen()
    {
        LoadingUI.instance.Active();
    }

    public void ShowLoginScreen()
    {
        ClearUI();
        LoginUI.instance.SetUp();
    }

    public void ShowRegisterScreen()
    {
        ClearUI();
        RegisterUI.instance.Active();
    }

    public void ShowMenuScreen()
    {
        ClearUI();
        MenuUI.instance.Active();
    }
}
