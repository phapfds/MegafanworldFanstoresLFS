using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum FlagType
{
    Enemy,
    Friend,
}
public enum FlagPositionType
{
    WindowFlag,
    WalkingPeopleFlag,
    BikeFlag,
    BullFlag,
    BirdFlag,
    HighBirdFlag,
    USSpecialEnemyFlag
}
public class FlagManager : Singleton<FlagManager>
{
    [SerializeField] List<Flag> windowFlagPrefab;
    private Dictionary<FlagType, Queue<Flag>> dicWindowFlagsPool;

    [SerializeField] List<GameObject> effect;
    public Dictionary<FlagType, GameObject> dicEffect;
    private void Awake()
    {
        dicWindowFlagsPool = new Dictionary<FlagType, Queue<Flag>>();
        dicEffect = new Dictionary<FlagType, GameObject>();
        dicWindowFlagsPool.Add(FlagType.Enemy, new Queue<Flag>());
        dicWindowFlagsPool.Add(FlagType.Friend, new Queue<Flag>());
        if (windowFlagPrefab.Count > 0)
        {
            foreach (Flag e in windowFlagPrefab)
            {
                for (int i = 0; i <= 15; i++)
                {
                    var flag = Instantiate(e, Vector3.zero, Quaternion.Euler(0, 50, 0));
                    flag.gameObject.SetActive(false);
                    dicWindowFlagsPool[e.flagType].Enqueue(flag);
                }
            }
        }



        if (effect.Count > 0)
        {
            GameObject ob_0 = Instantiate(effect[0]);
            GameObject ob_1 = Instantiate(effect[1]);
            dicEffect.Add(FlagType.Enemy, ob_0);
            dicEffect.Add(FlagType.Friend, ob_1);
        }

    }

    public void SpawnWindowFlag(Vector3 pos)
    {
        int flagType = UnityEngine.Random.Range((int)FlagType.Enemy, (int)FlagType.Friend + 1);
        Flag flag = dicWindowFlagsPool[(FlagType)flagType].Dequeue();
        flag.transform.position = pos;
        flag.gameObject.SetActive(true);
    }


    public void DestroyWindowFlag(Flag flag)
    {
        flag.gameObject.SetActive(false);
        dicWindowFlagsPool[flag.flagType].Enqueue(flag);
    }

    public IEnumerator DestroyAndSpawnNewPeopleFlag(Transform flag)
    {
        flag.gameObject.SetActive(false);
        Mascot.Instance.mascotState = MascotState.Idle;
        yield return new WaitForSeconds(10);
        flag.gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        m_ShuttingDown = false;
        m_Instance = null;
    }
    private void OnApplicationQuit()
    {
        m_ShuttingDown = false;
        m_Instance = null;
    }
}
