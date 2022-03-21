using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrowing : Mascot
{
    [SerializeField] GameObject idleObjectThrowing;
    private float animTime;
    private bool endOfThrowAnim = false;
    private Vector3 posObjectStartAttack;
    private bool detectedEndAnim;
    private List<MeshRenderer> mesh = new List<MeshRenderer>();
    public override void OnAwake()
    {
        base.OnAwake();
        idleObjectThrowing.SetActive(true);
        if (this.gameObject.GetComponent<MeshRenderer>() != null)
        {
            mesh.Add(this.gameObject.GetComponent<MeshRenderer>());
        }
        else
        {
            MeshRenderer[] _mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer m in _mesh)
            {
                mesh.Add(m);
            }
        };

        animTime = 0;
        endOfThrowAnim = false;
        detectedEndAnim = false;
        foreach (MeshRenderer m in mesh)
        {
            m.enabled = false;
        }
    }
    public override void IdleState()
    {
        transform.position = idleObjectThrowing.transform.position;
    }
    public override void AttackState(Vector3 aim)
    {
        base.AttackState(aim);
        if (!detectedEndAnim)
        {
            StartCoroutine(EndOfAnim());
            detectedEndAnim = true;
        }
        if (endOfThrowAnim)
        {
            StopCoroutine(EndOfAnim());
            idleObjectThrowing.SetActive(false);
            foreach (MeshRenderer m in mesh)
            {
                m.enabled = true;
            }
            animTime += Time.deltaTime;
            animTime %= 2f;
            Vector3 aimPos = aim;
            transform.position = MathParabola.Parabola(posObjectStartAttack, aimPos, aimPos.y, animTime / 2f);
        }
    }
    public override void AttackState(Flag flag)
    {
        base.AttackState(flag);
        if (!detectedEndAnim)
        {
            StartCoroutine(EndOfAnim());
            detectedEndAnim = true;
        }
        if (endOfThrowAnim)
        {
            StopCoroutine(EndOfAnim());
            idleObjectThrowing.SetActive(false);
            foreach (MeshRenderer m in mesh)
            {
                m.enabled = true;
            }
            animTime += Time.deltaTime;
            animTime %= 2f;
            Vector3 aimPos = flag.transform.position;
            if (flag.flagPositionType == FlagPositionType.WindowFlag)
                transform.position = MathParabola.Parabola(posObjectStartAttack, aimPos, aimPos.y, animTime / 2f);
            else
                transform.position = MathParabola.Parabola(posObjectStartAttack, aimPos, aimPos.y, 1.5f * animTime);
        }
    }
    IEnumerator EndOfAnim()
    {
        yield return new WaitForSeconds(0.3f);
        posObjectStartAttack = idleObjectThrowing.transform.position;
        endOfThrowAnim = true;
    }
    public override void OnUpdate()
    {
        //Debug.LogError(mascotState);
        if (mascotState == MascotState.AttackWrongObj)
        {
            if (Vector3.Distance(transform.position, aimFlag) < 10)
            {
                Flag.scorePopups?.Invoke(0, transform.position);
                mascotState = MascotState.Idle;
                idleObjectThrowing.SetActive(true);
                foreach (MeshRenderer m in mesh)
                {
                    m.enabled = false;
                }
                animTime = 0;
                endOfThrowAnim = false;
                detectedEndAnim = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Flag"))
        {
            if (other.gameObject.GetComponent<Flag>().canAttack)
            {
                //Debug.LogError("Change to idle");
                mascotState = MascotState.Idle;
                idleObjectThrowing.SetActive(true);
                foreach (MeshRenderer m in mesh)
                {
                    m.enabled = false;
                }
                animTime = 0;
                endOfThrowAnim = false;
                detectedEndAnim = false;
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (mascotState == MascotState.Attack)
        {

        }
    }
}
