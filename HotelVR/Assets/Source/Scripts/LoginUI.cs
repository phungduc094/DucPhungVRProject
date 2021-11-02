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
    [SerializeField] private RememberAccBtn rememberAcc;

    public override void SetUp()
    {
        emailLoginField.text = "";
        passwordLoginField.text = "";

        rememberAcc.SetUp();

        Active();
    }

    //Function for the login button
    public void LoginButton()
    {
        FirebaseManager.instance.Login(emailLoginField.text, passwordLoginField.text);
        KeyBoardUI.instance.Deactive();
    }

    public void RegisterButton()
    {
        UIManager.instance.ShowRegisterScreen();
        KeyBoardUI.instance.Deactive();
    }

    const float y = -500f;
    public void OnSelectField(int index)
    {
        if (index == 0)
        {
            InputManager.instance.SelectInputField(emailLoginField);
        }
        else
        {
            InputManager.instance.SelectInputField(passwordLoginField);
        }

        KeyBoardUI.instance.SetUp(y);
    }
}
  