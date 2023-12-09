using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave//宣告類別波次
{
    public GameObject enemyPrefab;//宣告敵人預製件物件
    public float spawnInterval = 2;//宣告生成時間間隔並設定為2秒
    public int maxEnemies = 20;//宣告最大敵人數並設定為20
}
public class SpawnEnemy : MonoBehaviour {
    public GameObject[] waypoints;//宣告路徑點物件陣列
    public GameObject testEnemyPrefab;//宣告測試用的敵人預製件物件
    public Wave[] waves;//宣告波次類別的陣列
    public int timeBetweenWaves = 5;//宣告波次與波次間的時間並設定為5

    private GameManagerBehavior gameManager;//宣告遊戲管理器

    private float lastSpawnTime;//宣告最後生成時間
    private int enemiesSpawned = 0;//宣告已經生成的敵人數量並設定為0
    // Use this for initialization
    void Start () {
        //Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
        //實例化測試用敵人並將其MoveEnemy腳本中的waypoints設定為這個物件的路徑點陣列

        lastSpawnTime = Time.time;//最後生成時間設定為當前時間點
        gameManager = GameObject.Find("GameManager").GetComponent<GameManagerBehavior>();
        //將gameManager設定為GameManager這個物件的GameManagerBehavior這個元件(腳本物件)
    }

    // Update is called once per frame
    void Update () {
        // 1
        int currentWave = gameManager.Wave;//宣告當前波次數並設定為遊戲管理器中儲存的波次數(起始為0)
        if (currentWave < waves.Length)//如果當前波次數小於波次物件陣列的長度
        {
            // 2
            float timeInterval = Time.time - lastSpawnTime;
            //宣告生成完後的時間間隔並設定為當前時間點減去最後生成時間點
            float spawnInterval = waves[currentWave].spawnInterval;
            //生成時間間隔並設定為波次物件陣列的當前波次的生成時間間隔
            if (((enemiesSpawned == 0 && timeInterval > timeBetweenWaves) || timeInterval > spawnInterval) && enemiesSpawned < waves[currentWave].maxEnemies)
            {//如果((敵人已經生成的數量等於0且生成完後的時間間隔大於波次間的間隔)或生成完後的時間間隔大於生成時間間隔)且敵人已經生成數小於當前波次最大敵人數)
             //如果尚未生成且距離上次生成已經到了下一波生成的時間或是已經開始生成而到了生成下一隻的時間這兩種中其中一種狀況下尚未達到最大敵人數
                // 3  
                lastSpawnTime = Time.time;//最後生成時間點刷新成目前時間點(生成新的一隻敵人後記錄時間點)
                GameObject newEnemy = (GameObject)Instantiate(waves[currentWave].enemyPrefab);
                //宣告新的敵人物件並實例化，藍圖是當前波次中的敵人預製件
                newEnemy.GetComponent<MoveEnemy>().waypoints = waypoints;
                //新的敵人物件中的MoveEnemy腳本的路徑點陣列設定為這個物件的路徑點陣列
                enemiesSpawned++;//敵人已經生成數+1
            }
            // 4 
            if (enemiesSpawned == waves[currentWave].maxEnemies && GameObject.FindGameObjectWithTag("Enemy") == null)
            {//如果敵人已經生成數達到當前波次最大敵人數且尋找全域中所有物件中帶有"Enemy"標籤的物件都是空
             //如果當前波次已經結束且場景中已經沒有敵人
                gameManager.Wave++;//遊戲管理器的波次+1同時處發動畫
                gameManager.Gold = Mathf.RoundToInt(gameManager.Gold * 1.1f);
                //遊戲管理器的金幣數設定為原來的金幣數乘上1.1，並四捨五入到整數
                enemiesSpawned = 0;//敵人已經生成數量設定為0
                lastSpawnTime = Time.time;//最後生成時間點刷新成目前時間點(生成新的一隻敵人後記錄時間點)
            }
            // 5 
        }
        else//如果當前波次大於等於波次物件陣列的長度
        {
            gameManager.gameOver = true;//遊戲管理器的遊戲結束旗標設定為是
            GameObject gameOverText = GameObject.FindGameObjectWithTag("GameWon");
            //宣告遊戲結束文字物件並設定為全域中帶有"GameWon"標籤的物件
            gameOverText.GetComponent<Animator>().SetBool("gameOver", true);
            //遊戲結束文字的動畫控制器元件的"gameOver"動畫設定為是(此動畫為遊戲結束動畫不是輸掉的動畫)
        }
    }
}
