using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectatorManager : MonoBehaviour
{
    [SerializeField] GameObject spectator2D;
    [SerializeField] GameObject left;
    [SerializeField] bool left_;
    [SerializeField] GameObject right;
    [SerializeField] bool right_;
    [SerializeField] GameObject center;
    [SerializeField] bool center_;

    private void Awake()
    {
        spectator2D.SetActive(true);
        left_ = false;
        right_ = false;
        center_ = false;
    }

    private void Update()
    {
        if (InGameManager.Instance.IngameState == IngameState.DrinkBeer || InGameManager.Instance.IngameState == IngameState.EnterKeyport)
        {
            if (spectator2D.activeSelf)
                spectator2D.SetActive(false);
            if (left != null)
                if (left.activeSelf)
                {
                    left.SetActive(false);
                    left_ = true;
                }
            if (right != null)
                if (right.activeSelf)
                {
                    right.SetActive(false);
                    right_ = true;
                }
            if (center != null)
                if (center.activeSelf)
                {
                    center.SetActive(false);
                    center_ = true;
                }

        }
        else
        {
            if (!spectator2D.activeSelf)
                spectator2D.SetActive(true);
            if (left != null)
                if (left_)
                {
                    left.SetActive(true);
                    left_ = false;
                }
            if (right != null)
                if (right_)
                {
                    right.SetActive(true);
                    right_ = false;
                }
            if (center != null)
                if (center_)
                {
                    center.SetActive(true);
                    center_ = false;
                }
        }
    }
}
