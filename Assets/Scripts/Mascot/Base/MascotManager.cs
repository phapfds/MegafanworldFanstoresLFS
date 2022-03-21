using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum MascotType
{
    ObjectThrow,
    Eagle,
    Animal,
    SoccerBall,
}
public class MascotManager : MonoBehaviour
{

    [SerializeField] List<Mascot> mascots;
    public Dictionary<Club, Mascot> dicMascot;
    private void Start()
    {
        if(InGameManager.Instance.IngameType == IngameType.WalkingStreet)
        {
            dicMascot = new Dictionary<Club, Mascot>();
            foreach (Mascot m in mascots)
            {
                dicMascot.Add(m.mascotOfClub, m);
            }
            dicMascot.Add(Club.Monchengladbach, dicMascot[Club.Hannover]);
            dicMascot.Add(Club.Neutral, dicMascot[Club.Hannover]);
            dicMascot[UserInfoManager.Instance.userInfo.club].gameObject.SetActive(true);
        }
    }
}
