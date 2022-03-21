using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewKickerController : MonoBehaviour
{
    public static NewKickerController ins;
    private KickerDataBiding dataBiding;
    [SerializeField] Transform ballPos;
    private Vector3 posOrigin;
    private Animator anim;
    private void Awake()
    {
        ins = this;
        posOrigin = transform.position;
        anim = GetComponent<Animator>();
        dataBiding = GetComponent<KickerDataBiding>();
        FreeKickManager.Ins.goal += GoalLine_Goal;
    }

    public void GoalLine_Goal(bool e)
    {
        if (e)
            dataBiding.GoalPar = true;
        else
            dataBiding.DefeatPar = true;
    }

    public void OnReset()
    {
        transform.position = posOrigin;
        anim.SetTrigger("Reset");
    }


    public void StartKickAnim()
    {
        dataBiding.KickPar = true;
    }

    public void EndOfKickAnim()
    {
        SwipeBall.Ins.ApplyForce();
    }

    private void Update()
    {
        if (dataBiding.KickState)
        {
            transform.position = Vector3.MoveTowards(transform.position, ballPos.position, Time.deltaTime * 2.5f);
        }
    }

}
