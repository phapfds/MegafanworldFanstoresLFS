using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Animal : Mascot
{
    [SerializeField] NavMeshAgent nav;
    [SerializeField] AnimalDataBinding animalDataBinding;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject idlePos;
    private float timeAttackWrongPos;
    public override void OnAwake()
    {
        base.OnAwake();
        audioSource = GetComponent<AudioSource>();
    }
    public override void IdleState()
    {
        //nav.SetDestination(player.transform.position + 5 * Vector3.back);
        nav.SetDestination(idlePos.transform.position);
        Land();
        audioSource.mute = true;
    }
    public override void AttackState(Vector3 pos)
    {
        base.AttackState(pos);
        //nav.SetDestination(pos);
        transform.position = Vector3.MoveTowards(transform.position, pos, 0.5f);
        RotateToAgent(pos);

        Fly(pos);
    }
    public override void AttackState(Flag flag)
    {
        base.AttackState(flag.transform.position);
        //nav.SetDestination(flag.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, flag.gameObject.transform.position, 0.5f);
        RotateToAgent(flag.gameObject);

        Fly(flag.transform.position);

    }
    public override void OnUpdate()
    {
        animalDataBinding.Speed = nav.velocity.normalized.magnitude;
        if (mascotState == MascotState.AttackWrongObj)
        {
            timeAttackWrongPos += Time.deltaTime;
            if (timeAttackWrongPos >= 2)
            {
                Flag.scorePopups?.Invoke(0, transform.position);
                mascotState = MascotState.Idle;
                timeAttackWrongPos = 0;
            }
        }
    }
    private void Fly(Vector3 pos)
    {
        if (Vector3.Distance(transform.position, pos) <= pos.y + 5)
        {
            audioSource.mute = false;

            if (transform.position.y < pos.y)
                nav.baseOffset += 0.5f;
            else
                if (transform.position.y > pos.y + 0.5)
                nav.baseOffset -= 0.1f;
        }
    }
    private void Land()
    {
        if (nav.baseOffset > 0)
            nav.baseOffset -= 0.1f;
        if (nav.baseOffset < 0)
            nav.baseOffset = 0;
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
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Flag"))
        {
            if (other.gameObject.GetComponent<Flag>().canAttack)
            {
                animalDataBinding.Attack = true;
                mascotState = MascotState.Idle;
                audioSource.mute = true;
            }
        }
    }
}


