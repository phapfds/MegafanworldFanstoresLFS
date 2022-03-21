using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;
    private int kickHash;
    private int throwHash;
    private GameObject aimObject;
    private Vector3 aimPos;
    public bool Kick
    {
        set
        {
            if (value)
                anim.SetTrigger(kickHash);
        }
    }
    public bool Throw
    {
        set
        {
            if (value)
                anim.SetTrigger(throwHash);
        }
    }
    private void Awake()
    {
        kickHash = Animator.StringToHash("Kick");
        throwHash = Animator.StringToHash("Throw");

    }
    private void Start()
    {
        if (InGameManager.Instance.IngameType == IngameType.WalkingStreet)
        {
            Flag.isAttacked += RotateToAgentAndAttack;
            Mascot.Instance.attackWrongPos += RotateToWrongPosAndAttack;
        }
    }
    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Throw") || anim.GetCurrentAnimatorStateInfo(0).IsName("Kick"))
        {
            if (aimObject != null)
                RotateToAim(aimObject.transform.position);
            else
                RotateToAim(aimPos);
        }
    }
    public void RotateToAgentAndAttack(Flag fl)
    {
        RotateToAim(fl.transform.position);
        aimObject = fl.gameObject;
        if (Mascot.Instance.mascotType == MascotType.SoccerBall)
        {
            Kick = true;
        }
        else
        if (Mascot.Instance.mascotType == MascotType.ObjectThrow)
        {
            Throw = true;
        }
    }
    public void RotateToWrongPosAndAttack(Vector3 aim)
    {
        RotateToAim(aim);
        aimPos = aim;
        if (Mascot.Instance.mascotType == MascotType.SoccerBall)
        {
            Kick = true;
        }
        else
        if (Mascot.Instance.mascotType == MascotType.ObjectThrow)
        {
            Throw = true;
        }
    }
    public void RotateToAim(Vector3 aim)
    {
        Vector3 dir = aim - transform.position;
        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Euler(new Vector3(0, q.eulerAngles.y, 0));
        }
    }
    private void OnApplicationQuit()
    {
        Flag.isAttacked -= RotateToAgentAndAttack;

    }
    private void OnDestroy()
    {
        Flag.isAttacked -= RotateToAgentAndAttack;
    }
}
