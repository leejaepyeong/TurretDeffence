using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int gold;

    public int wave = 1;
    public int waveCount;
    public int maxWaveCount = 1;
    public int level = 0;
    private int repairCost = 100;

    float gameSpeed = 1;

    public bool isBossSpawn;
    public bool isUpgrade;
    public bool isWaveOver;
    public bool isGameOver;
    public bool isWin;
    bool isSpeed = false;

    public Text goldTxt;

    public GameObject goldEffect;
    public Transform goldPos;
    public GameObject UpgradePanel;
    public GameObject[] upgradeOnOff;   //  0 : On / 1 : Off

    public GameObject optionPanel;

    public GameObject waveInfoPanel;
    public Image waveInfoImg;
    public Text waveTxt;
    public Text monsterHpTxt;
    public Text monsterAttTxt;
    public Text monsterDpTxt;
    public GameObject bossImg;
    public Text currentSpeedTxt;
    public Text repaitCostTxt;

    public GameObject gameOverPanel;
    public GameObject speedPanel;

    public Player player;
    public Spawn spawn;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }

        goldTxt.text = gold.ToString();

        Invoke("StartWave", 1f);

    }

    // Upgrade gold cost Use
    public void UpgradeCost(int _cost)
    {
        gold -= _cost;

        goldTxt.text = gold.ToString();
    }

    //  Upgrade panel On /Off
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

    // Player Get Gold by Kill Enemy or Tab
    public void GetGold()
    {
        gold += 3;
        goldTxt.text = gold.ToString();
        StartCoroutine(GoldUp());
        
    }

    // Player Gold Increase
    IEnumerator GoldUp()
    {
        GameObject goldAnim = Instantiate(goldEffect, goldPos.position, goldPos.rotation);
        yield return new WaitForSeconds(0.6f);
        Destroy(goldAnim);
    }

    // Option Button (On / Off)
    public void OptionBtn()
    {
        Time.timeScale = 0;
        optionPanel.SetActive(true);
    }

    // option Menu (Resume Game / Go Title / Game Quit)
    public void OptionMenuBtn(int num)
    {
        switch(num)
        {
            case 0:
                Time.timeScale = gameSpeed;
                optionPanel.SetActive(false);
                break;
            case 1:
                Time.timeScale = 1;
                SceneManager.LoadScene(0);  // Title Scene
                break;
            case 2:
                Application.Quit();
                break;
        }    
    }    

    // Show Monster Information in Wave
    public void ShowWaveInfo(Enemy _enemy)
    {
        if (isBossSpawn)
            bossImg.SetActive(true);
        else
            bossImg.SetActive(false);

        waveTxt.text = "Wave " + wave;
        waveInfoImg.sprite = _enemy.image;

        if(!isBossSpawn)
        {
            monsterHpTxt.text = (_enemy.MaxHp + 8 * GameManager.instance.level).ToString();
            monsterDpTxt.text = (_enemy.deffence + 2 * GameManager.instance.level).ToString();
            monsterAttTxt.text = (_enemy.damage + 1 * GameManager.instance.level).ToString();
        }
        else
        {
            monsterHpTxt.text = (_enemy.MaxHp + 320 * ((GameManager.instance.level) / 4)).ToString();
            monsterDpTxt.text = (_enemy.deffence + 24 * ((GameManager.instance.level) / 4)).ToString();
            monsterAttTxt.text = (_enemy.damage + 12 * ((GameManager.instance.level) / 4)).ToString();
        }
        
            

        StartCoroutine(WaveInfo());
    }

    IEnumerator WaveInfo()
    {
        waveInfoPanel.SetActive(true);

        yield return new WaitForSeconds(5f);

        waveInfoPanel.SetActive(false);
    }

    // Game Over
    public void GameOver()
    {
        isGameOver = true;

        Invoke("GameOverOn", 3f);
    }

    private void GameOverOn()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
    }

    // gameover menu (0 : Retry / 1 : Title / 2 : Quit)
    public void GameOverMenuBtn(int num)
    {
        switch (num)
        {
            case 0:
                Time.timeScale = 1;
                SceneManager.LoadScene(1);
                break;
            case 1:
                Time.timeScale = 1;
                SceneManager.LoadScene(0);  // Title Scene
                break;
            case 2:
                Application.Quit();
                break;
        }
    }

    // Check Spawn 
    public void SpawnWave()
    {
        if (waveCount == maxWaveCount || isBossSpawn)
        {
            waveCount = 0;
            isBossSpawn = false;
            isWaveOver = true;
            wave++;
            if(wave % 5 == 0)
            {
                isBossSpawn = true;
                level++;

            }

            Invoke("StartWave", 18f);

            return;
        }
            

        waveCount++;
    }


    // Start Spawn
    public void StartWave()
    {
        isWaveOver = false;


        ShowWaveInfo(isBossSpawn ? spawn.bossPrefabs[((GameManager.instance.wave) / 5 - 1) % 4] : spawn.enemyPrefabs[(GameManager.instance.wave - 1) % 5]);


    }

    public void SpeedPanelBtn()
    {
        isSpeed = !isSpeed;

        speedPanel.SetActive(isSpeed);
    }

    // Game Speed Control
    public void SpeedBtn(int num)
    {
        switch(num)
        {
            case 0:
                gameSpeed = 1;
                break;
            case 1:
                gameSpeed = 1.5f;
                break;
            case 2:
                gameSpeed = 2;
                break;
        }

        Time.timeScale = gameSpeed;

        currentSpeedTxt.text = "X " + gameSpeed;

        isSpeed = false;
        speedPanel.SetActive(isSpeed);
    }

    // Repair Castle
    public void RepairBtn()
    {
        if (gold < repairCost) return;

        if (player.currentHp == player.maxHp) return;

        gold -= repairCost;

        goldTxt.text = gold.ToString();

        player.currentHp = player.maxHp;


        repairCost += 50;
        repaitCostTxt.text = repairCost + " Gold";


    }
}
