using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataBinding : MonoBehaviour
{
    [SerializeField] Animator anim;
    private int kickHash;
    private int throwHash;
    private int speedHash;
    private int isCatchedBySecurity;
    public bool Kick
    {
        set
        {
            if (value)
                anim.SetTrigger(kickHash);
        }
    }
    public bool Throw
    {
        set
        {
            if (value)
                anim.SetTrigger(throwHash);
        }
    }
    public float Speed
    {
        set
        {
            anim.SetFloat(speedHash, value);
        }
        get
        {
            return anim.GetFloat(speedHash);
        }
    }
    public bool IsCatchedBySecurity
    {
        set
        {
            anim.SetBool(isCatchedBySecurity, value);
        }
    }
    private void Awake()
    {
        kickHash = Animator.StringToHash("Kick");
        throwHash = Animator.StringToHash("Throw");
        speedHash = Animator.StringToHash("Speed");
        isCatchedBySecurity = Animator.StringToHash("IsCatchedBySecurity");
    }


}
