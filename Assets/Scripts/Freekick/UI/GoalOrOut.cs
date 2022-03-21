using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class GoalOrOut : MonoBehaviour
{
    public GoalLine goalLine;
    //public TextMesh goalOrOut;
    public TextMeshPro goalOrOut;
    public Animator anim;
    public FreeKickManagement freeKickManagement;
    void Start()
    {
        //goalLine.Goal += GoalLine_Goal;
    }

    private void GoalLine_Goal(object sender, bool e)
    {
        StartCoroutine(DisplayText(e));
    }
    IEnumerator DisplayText(bool e)
    {
        if (e)
        {
            goalOrOut.text = "Goal";

        }
        else
        {
            goalOrOut.text = "Out";
        }
        goalOrOut.gameObject.SetActive(true);

        if (freeKickManagement.currentState == FreeKickState.GoalKeeper)
        {
            goalOrOut.transform.localScale = new Vector3(1f, 1f, 1f);
            anim.SetTrigger("ForKicker");

        }
        else
        {
            goalOrOut.transform.localScale = new Vector3(-1f, 1f, 1f);
            anim.SetTrigger("ForGoalKeeper");
        }

        yield return new WaitForSeconds(3);
        goalOrOut.gameObject.SetActive(false);

    }
}
