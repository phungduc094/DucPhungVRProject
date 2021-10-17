using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginUI : Panel
{
    public static LoginUI instance;

    public override void Init()
    {
        instance = this;
    }

    [SerializeField] private TMP_InputField emailLoginField;
    [SerializeField] private TMP_InputField passwordLoginField;

    public override void SetUp()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";

        Active();
    }

    //Function for the login button
    public void LoginButton()
    {
        AuthManager.instance.Login(emailLoginField.text, passwordLoginField.text);
    }

    public void RegisterButton()
    {
        UIManager.instance.ShowRegisterScreen();
    }
}