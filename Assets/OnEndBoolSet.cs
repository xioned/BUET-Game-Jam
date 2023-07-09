using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnEndBoolSet : StateMachineBehaviour
{
    public string boolName;
    public bool setBool;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(boolName, setBool);
    }
}
