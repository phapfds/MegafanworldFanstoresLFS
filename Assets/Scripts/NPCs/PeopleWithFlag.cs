using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleWithFlag : WalkingPeople
{
    [SerializeField] Animator anim;
    [SerializeField] Transform flag;
    public override void OnAwake()
    {
        base.OnAwake();
        flag = gameObject.GetComponentInChildren<Flag>().gameObject.transform;
        flag.GetComponent<Flag>().flagPositionType = FlagPositionType.WalkingPeopleFlag;
        flag.GetComponent<Flag>().StopDestroy();
        if (anim == null)
            anim = gameObject.GetComponent<Animator>();
    }
    private bool RunWithFlag
    {
        set
        {
            anim.SetBool("RunWithFlag", value);
        }
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        IsTakeFlag(flag);
    }
    public void IsTakeFlag(Transform flag)
    {
        RunWithFlag = flag.gameObject.activeSelf;
    }
}
