using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBoolOnEnter : StateMachineBehaviour
{
    public string pamareterName;
    public bool pamareterValue;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(pamareterName, pamareterValue);
    }
}
