using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerController : MonoBehaviour
{
    public static KickerController ins;
    private KickerDataBiding dataBiding;
    private KickerInputManager inputManager;
    public Transform ballPos;
    private ColliderFit colliderFit;
    public BallPower ball;
    private Vector3 posOrigin;
    private void Awake()
    {
        ins = this;
        posOrigin = transform.position;
    }
    void Start()
    {
        dataBiding = GetComponent<KickerDataBiding>();
        inputManager = GetComponent<KickerInputManager>();
        colliderFit = GetComponentInChildren<ColliderFit>();
        inputManager.Kick += Kick;
        ball.IsKicked += Ball_IsKicked;
    }

    public void GoalLine_Goal(object sender, bool e)
    {
        if (e)
            dataBiding.GoalPar = true;
        else
            dataBiding.DefeatPar = true;
    }

    public void OnReset()
    {
        transform.position = posOrigin;
    }
    private void Ball_IsKicked(object sender, System.EventArgs e)
    {
        colliderFit.enabled = false;
    }

    private void Kick(object sender, TypeKickInput e)
    {
        if (e == TypeKickInput.Kick)
        {
            dataBiding.KickPar = true;
            colliderFit.enabled = true;
        }

    }

    private void Update()
    {
        if (dataBiding.KickState)
        {
            transform.position = Vector3.MoveTowards(transform.position, ballPos.position, Time.deltaTime * 2.5f);
        }
    }

}
