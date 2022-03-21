using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalDataBinding : MonoBehaviour
{
    [SerializeField] Animator anim;
    private int speedHash;
    private int attackHash;
    public float Speed
    {
        set
        {
            anim.SetFloat(speedHash, value);
        }
    }
    public bool Attack
    {
        set
        {
            anim.SetTrigger(attackHash);
        }
    }
    private void Awake()
    {
        speedHash = Animator.StringToHash("Speed");
        attackHash = Animator.StringToHash("Attack");
    }
}