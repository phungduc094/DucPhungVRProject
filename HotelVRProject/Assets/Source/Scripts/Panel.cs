using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [SerializeField] protected Animator anim;

    public virtual void Init() { }
    public virtual void SetUp() { }
    public virtual void Active() 
    {
        this.gameObject.SetActive(true);
    }
    public virtual void Show() { }
    public virtual void Deactive()
    {
        this.gameObject.SetActive(false);
    }
    public virtual void Hide() 
    {
        anim.SetTrigger("hide");
    }
}
