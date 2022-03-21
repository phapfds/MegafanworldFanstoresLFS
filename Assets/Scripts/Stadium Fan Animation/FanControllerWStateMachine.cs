using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanControllerWStateMachine : StateMachineBehaviour
{
    public string ParaName;
    public int StateCount = 0;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger(ParaName, Random.Range(0, StateCount));
    }
}
