using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChatZoneController : MonoBehaviour
{
    [SerializeField] private bool isHiding;
    [SerializeField] ScrollRect scrollRect;

    public RectTransform rect;
    [Range(0f, 1f)] public float speed = 0.5f;

    public Image hideButton;
    public Image showButton;

    [SerializeField] Transform hidePosition;
    [SerializeField] Transform openPosition;
    private void Start()
    {
        //rect = GetComponent<RectTransform>();
        isHiding = false;
        StartCoroutine(ScrollToBottom());

        HideButton();
    }

    public void HideButton()
    {
        if(Time.timeScale == 1)
        {
            isHiding = !isHiding;
            if (!isHiding)
            {
                //rect.DOMoveX(rect.position.x + 727, 0.1f);
                rect.DOMoveX(openPosition.position.x, 0.1f);
                showButton.gameObject.SetActive(true);
                hideButton.gameObject.SetActive(false);
            }
            else
            {
                //rect.DOMoveX(rect.position.x - 727, 0.1f);
                rect.DOMoveX(hidePosition.position.x, 0.1f);
                showButton.gameObject.SetActive(false);
                hideButton.gameObject.SetActive(true);
            }
        }
    }
    IEnumerator ScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}
