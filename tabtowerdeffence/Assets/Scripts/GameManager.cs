using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int wave;
    public int waveCount;
    public int maxWaveCount = 4;

    public bool isWaveOver;
    public bool isGameOver;
    public bool isWin;


    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

    }


    public void GameOver()
    {
        isGameOver = true;
    }


    public void SpawnWave()
    {
        if (waveCount == maxWaveCount)
        {
            isWaveOver = true;
            wave++;
        }
            

        waveCount++;
    }

    public void StartWave()
    {
        isWaveOver = false;
    }
}
