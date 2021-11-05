using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeController : MonoBehaviour
{
    [SerializeField] private Instructor instructor;
    [SerializeField] private Step[] steps;
    [SerializeField] private ItemGroup[] itemGroups;

    int index;
    public void StartPractice()
    {
        index = 0;
        instructor.ReadLessonTitle();
    }

    public bool isCheck { get; set; }
    public void NextStep()
    {
        if (index == steps.Length)
        {
            Debug.Log("Finished Practice.");
            LessonController.instance.NextLesson();
            return;
        }

        ClearGrabbale();

        SetConditions(steps[index].conditionAmount);
        if (steps[index].prepareTask != null) steps[index].prepareTask?.Invoke();

        isCheck = false;
        instructor.ReadGuide(index);
        StartCoroutine(WaitingFinishedGuide());
    }

    private IEnumerator WaitingFinishedGuide()
    {
        while (!isCheck)
        {
            yield return null;
        }

        foreach (Item it in itemGroups[index].items) it.SetGrabbale(true);
    }


    private void Update()
    {
        if (!isCheck) return;

        if (index < steps.Length)
        {
            steps[index].task?.Invoke();
        }
    }

    private void LateUpdate()
    {
        if (!isCheck) return;

        if (CheckAllDone())
        {
            index++;
            NextStep();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            index++;
            NextStep();
        }
    }

    public bool[] checkConditions { get; set; }
    private void SetConditions(int conditionAmount)
    {
        checkConditions = new bool[conditionAmount];
        for (int i = 0; i < conditionAmount; i++) checkConditions[i] = false;
    }

    private bool CheckAllDone()
    {
        for(int i = 0; i < checkConditions.Length; i++)
        {
            if (checkConditions[i] == false) return false;
        }
        return true;
    }

    private void ClearGrabbale()
    {
        for (int i = 0; i < itemGroups.Length; i++)
        {
            foreach (Item it in itemGroups[i].items) it.SetGrabbale(false);
        }
    }
}

[System.Serializable]
public class ItemGroup
{
    public Item[] items;
}

[System.Serializable]
public class Step
{
    public int conditionAmount;
    public UnityEngine.Events.UnityEvent prepareTask;
    public UnityEngine.Events.UnityEvent task;
}