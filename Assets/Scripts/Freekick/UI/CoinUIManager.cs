using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class CoinUIManager : MonoBehaviour
{
    [Header("UI references")]
    [SerializeField] GameObject coinPrefap;
    [SerializeField] Transform target;
    [SerializeField] Transform avatar_1;
    [SerializeField] Transform avatar_2;


    [Space]
    [Header("Available coins : (coins to pool)")]
    [SerializeField] int maxCoins;
    Queue<GameObject> coinsQueue = new Queue<GameObject>();

    [Space]
    [Header("Animation settings")]
    [SerializeField] [Range(0.5f, 0.9f)] float minAnimDuration;
    [SerializeField] [Range(0.9f, 2f)] float maxAnimDuration;

    [SerializeField] Ease easeType;
    [SerializeField] float spread;

    Vector3 targetPosition;
    public FreeKickManagement freeKickManagement;
    void Awake()
    {
        targetPosition = target.position;
        PrepareCoins(avatar_2);
        //PrepareCoins(avatar_2);
    }

    void PrepareCoins(Transform avatar)
    {
        GameObject coin;
        for (int i = 0; i < maxCoins; i++)
        {
            coin = Instantiate(coinPrefap);
            coin.transform.parent = avatar;
            coin.SetActive(false);
            coinsQueue.Enqueue(coin);
        }
    }
    private void Animate(Vector3 position, int amount)
    {
        int completed = 0;
        for (int i = 0; i < amount; i++)
        {

            //check if there's coins in the pool
            if (coinsQueue.Count > 0)
            {
                //extract a coin from the pool
                GameObject coin = coinsQueue.Dequeue();
                coin.SetActive(true);
                //move coin to the collected coin pos
                coin.transform.position = position + new Vector3(Random.Range(-spread, spread), 0f, 0f);
                //animate coin to target position
                float duration = Random.Range(minAnimDuration, maxAnimDuration);
                coin.transform.DOMove(targetPosition, duration)
                .SetEase(easeType)
                .OnComplete(() =>
                {
                    //executes whenever coin reach target position
                    coin.SetActive(false);
                    coinsQueue.Enqueue(coin);
                    completed++;
                    if (completed == amount)
                        freeKickManagement.ChangeToIntroductionState();
                });
            }
        }
    }
    public void AddCoins()
    {
        Animate(avatar_1.position, 26);
        Animate(avatar_2.position, 25);
    }

}
