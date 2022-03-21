using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    public static UIResult Ins { get; private set; }
    [SerializeField] List<Image> yourScore;
    private void OnEnable()
    {
        FreeKickManager.Ins.goal += DisplayResult;
    }
    public void DisplayResult(bool result)
    {
        if (yourScore[FreeKickManager.Ins.kickNum - 1] != null)
            if (result)
                yourScore[FreeKickManager.Ins.kickNum - 1].color = Color.green;
            else
                yourScore[FreeKickManager.Ins.kickNum - 1].color = Color.red;

    }

    private void OnDisable()
    {
        foreach(Image img in yourScore)
        {
            if (img != null)
                img.color = Color.white;
        }
        FreeKickManager.Ins.goal -= DisplayResult;

    }

    #region "Old"
    //public void DisplayResult(List<Image> yourScore, bool result)
    //{
    //    int i;
    //    for (i = 0; i <= yourScore.Count - 1; i++)
    //    {
    //        Color _color = yourScore[i].color;
    //        if (_color != Color.red && _color != Color.green)
    //        {
    //            break;
    //        }
    //    }
    //    if (i <= yourScore.Count)
    //    {
    //        if (result)
    //        {
    //            yourScore[i].color = Color.green;
    //        }

    //        else
    //        {
    //            yourScore[i].color = Color.red;
    //        }
    //    }
    //}
    #endregion
}
