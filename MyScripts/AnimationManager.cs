using UnityEngine;
using System.Collections;
using Mebiustos.MMD4MecanimFaciem;

public class AnimationManager : MonoBehaviour
{

    private MMDDialogManager dialogManager;
    private Animator animator;
    private FaciemController faciemController;

    private void Awake()
    {
        faciemController = GetComponent<FaciemController>();
        dialogManager = GetComponent<MMDDialogManager>();
        animator = GetComponent<Animator>();
    }

    /*
    private void Update()
    {
        if (dialogManager.currentLine == 7 && !bTriggered)
        {
            bTriggered = true;
            animator.SetTrigger("tIncorrect");
        }
        if (dialogManager.currentLine == 14 && !bTriggered)
        {
            bTriggered = true;
            animator.SetTrigger("tIncorrect");
        }
    }
    */

    public void SetAnimation(int currentLine)
    {
        if (currentLine == 1)
        {
            
            faciemController.SetFace("serious");
        }

        if (currentLine == 7)
        {
            animator.SetTrigger("tIncorrect");
            faciemController.SetFace("serious");
        }

        if (currentLine == 14)
        {
            animator.SetTrigger("tShowGratitude");
        }
    }
}
