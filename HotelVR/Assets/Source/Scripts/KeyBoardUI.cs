using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardUI : Panel
{
    public static KeyBoardUI instance;

    public override void Init()
    {
        if (instance == null)
        {
            instance = this;
        }
        SetUp();
    }

    public override void SetUp()
    {
        foreach (KeyBtn k in keyBtns) k.SetUp();
    }

    [SerializeField] private KeyBtn[] keyBtns;
    bool isUpperCase = false;

    public void KeyInput(KeyValue key)
    {
        if (key.type == KeyType.Shift)
        {
            isUpperCase = !isUpperCase;
            foreach (KeyBtn k in keyBtns) k.ToogleKey(isUpperCase);
        }
        else if (key.type == KeyType.Del || key.type == KeyType.Space)
        {
            InputManager.instance.FunctionKey(KeyManager.GetTypeKey(key));
        }
        else if (key.type == KeyType.OK)
        {
            Deactive();
        }
        else
        {
            InputManager.instance.InsertKey(KeyManager.GetKeyValue(key, isUpperCase));
        }
    }

    private void NormalKeyInput()
    {

    }

    [SerializeField] private RectTransform content;
    public void SetUp(float y)
    {
        content.anchoredPosition = new Vector2(0, y);
        Active();
    }
}
