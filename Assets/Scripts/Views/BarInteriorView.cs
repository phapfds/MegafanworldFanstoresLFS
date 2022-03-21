using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BarInteriorView : Views
{
    [SerializeField] GameObject playerDrinkingCamTRPrefab;
    private GameObject playerDrinkingCamTR;
    [SerializeField] Image questionToGetMoreCredits;
    [SerializeField] Image questionToBuyABeer;
    [SerializeField] Text creditsNum;

    [SerializeField] Image timeProgressBar;
    [SerializeField] Text timeNow;
    [SerializeField] Text timePlay;
    [SerializeField] Text score;

    public List<GameObject> gameObjects = new List<GameObject>();
    private bool autoBuy;
    private void OnDisable()
    {
        if (playerDrinkingCamTR != null)
            Destroy(playerDrinkingCamTR);
        foreach(GameObject gb in gameObjects)
        {
            gb.SetActive(true);
        }
        gameObjects.Clear();
    }
    public override void SetUp()
    {
        foreach (GameObject gb in InGameManager.Instance.gameObjects)
        {
            if (gb != null && gb.activeSelf == true)
            {
                gameObjects.Add(gb);
                gb.SetActive(false);
            }
        }
        questionToGetMoreCredits.gameObject.SetActive(false);
        questionToBuyABeer.gameObject.SetActive(true);
        playerDrinkingCamTR = Instantiate(playerDrinkingCamTRPrefab);
        creditsNum.text = UserInfoManager.Instance.userInfo.coinsNum + " credits";

        timeNow.text = InGameManager.Instance.timeNow.ToString("0.0");
        timePlay.text = InGameManager.Instance.timeToPlay.ToString();
        timeProgressBar.fillAmount = InGameManager.Instance.timeNow / InGameManager.Instance.timeToPlay;
        score.text = ScoreManager.score.ToString();

        if (autoBuy)
        {
            autoBuy = false;
            StartCoroutine(DrinkBeerCoroutine());
        }
    }
    public void AnswerYesToBuy()
    {
        if (UserInfoManager.Instance.userInfo.coinsNum >= 1)
        {
            StartCoroutine(DrinkBeerCoroutine());
        }
        else
        {
            //InGameManager.Instance.IngameState = IngameState.IAP;
            questionToBuyABeer.gameObject.SetActive(false);
            questionToGetMoreCredits.gameObject.SetActive(true);
        }
    }
    IEnumerator DrinkBeerCoroutine()
    {
        questionToBuyABeer.gameObject.SetActive(false);
        InGameManager.Instance.IngameState = IngameState.DrinkBeer;
        UserInfoManager.Instance.userInfo.coinsNum -= 1;
        UserInfoManager.Instance.SaveData();
        creditsNum.text = UserInfoManager.Instance.userInfo.coinsNum + " credits";
        SetTriggerDrinkAnim.TriggerDrink();
        yield return new WaitForSecondsRealtime(15);
        InGameManager.Instance.timeToPlay += InGameManager.Instance.timeBonus;


        //var igView = ViewsManager.Instance.dicViews[ViewType.InGameView] as InGameView;
        //igView.credits.text = UserInfoManager.Instance.userInfo.coinsNum.ToString() + " credits";
        InGameManager.Instance.IngameState = IngameState.Ingame;
    }
    public void AnserNoToBuy()
    {
        questionToBuyABeer.gameObject.SetActive(false);
        InGameManager.Instance.IngameState = IngameState.Ingame;
    }

    public void MoveToIAP()
    {
        autoBuy = true;
        questionToGetMoreCredits.gameObject.SetActive(false);
        InGameManager.Instance.IngameState = IngameState.IAP;

    }
    public void CancelBuyCredit()
    {
        questionToBuyABeer.gameObject.SetActive(false);
        InGameManager.Instance.IngameState = IngameState.Ingame;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ShowNotice(Image notice)
    {
        notice.gameObject.SetActive(true);
    }
    public void OffNotice(Image notice)
    {
        notice.gameObject.SetActive(false);
    }
}
