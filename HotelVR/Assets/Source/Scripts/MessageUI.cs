using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageUI : Panel
{
    public static MessageUI instance;

    public override void Init()
    {
        instance = this;
    }

    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI confirmText;

    public void ShowWarning(string mess)
    {
        warningText.text = mess;
        confirmText.text = "";

        Active();
        StartCoroutine(DoFadeHide());
    }

    public void ShowConfirm(string mess)
    {
 
        warningText.text = "";
        confirmText.text = mess;

        Active();
        StartCoroutine(DoFadeHide());
    }

    private IEnumerator DoFadeHide()
    {
        yield return new WaitForSeconds(3f);
        Deactive();
    }
}