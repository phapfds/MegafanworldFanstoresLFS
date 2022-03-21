using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoginManager : MonoBehaviour
{
    private void Awake()
    {
        ViewsManager.Instance.ChangeView(ViewType.LoginView);
    }
}

