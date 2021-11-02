using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonButton : MonoBehaviour, IButton
{
    public void OnTouch()
    {
        MenuUI.instance.Deactive();
        SelectActivitiesUI.instance.SetUp();
    }

    public void SetUp()
    {
        Debug.Log("Lesson Set up");
    }
}

public interface IButton
{
    void OnTouch();
    void SetUp();
}