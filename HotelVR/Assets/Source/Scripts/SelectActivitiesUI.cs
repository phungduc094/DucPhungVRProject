using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SelectActivitiesUI : Panel
{
    public static SelectActivitiesUI instance;

    public override void Init()
    {
        instance = this;
    }

    [SerializeField] private TextMeshProUGUI lessonName;
    [SerializeField] private TextMeshProUGUI progressLoadingScene;

    public override void SetUp()
    {
        /*switch(LessonManager.instance.lesson)
        {
            case Lesson.Lesson1:
                lessonName.text = "THỰC ĐƠN ALACARTE";
                break;
            case Lesson.Lesson2:
                lessonName.text = "NGHIỆP VỤ BUỒNG PHÒNG";
                break;
            default:
                lessonName.text = "PHA CHẾ";
                break;
        }*/

        lessonName.text = "THỰC ĐƠN ALACARTE";

        Active();
    }

    public override void Active()
    {
        base.Active();
        canTouch = true;
    }

    bool canTouch = true;

    #region ButtonFunctions

    public void SelectActivitiesBack()
    {
        if (!canTouch) return;
        
        MenuUI.instance.Active();
        Deactive();
    }

    public void ExitBtn()
    {
        if (!canTouch) return;

        Application.Quit();
    }

    public void PracticeBtn()
    {
        /*if (!canTouch) return;

        canTouch = false;*/
        LessonManager.instance.Practice();
        Deactive();
        //SceneLoader.instance.LoadScene();
    }

    #endregion

    public void UpdateProgressLoadingScene(float value)
    {
        progressLoadingScene.text = ((int)value * 100).ToString() + "%";
    }
}
