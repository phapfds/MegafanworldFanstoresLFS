using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
public class Flag : MonoBehaviour
{
    public FlagPositionType flagPositionType;
    public FlagType flagType;
    public static Action<Flag> isAttacked;
    public static Action score;
    public static Action<int, Vector3> scorePopups;

    public bool canAttack;
    private int t;


    private void OnEnable()
    {
        _OnEnable();
    }
    public virtual void _OnEnable()
    {
        canAttack = false;
        StartCoroutine(AutoDestroy());
    }
    public void OnAttack()
    {
        if (flagType == FlagType.Enemy)
        {
            var ef = FlagManager.Instance.dicEffect[FlagType.Enemy];
            ef.transform.position = transform.position + Vector3.forward;
            ef.SetActive(true);
            switch (flagPositionType)
            {
                case FlagPositionType.WindowFlag:
                    //Debug.Log("Score: + 100");
                    ScoreManager.score += 100;
                    t = 100;
                    break;
                case FlagPositionType.WalkingPeopleFlag:
                    //Debug.Log("Score: + 200");
                    ScoreManager.score += 200;
                    t = 200;
                    break;
                case FlagPositionType.BikeFlag:
                    //Debug.Log("Score: + 500");
                    ScoreManager.score += 500;
                    t = 500;
                    break;
                case FlagPositionType.BullFlag:
                    //Debug.Log("Score: + 1000");
                    ScoreManager.score += 1000;
                    t = 1000;
                    break;
                case FlagPositionType.BirdFlag:
                    //Debug.Log("Score: + 1000");
                    ScoreManager.score += 500;
                    t = 500;
                    break;
                case FlagPositionType.HighBirdFlag:
                    //Debug.Log("Score: + 1000");
                    ScoreManager.score += 1000;
                    t = 1000;
                    break;
                case FlagPositionType.USSpecialEnemyFlag:
                    ScoreManager.score += 500;
                    t = 500;
                    break;
            }
        }
        else
        {
            //Debug.Log("Score: -50");
            ScoreManager.score -= 50;
            t = -50;
            var ef = FlagManager.Instance.dicEffect[FlagType.Friend];
            ef.transform.position = transform.position + Vector3.forward;
            ef.SetActive(true);
        };
        score?.Invoke();
        scorePopups?.Invoke(t, transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Mascot") && canAttack)
        {
            if (flagPositionType == FlagPositionType.WindowFlag)
                FlagManager.Instance.DestroyWindowFlag(this);
            else
            {
                FlagManager.Instance.StartCoroutine(FlagManager.Instance.DestroyAndSpawnNewPeopleFlag(this.transform));
            }
            OnAttack();
        }
    }
    private void OnMouseDown()
    {

#if UNITY_STANDALONE_WIN || UNITY_WEBGL
OnTouch();
#endif
    }

    public void OnTouch()
    {
        if (InGameManager.Instance.canAttack && InGameManager.Instance.IngameState == IngameState.Ingame && !EventSystem.current.IsPointerOverGameObject() && Mascot.Instance.mascotState == MascotState.Idle)
        {
            isAttacked?.Invoke(this);
            canAttack = true;
            InGameManager.Instance.timeAttack = 0;
        }
    }



    private void OnDisable()
    {
        StopCoroutine(AutoDestroy());
        //if (FindObjectOfType<Mascot>() != null)
        //    Mascot.Instance.mascotState = MascotState.Idle;

    }

    public void StopDestroy()
    {
        StopAllCoroutines();

    }

    IEnumerator AutoDestroy()
    {
        if (flagPositionType == FlagPositionType.WindowFlag)
        {
            yield return new WaitForSeconds(10);
            if (!canAttack)
                FlagManager.Instance.DestroyWindowFlag(this);
        }
    }
}
