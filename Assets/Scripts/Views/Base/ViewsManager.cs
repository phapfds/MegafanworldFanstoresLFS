
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewsManager : Singleton<ViewsManager>
{
    [SerializeField] List<Views> views;
    public Dictionary<ViewType, Views> dicViews;
    public ViewType previousView;
    public Views currentView;
    private void Awake()
    {
        dicViews = new Dictionary<ViewType, Views>();
        foreach (Views e in views)
        {
            if (e != null)
            {
                var cv = Instantiate(e);
                cv.transform.SetParent(gameObject.transform);
                cv.gameObject.SetActive(false);
                dicViews.Add(e.viewType, cv);
            }
        }
        currentView = dicViews[ViewType.EmptyView];
        currentView.gameObject.SetActive(true);
    }
    public void ChangeView(ViewType newView)
    {

        previousView = currentView.viewType;
        currentView.gameObject.SetActive(false);
        currentView = dicViews[newView];
        if (newView != ViewType.LoginView && newView != ViewType.SignUpView && newView != ViewType.ForgotPassView && newView != ViewType.EmptyView && newView != ViewType.LoadingView)
        {
            ChatManager.inst.DisableChatSystem();
            ChatManager.inst.chatObject.transform.SetParent(currentView.transform);
            ChatManager.inst.chatObject.transform.localScale = Vector3.one;
            ChatManager.inst.EnableChatSystem();
        }
        currentView.gameObject.SetActive(true);
    }
    public void ChangeView(ViewType preView, ViewType newView)
    {

        previousView = preView;
        currentView.gameObject.SetActive(false);
        currentView = dicViews[newView];
        if (newView != ViewType.LoginView && newView != ViewType.SignUpView && newView != ViewType.ForgotPassView && newView != ViewType.EmptyView && newView != ViewType.LoadingView)
        {
            ChatManager.inst.DisableChatSystem();
            ChatManager.inst.chatObject.transform.SetParent(currentView.transform);
            ChatManager.inst.chatObject.transform.localScale = Vector3.one;
            ChatManager.inst.EnableChatSystem();
        }
        currentView.gameObject.SetActive(true);
    }
    public void LoadSceneByName(string name)
    {
        currentView.gameObject.SetActive(false);
        currentView = dicViews[ViewType.LoadingView];
        currentView.gameObject.SetActive(true);
        LoadingView loadingView = currentView as LoadingView;
        loadingView.LoadSceneByName(name);
    }
    public void NoView()
    {
        currentView.gameObject.SetActive(false);
    }
}
