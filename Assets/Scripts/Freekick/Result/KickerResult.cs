using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickerResult : MonoBehaviour
{
    public static KickerResult inst;
    public FreeKickManager freeKickManager;
    public UIResult UI;
    private void Awake()
    {
        inst = this;
    }
    public void GoalLine_Goal(object sender, bool e)
    {
        //if (FreeKickManager.Ins.curr == FreeKickState.Kicker)
        //    UI.DisplayResult(UI.yourScore, e);
        //else

        //    UI.DisplayResult(UI.opponentScore, e);
    }
}

