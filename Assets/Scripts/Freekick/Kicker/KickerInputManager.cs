using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//public enum TypeKickInput
//{
//    ForwardAndRightDirs,
//    UpDir
//}
public enum TypeKickInput
{
    ChooseDirections,
    Kick
}
public class KickerInputManager : MonoBehaviour
{
    public event EventHandler<TypeKickInput> Kick;
    public TypeKickInput typeKickInput;
    public Animator anim;
    private void Start()
    {
        typeKickInput = TypeKickInput.ChooseDirections;
        anim.GetComponent<Animator>();
    }
    public void Onreset()
    {
        typeKickInput = TypeKickInput.ChooseDirections;
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //{
        //    Kick?.Invoke(this, typeKickInput);
        //    if (typeKickInput == TypeKickInput.ChooseDirections)
        //        typeKickInput = TypeKickInput.Shoot;
        //    else
        //        typeKickInput = TypeKickInput.ChooseDirections;
        //}
        if (Input.GetKeyDown(KeyCode.Space) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && typeKickInput == TypeKickInput.ChooseDirections)
        {
            Kick?.Invoke(this, typeKickInput);
        }
        if (Input.GetKeyUp(KeyCode.Space) && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && typeKickInput == TypeKickInput.ChooseDirections)
        {
            typeKickInput = TypeKickInput.Kick;
            Kick?.Invoke(this, typeKickInput);
            typeKickInput = TypeKickInput.Kick;

        }
    }

}
