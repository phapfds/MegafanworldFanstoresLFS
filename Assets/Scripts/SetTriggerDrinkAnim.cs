using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTriggerDrinkAnim : MonoBehaviour
{
    private static Animator anim;
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }
    public static void TriggerDrink()
    {
        anim.SetTrigger("Drink");
    }
 
}
