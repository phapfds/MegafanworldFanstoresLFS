using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LoadingView : Views
{
    [SerializeField] Text percentLoaded;
    [SerializeField] Image progressBar;

    [SerializeField] List<Image> backgroundOnboarding;
    [SerializeField] Text score;
    [SerializeField] Image scoreEndgame;

    //[SerializeField] Image goingToStadium;
    //[SerializeField] Text goingToStadiumText;
    //[SerializeField] Image backgroundTransparent;
    public void LoadSceneByName(string scene)
    {
        if (this.gameObject.activeSelf)
            StartCoroutine(LoadScene(scene));
    }
    IEnumerator LoadScene(string scene)
    {
        yield return new WaitForSeconds(0.3f);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);
        asyncOperation.allowSceneActivation = false;
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            percentLoaded.text = (asyncOperation.progress * 100).ToString("0") + "%";
            progressBar.fillAmount = asyncOperation.progress;

            if (asyncOperation.progress >= 0.9f)
            {
                if (progressBar.fillAmount < 100)
                    progressBar.fillAmount += 0.1f;
                else
                    progressBar.fillAmount = 100;
                percentLoaded.text = (progressBar.fillAmount * 100).ToString("0") + "%";
                asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    public override void SetUp()
    {
        progressBar.fillAmount = 0f;
        percentLoaded.text = (progressBar.fillAmount * 100).ToString("0") + "%";
        base.SetUp();
        //if (HighscoreTable.timeEnd)
        //{
        //    score.text = "Your score is " + ScoreManager.score;
        //    //scoreEndgame.gameObject.SetActive(true);
        //}
        //if (ScoreManager.score >= 100 && InGameManager.loadLevel2)
        //{
        //    goingToStadium.gameObject.SetActive(true);
        //    goingToStadiumText.text = "Congratulation! Your score is more than 10.000 points. Let's go to stadium now.";
        //    backgroundTransparent.gameObject.SetActive(true);
        //    InGameManager.loadLevel2 = false;
        //}
        //else
        backgroundOnboarding[Random.Range(0, backgroundOnboarding.Count)].gameObject.SetActive(true);

    }
    private void OnDisable()
    {
        foreach (Image image in backgroundOnboarding)
        {
            image.gameObject.SetActive(false);
        }
        if (scoreEndgame.gameObject.activeSelf)
            scoreEndgame.gameObject.SetActive(false);
        //if (goingToStadium.gameObject.activeSelf)
        //    goingToStadium.gameObject.SetActive(false);
        //backgroundTransparent.gameObject.SetActive(false);


    }
}
