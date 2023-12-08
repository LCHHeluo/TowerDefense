using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour {
    public GameObject[] waypoints;//宣告路徑點陣列
    public GameObject testEnemyPrefab;//宣告測試用的敵人預製件
    // Use this for initialization
    void Start () {
        Instantiate(testEnemyPrefab).GetComponent<MoveEnemy>().waypoints = waypoints;
        //實例化測試用敵人並將其MoveEnemy腳本中的waypoints設定為這個物件的路徑點陣列
    }

    // Update is called once per frame
    void Update () {
		
	}
}
