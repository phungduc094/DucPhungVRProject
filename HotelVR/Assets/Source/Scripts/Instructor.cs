using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructor : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Teacher teacher;
    [SerializeField] private LessonTutorial lessonTutorial;

    [Header("Preferences")]
    [SerializeField] private TextMeshProUGUI textGuide;

    public void ReadLessonTitle()
    {
        AudioClip clip = lessonTutorial.titleClip;
        SoundManager.instance.PlaySFX(clip, 1f);
        StartCoroutine(DoReadingTitle(clip));
    }

    private IEnumerator DoReadingTitle(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);

        yield return new WaitForSeconds(0.5f);
        LessonController.instance.practiceCurrent.NextStep();
    }

    public void ReadGuide(int index)
    {
        textGuide.text = lessonTutorial.instructions[index];
        AudioClip clip = lessonTutorial.clips[index];
        SoundManager.instance.PlaySFX(clip, 1f);
        teacher.PlayBowAnim();

        StartCoroutine(DoReading(clip));
    }

    private IEnumerator DoReading(AudioClip clip)
    {
        yield return new WaitForSeconds(clip.length);

        LessonController.instance.practiceCurrent.isCheck = true;
    }
}

[System.Serializable]
public class LessonTutorial
{
    public string[] instructions;
    public AudioClip titleClip;
    public AudioClip[] clips;
}