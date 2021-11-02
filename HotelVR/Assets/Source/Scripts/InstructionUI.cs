using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionUI : Panel
{
    public static InstructionUI instance;

    public override void Init()
    {
        instance = this;
    }

    /*private void Start()
    {
        instructor.PlayInstructor(Lesson.Lesson1);
    }

    public override void Active()
    {
        base.Active();

        instructor.PlayInstructor(Lesson.Lesson1);
    }

    [SerializeField] private Instructor instructor;
    bool isCanNext = true;
    public void NextBtn()
    {
        if (!isCanNext) return;

        isCanNext = false;
        instructor.Next();
        StartCoroutine(DoResetCanNext());
    }

    private IEnumerator DoResetCanNext()
    {
        yield return new WaitForSeconds(0.5f);
        isCanNext = true;

        instructor.ContinueInstruction();
    }

    public void ContinueBtn()
    {
        instructor.ContinueInstruction();
    }*/
}