using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoger : MonoBehaviour
{
    private static GameLoger instance;

    public string pemain;
    public int indexSoal;
    public string schema;
    public float z1;
    public float z2;
    public float z3;
    public float z4;
    public float difficulty;
    public string status;

    public float prevDifficulty;
    public float prevSuccessDifficulty;
    public float prevPerformance;

    public float targetDifficulty;

    public string prevStatus;
    public float currentSum;
    public float prevSum;

    void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }
}
