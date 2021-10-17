using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonButton : MonoBehaviour, IButton
{
    [SerializeField] private Lesson lesson;

    public void OnTouch()
    {
        LessonManager.instance.SetUp(lesson);
        MenuUI.instance.Deactive();
        SelectActivitiesUI.instance.SetUp();
    }
}

public interface IButton
{
    void OnTouch();
}