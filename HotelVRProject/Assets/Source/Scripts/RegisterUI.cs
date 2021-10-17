using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterUI : Panel
{
    public static RegisterUI instance;

    public override void Init()
    {
        instance = this;
    }

    // Register Variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_InputField emailRegisterField;
    public TMP_InputField passwordRegisterField;
    public TMP_InputField passwordRegisterVerifyField;

    public override void SetUp()
    {
        usernameRegisterField.text = "";
        emailRegisterField.text = "";
        passwordRegisterField.text = "";
        passwordRegisterVerifyField.text = "";

        Active();
    }

    public void RegisterButton()
    {
        AuthManager.instance.RegisterButton(emailRegisterField.text, passwordRegisterField.text, passwordRegisterVerifyField.text, usernameRegisterField.text);
    }
}