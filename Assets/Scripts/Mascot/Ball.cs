using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : Mascot
{
    private float animTime;
    private bool endOfKickAnim = false;
    [SerializeField] Transform idlePos;
    private bool detectedEndAnim;
    private float speedRotate;
    private PlayerDataBinding playerDataBinding;

    [SerializeField] AudioSource audioSource;
    private MeshRenderer meshRenderer;
    [SerializeField] Rigidbody rigidbodyPlayer;
    public override void OnAwake()
    {
        base.OnAwake();
        animTime = 0;
        endOfKickAnim = false;
        detectedEndAnim = false;
        speedRotate = 0;
        //playerDataBinding = player.GetComponent<PlayerController>().playerDataBinding;
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = true;
    }
    public override void IdleState()
    {

        if (rigidbodyPlayer.velocity.magnitude >= 2f)
            gameObject.transform.position = new Vector3(idlePos.position.x, 0.1479751f, idlePos.position.z);
        if (rigidbodyPlayer.velocity.magnitude >= 2f)
        {
            speedRotate = speedRotate < 4 ? speedRotate + 0.1f : 4;
        }
        else
        {
            speedRotate = speedRotate > 0 ? speedRotate - 0.1f : 0;
        }
        Rotate(speedRotate);

    }

    public override void AttackState(Vector3 aim)
    {
        base.AttackState(aim);
        meshRenderer.enabled = true;
        if (!detectedEndAnim)
        {
            StartCoroutine(EndOfAnim());
            detectedEndAnim = true;
        }
        if (endOfKickAnim)
        {
            StopCoroutine(EndOfAnim());
            animTime += Time.deltaTime;
            animTime %= 2f;
            Vector3 aimPos = aim;
            transform.position = MathParabola.Parabola(idlePos.position, aimPos, aimPos.y, animTime / 2f);
        }
    }

    public override void AttackState(Flag flag)
    {
        base.AttackState(flag);
        meshRenderer.enabled = true;
        if (!detectedEndAnim)
        {
            StartCoroutine(EndOfAnim());
            detectedEndAnim = true;
        }
        if (endOfKickAnim)
        {
            StopCoroutine(EndOfAnim());
            animTime += Time.deltaTime;
            animTime %= 2f;
            Vector3 aimPos = flag.transform.position;
            if (flag.flagPositionType == FlagPositionType.WindowFlag)
                transform.position = MathParabola.Parabola(idlePos.position, aimPos, aimPos.y, animTime / 2f);
            else
                transform.position = MathParabola.Parabola(idlePos.position, aimPos, aimPos.y, 1.5f * animTime);
        }
    }
    IEnumerator EndOfAnim()
    {
        gameObject.transform.position = idlePos.position;
        yield return new WaitForSeconds(0.5f);
        endOfKickAnim = true;
        audioSource.Play();

    }
    public override void OnUpdate()
    {
        if (mascotState == MascotState.AttackWrongObj)
        {
            if (Vector3.Distance(transform.position, aimFlag) < 10)
            {
                Flag.scorePopups?.Invoke(0, transform.position);
                mascotState = MascotState.Idle;
                animTime = 0;
                endOfKickAnim = false;
                detectedEndAnim = false;
                gameObject.transform.position = new Vector3(idlePos.position.x, 0.1479751f, idlePos.position.z);

            }
        }
    }
    public override void OnAttackWrongObject(Vector3 pos)
    {
        base.OnAttackWrongObject(pos);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Contains("Flag"))
        {
            if (other.gameObject.GetComponent<Flag>().canAttack)
            {
                //Debug.LogError("Change to idle state");
                mascotState = MascotState.Idle;
                animTime = 0;
                endOfKickAnim = false;
                detectedEndAnim = false;
                gameObject.transform.position = new Vector3(idlePos.position.x, 0.1479751f, idlePos.position.z);

            }
        }
    }
    private void Rotate(float speed)
    {
        transform.Rotate(idlePos.position - rigidbodyPlayer.transform.position, speed);
    }
}
