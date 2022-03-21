using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.PostProcessing;

public enum ArrowDirType
{
    Pause,
    LeftToRight,
    RightToLeft
}
public class Arrow : MonoBehaviour
{
    public KickerInputManager kicker;
    public static Vector2 direction;
    private double angle;
    public float angleLimit = 40;
    public ArrowDirType currentDir;

    [Range(0, 1)]
    public float speed;
    private void Start()
    {
        kicker.Kick += Kicker_Kick;
        OnReset();
    }

    private void Kicker_Kick(object sender, TypeKickInput e)
    {
        if (e == TypeKickInput.ChooseDirections)
            currentDir = ArrowDirType.Pause;
    }

    public void OnReset()
    {
        angle = 0;
        currentDir = ArrowDirType.LeftToRight;

    }
    void FixedUpdate()
    {
        if (currentDir == ArrowDirType.LeftToRight)
            angle += speed;
        else
            if (currentDir == ArrowDirType.RightToLeft)
            angle -= speed;


        transform.rotation = Quaternion.Euler(90, (float)angle, 0);
        if (angle >= angleLimit)
            currentDir = ArrowDirType.RightToLeft;
        if (angle <= -angleLimit)
            currentDir = ArrowDirType.LeftToRight;
        direction = new Vector2((float)Math.Sin((angle * Math.PI) / 180), (float)Math.Cos((angle * Math.PI) / 180));
    }
    private void OnEnable()
    {
        OnReset();
    }

}
