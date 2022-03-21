using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupManager : MonoBehaviour
{
    public static ViewType currentView = ViewType.SetupView;
    private void OnEnable()
    {
        ViewsManager.Instance.ChangeView(ViewType.SetupView, currentView);
    }
    public static void SetUpView(ViewType loadView)
    {
        currentView = loadView;
    }
    private void OnDisable()
    {
        currentView = ViewType.SetupView;
    }
}
