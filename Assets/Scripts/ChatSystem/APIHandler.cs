using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class APIHandler : MonoBehaviour
{
    public static APIHandler Instance;
    public DatabaseAPI databaseAPI;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        databaseAPI = GetComponent<DatabaseAPI>();
    }

}
