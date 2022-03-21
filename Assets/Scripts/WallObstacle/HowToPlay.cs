using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject roundOneTip;
    public GameObject roundTwoTip;
    private void OnEnable()
    {
        roundOneTip.SetActive(InGameManager.Instance.IngameType == IngameType.WalkingStreet);
        roundTwoTip.SetActive(InGameManager.Instance.IngameType == IngameType.OutsideStadium);
    }
}
