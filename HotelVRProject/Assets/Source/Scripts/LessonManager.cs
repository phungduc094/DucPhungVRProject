using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonManager : MonoBehaviour
{
    public static LessonManager instance;

    public Lesson lesson;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SetUp(Lesson lesson)
    {
        this.lesson = lesson;
    }
}

public enum Lesson
{
    Lesson1,
    Lesson2,
    Lesson3
}
