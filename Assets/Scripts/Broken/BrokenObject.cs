using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenObject : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Ball"))
        {
            anim.SetTrigger("Break");
        }
    }

    public void DestroyThis()
    {
        FreeKickManager.Ins.currentGoalLines.Remove(GetComponent<NewGoalLine>());
        Destroy(gameObject);
    }
}
