using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Instructor : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private Teacher teacher;
    [SerializeField] private LessonTutorial[] lessonTutorials;

    [Header("Preferences")]
    [SerializeField] private TextMeshProUGUI textGuide;

    string[] instructionsCurrent;
    AudioClip[] clipsCurrent;
    int index = 0;

    [SerializeField] private LocomotionTeleport locomotion;
    [SerializeField] private Transform target;
    public void PlayLessionTutorial()
    {
        /*LoginUI.instance.Deactive();*/

        /*locomotion.DoTeleportCustom(target);*/
        index = 0;
        PlayLession(0);
    }

    private void PlayLession(int index)
    {
        instructionsCurrent = lessonTutorials[index].instructions;
        clipsCurrent = lessonTutorials[index].clips;

        StartCoroutine(DoPlayLesson());
    }

    bool nextStep;
    private IEnumerator DoPlayLesson()
    {
        SoundManager.instance.PlaySFX(clipsCurrent[0], 1f);
        textGuide.text = instructionsCurrent[0];
        teacher.PlayBowAnim();
        yield return new WaitForSeconds(clipsCurrent[0].length);

        int i = 1;
        nextStep = false;

        while(i < instructionsCurrent.Length)
        {
            SoundManager.instance.PlaySFX(clipsCurrent[i], 1f);
            textGuide.text = instructionsCurrent[i];
            teacher.PlayBowAnim();

            while(!nextStep)
            {
                yield return null;
            }

            nextStep = false;
            i++;
        }

        Debug.Log("Finished Lesson " + (index + 1).ToString());
        index++;
        if (index < lessonTutorials.Length)
        {
            PlayLession(index);
        }
    }

    public void NextStep()
    {
        nextStep = true;
    }

    [SerializeField]
    Transform ovrCam;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            SetUp();
            PlayLessionTutorial();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            nextStep = true;
        }

        if (ovrCam != null)
            textGuide.transform.LookAt(ovrCam.position);
    }

    public void SetUp()
    {
        teacher.gameObject.SetActive(true);
    }
}

[System.Serializable]
public class LessonTutorial
{
    public string[] instructions;
    public AudioClip[] clips;
}