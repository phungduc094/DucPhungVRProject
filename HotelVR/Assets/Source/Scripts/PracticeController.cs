using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeController : MonoBehaviour
{
    #region Singleton

    public static PracticeController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private UnityEngine.Events.UnityEvent[] steps;
    [SerializeField] private ItemGroup[] itemGroups;

    private void Start()
    {
        ResetGrabbale();

        NextStep();
    }

    int index = 0;
    private void Update()
    {
        if (index < steps.Length)
        {
            steps[index]?.Invoke();
        }
    }

    private void LateUpdate()
    {
        if (CheckAllDone())
        {
            index++;
            NextStep();
        }
    }
    
    private void ResetGrabbale()
    {
        for (int i = 0; i < itemGroups.Length; i++)
        {
            foreach (Item it in itemGroups[i].items) it.SetGrabbale(false);
        }
    }
    private void NextStep()
    {
        ResetGrabbale();
        foreach(Item it in itemGroups[index].items) it.SetGrabbale(true);
        LessonManager.instance.NextStep();
    }

    public bool[] checkConditions { get; set; }
    public void SetStep(int amountStep)
    {
        checkConditions = new bool[amountStep];
        for (int i = 0; i < amountStep; i++) checkConditions[i] = false;
    }

    private bool CheckAllDone()
    {
        for(int i = 0; i < checkConditions.Length; i++)
        {
            if (checkConditions[i] == false) return false;
        }
        return true;
    }
}

[System.Serializable]
public class ItemGroup
{
    public Item[] items;
}