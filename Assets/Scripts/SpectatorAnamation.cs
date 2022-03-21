using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorAnamation : MonoBehaviour
{
    IEnumerator Anim()
    {
        Animator animator = GetComponent<Animator>();
        switch (UserInfoManager.Instance.userInfo.club)
        {
            case Club.Berlin:
                animator.SetTrigger("Blue");
                break;
            case Club.Bremen:
                animator.SetTrigger("Green");
                break;
            case Club.Dortmund:
                animator.SetTrigger("Yelow");
                break;
            case Club.Dresden:
                animator.SetTrigger("Yelow");
                break;
            case Club.Frankfurt:
                animator.SetTrigger("FF");
                break;
            case Club.Freiburg:
                animator.SetTrigger("Red");
                break;
            case Club.Gelsenkirchen:
                animator.SetTrigger("Blue");
                break;
            case Club.Hamburg:
                animator.SetTrigger("White");
                break;
            case Club.HamburgPirates:
                animator.SetTrigger("Brown");
                break;
            case Club.Hannover:
                animator.SetTrigger("Green");
                break;
            case Club.Monchengladbach:
                animator.SetTrigger("White");
                break;
            case Club.Munchen:
                animator.SetTrigger("Blue");
                break;
            case Club.Nuremberg:
                animator.SetTrigger("Red");
                break;
            case Club.Wolfsburg:
                animator.SetTrigger("White");
                break;
            case Club.Neutral:
                animator.SetTrigger("Yelow");
                break;
            default:
                break;
        }
        yield return new WaitForSecondsRealtime(0);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    private void OnEnable()
    {
        StartCoroutine(Anim());
    }
}
