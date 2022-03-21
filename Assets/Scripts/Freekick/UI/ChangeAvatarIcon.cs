using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAvatarIcon : MonoBehaviour
{
    [SerializeField] Animator anim;
    public CoinUIManager coinUIManager;
    private void Start()
    {
        
    }
    public void ChangeAvatar()
    {
        anim.SetTrigger("Change");
    }
    public void FoundMatch()
    {
        anim.SetTrigger("Done");
    }
    public void AddCoinsAnim()
    {       
        coinUIManager.AddCoins();
    }
}
