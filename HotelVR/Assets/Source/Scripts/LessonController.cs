using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessonController : MonoBehaviour
{
    #region Singleton

    public static LessonController instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    [SerializeField] private int amountPractice;
    [SerializeField] private GameObject DYNAMIC;
    public bool isFinished;

    public PracticeController practiceCurrent { get; set; }

    public void Init()
    {
        DYNAMIC.SetActive(false);
        practices = new PracticeController[amountPractice];
    }

    GameObject newDynamic;
    PracticeController[] practices;
    int index;
    public void SetUp()
    {
        if (newDynamic != null) DestroyImmediate(newDynamic.gameObject);

        newDynamic = Instantiate(DYNAMIC, transform);

        // Get Pratices
        for(int i = 0; i < amountPractice; i++)
        {
            Transform trans = newDynamic.transform.GetChild(i);
            trans.gameObject.SetActive(false);
            practices[i] = trans.GetComponent<PracticeController>();
        }

        // Clear Grabbales
        Transform items = newDynamic.transform.GetChild(amountPractice + 1);
        foreach(Transform child in items)
        {
            Item item = child.GetComponent<Item>();
            if (item != null)
            {
                item.SetGrabbale(false);
            }
        }

        index = 0;
        SetUpLesson();

        newDynamic.SetActive(true);
    }

    public void StartLesson()
    {
        practiceCurrent.StartPractice();
    }

    public void ReStartLesson()
    {

    }

    public void NextLesson()
    {
        if (practiceCurrent != null) practiceCurrent.gameObject.SetActive(false);

        index++;
        SetUpLesson();
        Debug.Log("Practice: " + index);

        StartLesson();
    }

    public void LessonFail()
    {

    }

    public void LessonFinished()
    {

    }

    private void SetUpLesson()
    {
        practiceCurrent = practices[index];
        practiceCurrent.gameObject.SetActive(true);
    }
}