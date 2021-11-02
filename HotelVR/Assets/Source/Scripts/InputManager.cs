using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    TMP_InputField inputField;
    string inputValue;
    public void SelectInputField(TMP_InputField inputField)
    {
        this.inputField = inputField;
        inputValue = inputField.text;
    }

    public void InsertKey(string value)
    {
        inputValue += value;
        inputField.text = inputValue;
    }

    public void FunctionKey(KeyType type)
    {
        if (type == KeyType.Del)
        {
            inputValue = inputValue.Substring(0, inputValue.Length - 1);
            inputField.text = inputValue;
        }
        else if (type == KeyType.Space)
        {
            inputValue += " ";
            inputField.text = inputValue;
        }
    }
}
