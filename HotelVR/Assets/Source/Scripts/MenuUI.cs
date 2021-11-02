using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : Panel
{
    public static MenuUI instance;

    public override void Init()
    {
        instance = this;
    }

    public void MenuBackButton()
    {
        LoginUI.instance.SetUp();
        Deactive();
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}