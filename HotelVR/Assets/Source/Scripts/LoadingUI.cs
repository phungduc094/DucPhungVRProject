using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingUI : Panel
{
    public static LoadingUI instance;

    public override void Init()
    {
        instance = this;
    }

    [SerializeField] private TextMeshProUGUI text;

    public override void Active()
    {
        base.Active();
        StartCoroutine(DoLoading());
    }

    private IEnumerator DoLoading()
    {
        text.text = "Loading.";
        yield return new WaitForSeconds(0.15f);

        text.text = "Loading..";
        yield return new WaitForSeconds(0.15f);

        text.text = "Loading...";
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(DoLoading());
    }

    public override void Deactive()
    {
        StopAllCoroutines();
        base.Deactive();
    }
}