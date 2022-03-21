using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BaldEagle : Mascot
{
    [SerializeField] NavMeshAgent nav;
    [SerializeField] BaldEagleDataBinding baldEagleDataBinding;
    [SerializeField] AudioSource audioSource;
    public Transform idlePos;
    private float timeAttackWrongPos;
    private BoxCollider boxCollider;
    public NavMeshAgent agent;
    public override void OnAwake()
    {
        base.OnAwake();
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
    }
    public override void IdleState()
    {
        //nav.SetDestination(player.transform.position + 5 * Vector3.back);
        //if (agent.isOnNavMesh)
        //{
        //    nav.SetDestination(idlePos.position);
        //}
        //else
        //{
        //    transform.position = Vector3.MoveTowards(transform.position, idlePos.position, 0.5f);
        //    RotateToAgent(player.transform.position);
        //}
        nav.SetDestination(idlePos.position);
        if (boxCollider.enabled) boxCollider.enabled = false;
        Land();
        audioSource.mute = true;
    }
    public override void AttackState(Vector3 pos)
    {
        if (agent.enabled) agent.enabled = false;
        base.AttackState(pos);
        //nav.SetDestination(pos);
        transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
        Fly(pos);
        RotateToAgent(pos);
        audioSource.mute = false;
    }
    public override void AttackState(Flag flag)
    {
        if (agent.enabled) agent.enabled = false;
        base.AttackState(flag);
        //nav.SetDestination(flag.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, flag.gameObject.transform.position, 0.5f);
        RotateToAgent(flag.gameObject);
        Fly(flag.transform.position);
    }
    public override void OnUpdate()
    {
        baldEagleDataBinding.Speed = nav.velocity.normalized.magnitude;
        if (mascotState == MascotState.AttackWrongObj)
        {
            timeAttackWrongPos += Time.deltaTime;
            if (timeAttackWrongPos > 1f)
            {
                Flag.scorePopups?.Invoke(0, transform.position);
                mascotState = MascotState.Idle;
                boxCollider.enabled = false;
                agent.enabled = true;
                timeAttackWrongPos = 0;
            }
        }
        if (mascotState != MascotState.Idle)
        {
            if (!boxCollider.enabled) boxCollider.enabled = true;
        }
    }
    private void Fly(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) <= pos.y + 5)
        {
            audioSource.mute = false;
            if (transform.position.y < pos.y)
                nav.baseOffset += 0.1f;
            else
                if (transform.position.y > pos.y + 0.5)
                nav.baseOffset -= 0.01f;
        }
    }
    private void RotateToAgent(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;

        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Euler(new Vector3(0, q.eulerAngles.y, 0));
        }
    }
    private void RotateToAgent(GameObject pos)
    {
        Vector3 dir = pos.transform.position - transform.position;

        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Euler(new Vector3(0, q.eulerAngles.y, 0));
        }
    }
    private void Land()
    {
        if (nav.baseOffset > 1)
            nav.baseOffset -= 0.01f;
        if (nav.baseOffset < 1)
            nav.baseOffset = 1;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Flag"))
        {
            if (other.gameObject.GetComponent<Flag>().canAttack)
            {
                baldEagleDataBinding.Attack = true;
                mascotState = MascotState.Idle;
                audioSource.mute = false;
                boxCollider.enabled = false;
                agent.enabled = true;
            }
        }
    }
}
