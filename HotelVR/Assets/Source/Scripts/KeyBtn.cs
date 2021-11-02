using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyBtn : MonoBehaviour, IButton
{
    [SerializeField] KeyValue key;
    [SerializeField] private TextMeshProUGUI text;

    public void OnTouch()
    {
        KeyBoardUI.instance.KeyInput(key);
    }

    public void SetUp()
    {
        text.text = key.key1;
    }

    public void ToogleKey(bool isUpperCase)
    {
        if (key.type != KeyType.NormalKey) return;

        if (isUpperCase)
        {
            text.text = key.key2.ToString();
        }
        else
        {
            text.text = key.key1.ToString();
        }
    }
}

[System.Serializable]
public class KeyValue
{
    public KeyType type;
    public string key1;
    public string key2;
}

public enum KeyType
{
    NormalKey,
    Space,
    Del,
    Shift,
    OK
}

public static class KeyManager
{
    public static KeyType GetTypeKey(KeyValue value)
    {
        return value.type;
    }

    public static string GetKeyValue(KeyValue value, bool isUpperCase)
    {
        if (isUpperCase) return value.key2;
        else return value.key1;
    }
}