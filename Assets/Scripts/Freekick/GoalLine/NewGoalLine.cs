using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class NewGoalLine : MonoBehaviour
{
    [HideInInspector]public Animator anim;
    [SerializeField] GameObject core;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Ball"))
        {
            core.SetActive(false);
            anim.SetTrigger("Break");
            FreeKickManager.Ins.goal?.Invoke(true);
        }
    }

    public void DestroyThis()
    {
        
        FreeKickManager.Ins.currentGoalLines.Remove(this);
        FreeKickManager.Ins.PlusScore(transform);
        Destroy(gameObject);
    }

}
