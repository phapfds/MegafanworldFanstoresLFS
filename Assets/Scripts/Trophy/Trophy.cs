using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour
{
    public TrophyType trophyType;
    private readonly int score = 1000;

    //IEnumerator  Start()
    //{
    //    yield return new WaitForSeconds(15);
    //    Destroy(gameObject);
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Contains("Player"))
        {
            StartCoroutine(OnPlayerEnter());
        }
    }
    public void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
    IEnumerator OnPlayerEnter()
    {
        yield return new WaitForSeconds(0.2f);
        var ef = FlagManager.Instance.dicEffect[FlagType.Enemy];
        ef.transform.position = transform.position + Vector3.up;
        ef.SetActive(true);
        ScoreManager.score += score;
        Flag.score?.Invoke();
        if (TrophySpawnManager.Ins != null)
            TrophySpawnManager.Ins.currenItems.Remove(this);
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        if (TrophySpawnManager.Ins != null)
            TrophySpawnManager.Ins.StartCoroutine(TrophySpawnManager.Ins.End());
    }
}
