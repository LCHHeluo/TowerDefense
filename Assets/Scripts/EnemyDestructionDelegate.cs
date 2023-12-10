using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestructionDelegate : MonoBehaviour {
    public delegate void EnemyDelegate(GameObject enemy);//宣告一個委派型別的函式類別敵人委派(參數敵人)
    public EnemyDelegate enemyDelegate;//宣告一個敵人委派的委派
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnDestroy()//當摧毀物件(系統預設)
    {
        if (enemyDelegate != null)//如果敵人委派不為空
        {
            enemyDelegate(gameObject);//物件本身傳進敵人委派
        }
    }
}
