using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    [SerializeField] ChooseQualityLevel chooseQualityLevel;
    public void QualitySetting()
    {
        chooseQualityLevel.enabled = true;
    }
}
