using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolOnExit : StateMachineBehaviour
{
    public string pamareterName;
    public bool pamareterValue;

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(pamareterName, pamareterValue);
    }
}
