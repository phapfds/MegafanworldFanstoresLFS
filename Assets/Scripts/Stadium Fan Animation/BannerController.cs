using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerController : MonoBehaviour
{
    public GameObject bannerHang;
    public GameObject bannerCarry;
    Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hanging"))
            bannerHang.SetActive(true);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Carrying"))
            bannerCarry.SetActive(true);
        else
        {
            bannerHang.SetActive(false);
            bannerCarry.SetActive(false);
        }

    }
}
