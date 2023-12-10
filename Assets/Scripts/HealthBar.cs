using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour {
    public float maxHealth = 100;//宣告最大敵人生命值並設定為100
    public float currentHealth = 100;//宣告當前敵人生命值並設定為100
    private float originalScale;//宣告原規模
    // Use this for initialization
    void Start () {
        originalScale = gameObject.transform.localScale.x;//原規模設定為這個物件的X值規模
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 tmpScale = gameObject.transform.localScale;//宣告暫時規模並設定為當前規模
        tmpScale.x = currentHealth / maxHealth * originalScale;
        //暫時規模的X值設定為當前敵人生命值除以最大敵人生命值乘上原規模
        gameObject.transform.localScale = tmpScale;//這個物件的規模設定為暫時規模
    }
}
