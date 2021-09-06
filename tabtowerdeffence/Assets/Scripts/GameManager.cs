using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold;

    public int wave;
    public int waveCount;
    public int maxWaveCount = 4;


    public bool isBossSpawn;
    public bool isUpgrade;
    public bool isWaveOver;
    public bool isGameOver;
    public bool isWin;

    public Text goldTxt;

    public GameObject goldEffect;
    public Transform goldPos;
    public GameObject UpgradePanel;
    public GameObject[] upgradeOnOff;   //  0 : On / 1 : Off
    public GameObject optionPanel;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        goldTxt.text = gold.ToString();

        Invoke("StartWave", 1f);

    }

    public void UpgradeCost(int _cost)
    {
        gold -= _cost;

        goldTxt.text = gold.ToString();
    }


    public void UpgradeBtn()
    {
        isUpgrade = !isUpgrade;

        upgradeOnOff[0].SetActive(false);
        upgradeOnOff[1].SetActive(false);

        UpgradePanel.SetActive(isUpgrade);

        if (isUpgrade)
            upgradeOnOff[1].SetActive(true);
        else
            upgradeOnOff[0].SetActive(true);
    }

    public void GetGold()
    {
        gold++;
        goldTxt.text = gold.ToString();
        StartCoroutine(GoldUp());
        
    }

    IEnumerator GoldUp()
    {
        GameObject goldAnim = Instantiate(goldEffect, goldPos.position, goldPos.rotation);
        yield return new WaitForSeconds(0.6f);
        Destroy(goldAnim);
    }

    public void OptionBtn()
    {
        Time.timeScale = 0;
        optionPanel.SetActive(true);
    }

    public void OptionMenuBtn(int num)
    {
        switch(num)
        {
            case 0:
                Time.timeScale = 1;
                optionPanel.SetActive(false);
                break;
            case 1:
                SceneManager.LoadScene(0);  // Title Scene
                break;
            case 2:
                Application.Quit();
                break;
        }    
    }    

    public void GameOver()
    {
        isGameOver = true;
    }


    public void SpawnWave()
    {
        if (waveCount == maxWaveCount || isBossSpawn)
        {
            isBossSpawn = false;
            isWaveOver = true;
            wave++;
            if(wave % 5 == 0)
            {
                isBossSpawn = true;
            }

            Invoke("StartWave", 15f);
        }
            

        waveCount++;
    }

    public void StartWave()
    {
            isWaveOver = false;
    }
}
