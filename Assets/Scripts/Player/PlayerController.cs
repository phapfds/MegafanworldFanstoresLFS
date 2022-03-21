using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    private float currentVelocity;
    public PlayerDataBinding playerDataBinding;
    private float speed;
    private GameObject securityPos;
    [SerializeField] AudioSource audioSource;
    [SerializeField] int speedMove;
    // Start is called before the first frame update
    private void Awake()
    {
        if (UserInfoManager.Instance.userInfo.club == Club.Munchen || UserInfoManager.Instance.userInfo.club == Club.Frankfurt)
        {
            speed = 1f;
        }
        else
            if (UserInfoManager.Instance.userInfo.club == Club.Hannover || UserInfoManager.Instance.userInfo.club == Club.Monchengladbach || UserInfoManager.Instance.userInfo.club == Club.Hannover)
        {
            speed = 0.5f;
        }
        else
        {
            speed = 1f;
        }
        audioSource = GetComponent<AudioSource>();

    }
    public void IsCatchedBySecurity(Transform trans)
    {
        securityPos = trans.gameObject;
        playerDataBinding.IsCatchedBySecurity = true;
        RotateToAim(trans.position); ;
    }

    private void Start()
    {
        Flag.isAttacked += RotateToAgentAndAttack;
        Mascot.Instance.attackWrongPos += RotateToWrongPosAndAttack;
        if (InGameManager.Instance.IngameType == IngameType.OutsideStadium)
        {
            //Debug.LogError("Start couroutine security");
            Security.catched += IsCatchedBySecurity;
        }
    }

    private void LateUpdate()
    {
        if (InGameManager.Instance.IngameState == IngameState.Ingame && InputManager.direction.magnitude > 0.1f)
        {

            if (Mascot.Instance.mascotType == MascotType.ObjectThrow && Mascot.Instance.mascotState != MascotState.Idle)
            {
                //Dont allow to move
            }

            else if (Mascot.Instance.mascotType == MascotType.SoccerBall && Mascot.Instance.mascotState != MascotState.Idle)
            {
                //Dont't allow to move
            }
            else
            {
                float rotationAngle = Vector3.SignedAngle(Vector3.forward, -InputManager.direction, Vector3.up);
                rotationAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref currentVelocity, Time.deltaTime*2);
                transform.rotation = Quaternion.Euler(new Vector3(0, rotationAngle, 0));
                characterController.Move(-InputManager.direction * Time.deltaTime * speedMove);
                playerDataBinding.Speed = speed;
                audioSource.mute = false;
            }
        }
        else
        {
            playerDataBinding.Speed = 0;
            audioSource.mute = true;
        }
        if (InGameManager.Instance.IngameState == IngameState.CatchedBySecurity)
            RotateToAim(securityPos.transform.position);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - InputManager.direction);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward);
    }
    public void RotateToAgentAndAttack(Flag fl)
    {
        RotateToAim(fl.transform.position);
        if (Mascot.Instance.mascotType == MascotType.SoccerBall)
        {
            playerDataBinding.Kick = true;
        }
        else
        if (Mascot.Instance.mascotType == MascotType.ObjectThrow)
        {
            playerDataBinding.Throw = true;
        }
    }
    public void RotateToWrongPosAndAttack(Vector3 aim)
    {
        RotateToAim(aim);
        if (Mascot.Instance.mascotType == MascotType.SoccerBall)
        {
            playerDataBinding.Kick = true;
        }
        else
        if (Mascot.Instance.mascotType == MascotType.ObjectThrow)
        {
            playerDataBinding.Throw = true;
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
        Security.catched -= IsCatchedBySecurity;

    }
    private void OnDestroy()
    {
        Flag.isAttacked -= RotateToAgentAndAttack;
        Security.catched -= IsCatchedBySecurity;
    }
}
