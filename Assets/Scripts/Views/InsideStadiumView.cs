using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InsideStadiumView : Views
{
    [SerializeField] GameObject eatingCamTRPrefab;
    [SerializeField] GameObject eatingCamTRFrankfurtPrefab;
    private GameObject eatingCamTR;
    [SerializeField] GameObject playerEatingTRPrefab;
    [SerializeField] GameObject playerEatingTRPrefabFrankfurt;
    private GameObject playerEatingTR;
    [SerializeField] Image questionToGetMoreCredits;
    [SerializeField] Image questionToBuyABeer;

    [SerializeField] Text creditsNum;
    [SerializeField] Image timeProgressBar;
    [SerializeField] Text timeNow;
    [SerializeField] Text timePlay;
    [SerializeField] Text score;

    private bool autoBuy;
    private void OnDisable()
    {
        if (playerEatingTR != null)
        {
            Destroy(playerEatingTR);
            Destroy(eatingCamTR);
        }
    }
    public override void SetUp()
    {
        questionToGetMoreCredits.gameObject.SetActive(false);
        questionToBuyABeer.gameObject.SetActive(true);
        if (SelectCityView.city == City.Munich)
        {
            playerEatingTR = Instantiate(playerEatingTRPrefab);
            eatingCamTR = Instantiate(eatingCamTRPrefab);
        }

        if (SelectCityView.city == City.Frankfurt)
        {
            playerEatingTR = Instantiate(playerEatingTRPrefabFrankfurt);
            eatingCamTR = Instantiate(eatingCamTRFrankfurtPrefab);
        };

        creditsNum.text = UserInfoManager.Instance.userInfo.coinsNum + " credits";

        timeNow.text = InGameManager.Instance.timeNow.ToString("0.0");
        timePlay.text = InGameManager.Instance.timeToPlay.ToString();
        timeProgressBar.fillAmount = InGameManager.Instance.timeNow / InGameManager.Instance.timeToPlay;
        score.text = ScoreManager.score.ToString();

        if (autoBuy)
        {
            autoBuy = false;
            StartCoroutine(EatingBratwurst());
        }
    }
    public void AnswerYesToBuy()
    {
        if (UserInfoManager.Instance.userInfo.coinsNum >= 1)
        {
            StartCoroutine(EatingBratwurst());
        }
        else
        {
            //InGameManager.Instance.IngameState = IngameState.IAP;
            questionToBuyABeer.gameObject.SetActive(false);
            questionToGetMoreCredits.gameObject.SetActive(true);
        }
    }
    IEnumerator EatingBratwurst()
    {
        InGameManager.Instance.DisableObjects();
        eatingCamTR.GetComponent<EatingCamera>().enabled = true;
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
        InGameManager.Instance.EnableObjects();

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
