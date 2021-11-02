using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefInfo
{
    // auto login
    public static bool GetAutoLogin() { return PlayerPrefs.GetInt("AutoLogin", 0) == 0 ? false : true; }
    public static void SetAutoLogin(int value) { PlayerPrefs.SetInt("AutoLogin", value); }
}