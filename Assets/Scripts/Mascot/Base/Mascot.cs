using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public enum MascotState
{
    Idle,
    Attack,
    AttackWrongObj
}
public abstract class Mascot : Singleton<Mascot>
{
    public GameObject player;
    public MascotType mascotType;
    public Club mascotOfClub;
    public MascotState mascotState;

    [HideInInspector] public Vector3 aimFlag = new Vector3();
    [HideInInspector] public Flag aimPeopleWithFlag;

    private bool enableWrongAttack = false;
    public Action<Vector3> attackWrongPos;
    public abstract void IdleState();
    public virtual void AttackState(Vector3 aim)
    {
    }
    public virtual void AttackState(Flag flag)
    {
    }
    private void Awake()
    {
        OnAwake();
    }
    public virtual void OnAwake()
    {
        //Debug.LogError("Mascot awake");
        Flag.isAttacked += ChangeToAttackEnemy;
        mascotState = MascotState.Idle;
        enableWrongAttack = true;
    }
    private void Start()
    {
        IdleState();
        OnStart();
    }
    public virtual void OnStart()
    {

    }
    public void FixedUpdate()
    {
        OnUpdate();
        if (mascotState == MascotState.Idle)
        {
            IdleState();
        }
        else if (mascotState == MascotState.Attack)
        {
            AttackState(aimPeopleWithFlag);
        }
        else
        {
            AttackState(aimFlag);
        }
#if UNITY_STANDALONE_WIN
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() && enableWrongAttack && mascotState == MascotState.Idle && InGameManager.Instance.canAttack && InGameManager.Instance.IngameState == IngameState.Ingame)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
                string _tag = raycastHit.transform.gameObject.tag;
                if (!_tag.Contains("Flag") && !_tag.Contains("Player") && !_tag.Contains("Mascot") && !_tag.Contains("Boundary"))
                {
                    OnAttackWrongObject(raycastHit.point);
                    //Debug.LogError(raycastHit.transform.gameObject.name);
                }
            }
        }
#elif UNITY_ANDROID
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject() && mascotState == MascotState.Idle && InGameManager.Instance.canAttack && InGameManager.Instance.IngameState == IngameState.Ingame)
        {
            Debug.LogError(Input.touchCount);
            foreach (Touch touch in Input.touches)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out RaycastHit raycastHit) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    string _tag = raycastHit.transform.gameObject.tag;
                    if (_tag.Contains("Flag") && !_tag.Contains("Player") && !_tag.Contains("Mascot") && !_tag.Contains("Boundary"))
                    {
                        Flag flag = raycastHit.transform.gameObject.GetComponent<Flag>();
                        flag.OnTouch();
                    }
                }
            }
        }
#endif
    }

    public virtual void OnAttackWrongObject(Vector3 pos)
    {
        //Debug.LogError(pos);
        attackWrongPos?.Invoke(pos);
        mascotState = MascotState.AttackWrongObj;
        aimFlag = pos;
        InGameManager.Instance.timeAttack = 0;
    }
    public abstract void OnUpdate();
    public void ChangeToAttackEnemy(Flag flag)
    {
        if (mascotState != MascotState.Attack)
        {
            mascotState = MascotState.Attack;
            aimFlag = flag.transform.position;
            aimPeopleWithFlag = flag;
        }
    }
    private void OnDestroy()
    {
        m_ShuttingDown = false;
        m_Instance = null;
    }

}
