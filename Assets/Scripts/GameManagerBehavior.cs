using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerBehavior : MonoBehaviour {
    public Text goldLabel;
    private int gold;
    public int Gold
    {
        get
        {
            return gold;
        }
        set
        {
            gold = value;
            goldLabel.GetComponent<Text>().text = "GOLD: " + gold;
        }
    }
    public Text waveLabel;//宣告波次標籤文字
    public GameObject[] nextWaveLabels;//宣告下一波波次標籤
    public bool gameOver = false;//宣告遊戲結束旗標並設定為否
    private int wave;//宣告波次數
    public int Wave//宣告波次數的公用版
    {
        get
        {
            return wave;//回傳值給外部
        }
        set
        {
            wave = value;//讀取外部的值
            if (!gameOver)//如果遊戲沒有結束
            {
                for (int i = 0; i < nextWaveLabels.Length; i++)//重複做兩次波次變換動畫
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                    //下一波波次標籤的動畫設定觸發器為"nextWave"
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);//波次標籤的文字設定為"WAVE: " + (波次 + 1)
        }
    }
    public Text healthLabel;//宣告玩家生命值標籤文字
    public GameObject[] healthIndicator;//宣告玩家生命指標物件陣列(綠色小怪獸)
    private int health;//宣告私有玩家生命值
    public int Health//宣告公有玩家生命值
    {
        get
        {
            return health;//回傳值給外部
        }
        set
        {
            // 1
            if (value < health)//如果外部讀取值小於玩家生命值
            {
                Camera.main.GetComponent<CameraShake>().Shake();
                //觸發相機的主物件的相機震動腳本的搖動
            }
            // 2
            health = value;//玩家生命值設定為外部讀取值
            healthLabel.text = "HEALTH: " + health;//玩家生命值標籤的文字設定為"HEALTH: "+玩家生命值
            // 3
            if (health <= 0 && !gameOver)//如果玩家生命值不大於0且遊戲結束旗標為否
            {
                gameOver = true;//遊戲結束旗標設定為是
                GameObject gameOverText = GameObject.FindGameObjectWithTag("GameOver");
                //宣告遊戲結束文字物件並設定為全域中帶有"Game結束"標籤的物件
                gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
                //遊戲結束文字的動畫控制器元件的"gameOver"動畫設定為是(此動畫為遊戲結束動畫不是單指輸掉的動畫)
            }
            // 4 
            for (int i = 0; i < healthIndicator.Length; i++)//迴圈判斷綠色怪物數量
            {
                if (i < Health)//如果i小於玩家生命值
                {
                    healthIndicator[i].SetActive(true);//玩家生命指標陣列的第i項設定為啟動
                }
                else
                {
                    healthIndicator[i].SetActive(false);//玩家生命指標陣列的第i項設定為不啟動
                }
            }
        }
    }
    // Use this for initialization
    void Start () {
        Gold = 1000;//金幣起始值設定為1000
        Wave = 0;//波次起始值設定為0
        Health = 5;//玩家生命值起始值設定為5
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
