using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public enum SecurityState
{
    Idle,
    Chasing,
    Punispunishment
}
public class Security : MonoBehaviour
{
    private int timesAttack;
    private float timeRate;
    private NavMeshAgent nav;
    [SerializeField] Transform idlePos;
    [SerializeField] Transform player;
    public static Action<Transform> catched;
    private Animator anim;
    public static SecurityState securityState;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();

        Flag.isAttacked += (ob) =>
        {
            if (timeRate >= 20)
                timesAttack++;
        };
        IdleState();
    }
    public void IdleState()
    {
        securityState = SecurityState.Idle;
        timesAttack = 0;
        timeRate = 0;
        //Debug.LogError("Idle state");
        nav.SetDestination(idlePos.transform.position);
    }
    IEnumerator ChaseState()
    {
        securityState = SecurityState.Chasing;
        //Debug.LogError("Chasing state");
        nav.SetDestination(player.position + Vector3.right);
        yield return new WaitForSeconds(20);
        IdleState();
    }
    IEnumerator PunishmentState()
    {

        securityState = SecurityState.Punispunishment;
        LookRotation();
        anim.SetBool("CatchPlayer", true);
        //Debug.LogError("Punishment state");
        catched?.Invoke(transform);
        yield return new WaitForSecondsRealtime(10);
        ScoreManager.score -= 50;
        //player.GetComponent<PlayerController>().playerDataBinding.IsCatchedBySecurity = false;
        InGameManager.Instance.IngameState = IngameState.Ingame;
        IdleState();
        anim.SetBool("CatchPlayer", false);
    }
    private void Update()
    {
        if (securityState == SecurityState.Idle)
            timeRate += Time.deltaTime;
        if (timesAttack >= 5)
        {
            timeRate = 0;
            timesAttack = 0;
            StartCoroutine(ChaseState());
        }
        #region "Animation"
        if (nav.velocity.normalized.magnitude > 0.2f)
            anim.SetBool("Running", true);
        else
            anim.SetBool("Running", false);
        #endregion
        if (securityState == SecurityState.Chasing)
            nav.SetDestination(player.position + Vector3.right);
        if (securityState == SecurityState.Punispunishment)
            LookRotation();

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Player"))
        {
            //Debug.LogError("Player Enter");
            if (securityState == SecurityState.Chasing)
            {
                StopAllCoroutines();
                StartCoroutine(PunishmentState());
            }
        }
    }
    public void LookRotation()
    {
        Vector3 dir = player.position - transform.position;

        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Euler(new Vector3(0, q.eulerAngles.y, 0));
        }
    }
    public void OnDisable()
    {
        securityState = SecurityState.Idle;
    }
}
