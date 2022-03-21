using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorOfFlag : MonoBehaviour
{
    [SerializeField] Texture redImage;
    [SerializeField] Texture blueImage;
    [SerializeField] Material enemyMat;
    [SerializeField] Material friendMat;
    private void Awake()
    {
        //if (UserInfoManager.Instance.userInfo.club == Club.RedTeam)
        //{
        //    friendMat.mainTexture = redImage;
        //    enemyMat.mainTexture = blueImage;

        //}
        //else
        //{
        //    friendMat.mainTexture = blueImage;
        //    enemyMat.mainTexture = redImage;
        //};
    }
}
