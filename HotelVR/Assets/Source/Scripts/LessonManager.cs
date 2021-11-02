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

    [SerializeField] private Instructor instructor;

    public void SetUp()
    {
        instructor.SetUp();
    }

    public void Practice()
    {
        //OVRCamPos.instance.SetCamPosition(1);
        SetUp();
        instructor.PlayLessionTutorial();
    }

    public void Test()
    {

    }

    public void NextStep()
    {
        instructor.NextStep();
    }
}
