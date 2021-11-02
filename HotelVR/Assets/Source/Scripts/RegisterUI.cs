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
        FirebaseManager.instance.RegisterButton(emailRegisterField.text, passwordRegisterField.text, passwordRegisterVerifyField.text, usernameRegisterField.text);
    }

    public void RegisterBackBtn()
    {
        UIManager.instance.ShowLoginScreen();
    }

    const float y = -500f;
    public void OnSelectField(int index)
    {
        if (index == 0)
        {
            InputManager.instance.SelectInputField(usernameRegisterField);
        }
        else if (index == 1)
        {
            InputManager.instance.SelectInputField(emailRegisterField);
        }
        else if (index == 2)
        {
            InputManager.instance.SelectInputField(passwordRegisterField);
        }
        else
        {
            InputManager.instance.SelectInputField(passwordRegisterVerifyField);
        }

        KeyBoardUI.instance.SetUp(y);
    }
}