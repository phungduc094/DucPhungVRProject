using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teacher : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    public void PlayBowAnim()
    {
        anim.SetTrigger("bowTrigger");
    }

    public void PlayIdleAnim()
    {
        anim.SetTrigger("idleTrigger");
    }
}