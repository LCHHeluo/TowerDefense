using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour {
    public float speed = 10;//宣告子彈數度並設定為10
    public int damage;//宣告子彈傷害
    public GameObject target;//宣告目標物件
    public Vector3 startPosition;//宣告開始位置座標
    public Vector3 targetPosition;//宣告目標位置座標

    private float distance;//宣告距離
    private float startTime;//宣告開始時間

    private GameManagerBehavior gameManager;//宣告遊戲管理器物件
    // Use this for initialization
    void Start () {
        startTime = Time.time;//開始時間設定為目前時間的時間點
        distance = Vector2.Distance(startPosition, targetPosition);//距離設定為開始位置座標減去目標位置座標
        GameObject gm = GameObject.Find("GameManager");//gm設定為全域中的名為"GameManager"的物件
        gameManager = gm.GetComponent<GameManagerBehavior>();//遊戲管理器設定為gm的GameManagerBehavior腳本
    }
	
	// Update is called once per frame
	void Update () {
        // 1 
        float timeInterval = Time.time - startTime;//宣告時間間隔定設定為目前時間點減去開始時間點
        gameObject.transform.position = Vector3.Lerp(startPosition, targetPosition, timeInterval * speed / distance);
        //這個物件的位置座標設定為在目前所花費的時間和起點到終點所花費時間(總時長)比率應該要在起始位置和目標位置兩點間的哪個座標
        // 2 
        if (gameObject.transform.position.Equals(targetPosition))//如果個物件的座標等同於目標位置座標
        {
            if (target != null)//如果目標有存在物件
            {
                // 3
                Transform healthBarTransform = target.transform.Find("HealthBar");
                //宣告敵人生命條的變換組件並設定為目標的變換組件的生命條物件
                HealthBar healthBar = healthBarTransform.gameObject.GetComponent<HealthBar>();
                //宣告生命條類別物件並設定為生命條變換物件的HealthBar腳本
                healthBar.currentHealth -= Mathf.Max(damage, 0);
                //生命條腳本中的當前生命值設定為自身減去子彈傷害值，如果小於0則減去0
                // 4
                if (healthBar.currentHealth <= 0)//如果生命條腳本中的當前生命值不大於0
                {
                    Destroy(target);//摧毀目標
                    AudioSource audioSource = target.GetComponent<AudioSource>();
                    //宣告音源並設定為目標的AudioSource元件
                    AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);
                    //播放audioSource，播放位置為這個物件的位置
                    gameManager.Gold += 50;//遊戲管理器腳本中的金幣數設定為自身加上50
                }
            }
            Destroy(gameObject);//摧毀這個物件
        }
    }
}
