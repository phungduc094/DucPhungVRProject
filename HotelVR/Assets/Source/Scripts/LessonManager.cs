using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonManager : MonoBehaviour
{
    #region Singleton

    public static LessonManager instance;

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

    #endregion

    public int totalLesson = 1;
    public int lesson = 1;
    private LessonController lessonController;

    public void StartLesson()
    {
        lesson = 1;
    }

    public void LoadLesson()
    {
        if (lessonController != null) Destroy(lessonController.gameObject);

        GameObject newLesson = Instantiate(Resources.Load(("AlacartPractice"), typeof(GameObject)), transform) as GameObject;
        lessonController = newLesson.GetComponent<LessonController>();

        Debug.Log("Load Lesson: " + newLesson.name);
        lessonController.Init();
        lessonController.SetUp();
        lessonController.StartLesson();
    }

    public void NextLesson()
    {
        if (lesson == totalLesson) Debug.Log("Finished Lesson");
        else
        {
            lesson++;
            LoadLesson();
        }
        lesson++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartLesson();
            LoadLesson();
        }
    }
}
