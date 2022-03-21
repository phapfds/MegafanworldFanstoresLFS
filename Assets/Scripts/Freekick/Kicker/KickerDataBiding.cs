using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerDataBiding : MonoBehaviour
{
    public Animator anim;

    private int idleHash;
    private int kickHash;
    private int goalHash;
    private int deafeatHash;

    public bool IdlePar
    {
        get
        {
            return anim.GetBool(idleHash);
        }
        set
        {
            anim.SetBool(idleHash, value);
        }
    }
    public bool KickPar
    {
        get
        {
            return anim.GetBool(kickHash);
        }
        set
        {
            if (value)
                anim.SetTrigger(kickHash);
        }
    }
    public bool GoalPar
    {
        set
        {
            if (value)
                anim.SetTrigger(goalHash);
        }
    }
    public bool DefeatPar
    {
        set
        {
            if (value)
                anim.SetTrigger(deafeatHash);
        }
    }
    public bool KickState
    {
        get
        {
            return anim.GetCurrentAnimatorStateInfo(0).fullPathHash == -1751635351;
        }
    }
    private void Awake()
    {
        idleHash = Animator.StringToHash("Idle");
        kickHash = Animator.StringToHash("Kick");
        goalHash = Animator.StringToHash("Goal");
        deafeatHash = Animator.StringToHash("Defeat");
    }
}
