using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GoalLine : MonoBehaviour
{
    public BallPower ball;
    [SerializeField] List<GoalLine> goalines;
    //public bool needDetectShootFail;
    //private void OnEnable()
    //{
    //    ball.IsKicked += Ball_IsKicked;
    //}

    //private void Ball_IsKicked(object sender, EventArgs e)
    //{
    //    StopAllCoroutines();
    //    if (needDetectShootFail)
    //        StartCoroutine(GoalDetect());
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains(ball.tag))
        {
            //StopAllCoroutines();
            FreeKickManagement.ins.StopCoroutineGoalDetect();
            ScoreManager.score += 1000;
            //Debug.Log(ScoreManager.score);
            FreeKickManagement.ins.GoalLine_Goal(this, true);
            //GKController.inst.GoalLine_Goal(this, true);
            KickerController.ins.GoalLine_Goal(this, true);
            KickerResult.inst.GoalLine_Goal(this, true);

            foreach (GoalLine gl in goalines)
            {
                if (gl != null)
                    gl.gameObject.SetActive(false);
            }
        }
    }
    //IEnumerator GoalDetect()
    //{
    //    yield return new WaitForSeconds(2f);
    //    FreeKickManagement.ins.GoalLine_Goal(this, false);
    //    //GKController.inst.GoalLine_Goal(this, false);
    //    KickerController.ins.GoalLine_Goal(this, false);
    //    KickerResult.inst.GoalLine_Goal(this, false);
    //}
    //private void OnDisable()
    //{
    //    ball.IsKicked -= Ball_IsKicked;
    //    StopAllCoroutines();

    //}
}
