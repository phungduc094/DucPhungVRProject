using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RememberAccBtn : MonoBehaviour, IButton
{
    private bool isRemember;

    public void OnTouch()
    {
        isRemember = !isRemember;
        PrefInfo.SetAutoLogin(isRemember == true ? 1 : 0);
        checkImage.SetActive(isRemember);

        KeyBoardUI.instance.Deactive();
    }

    public void SetUp()
    {
        isRemember = PrefInfo.GetAutoLogin();
        checkImage.SetActive(isRemember);
    }

    [SerializeField] private GameObject checkImage;
}