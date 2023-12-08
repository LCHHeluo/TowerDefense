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
    public Text waveLabel;//宣告波次標籤
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
                for (int i = 0; i < nextWaveLabels.Length; i++)//重複做波次總數的次數
                {
                    nextWaveLabels[i].GetComponent<Animator>().SetTrigger("nextWave");
                    //下一波波次標籤的動畫設定觸發器為"nextWave"
                }
            }
            waveLabel.text = "WAVE: " + (wave + 1);//波次標籤的文字設定為"WAVE: " + (波次 + 1)
        }
    }
    // Use this for initialization
    void Start () {
        Gold = 1000;//金幣起始值設定為1000
        Wave = 0;//波次起始值設定為0
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
