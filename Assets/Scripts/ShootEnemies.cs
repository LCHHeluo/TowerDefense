using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour {
    public List<GameObject> enemiesInRange;//宣告一個範圍內的敵人清單
    private float lastShotTime;//宣告最後射擊時間點
    private MonsterData monsterData;//宣告怪物資料類別腳本
    // Use this for initialization
    void Start () {
        enemiesInRange = new List<GameObject>();//實例化物件清單
        lastShotTime = Time.time;//最後射擊時間設定為目前時間的時間點
        monsterData = gameObject.GetComponentInChildren<MonsterData>();
        //取得怪物資料類別腳本(取得子物件元件，怪物物件本身包含三個不同等級的子物件)
    }
	
	// Update is called once per frame
	void Update () {
        GameObject target = null;
        // 1
        float minimalEnemyDistance = float.MaxValue;
        foreach (GameObject enemy in enemiesInRange)
        {
            float distanceToGoal = enemy.GetComponent<MoveEnemy>().DistanceToGoal();
            if (distanceToGoal < minimalEnemyDistance)
            {
                target = enemy;
                minimalEnemyDistance = distanceToGoal;
            }
        }
        // 2
        if (target != null)
        {
            if (Time.time - lastShotTime > monsterData.CurrentLevel.fireRate)
            {
                Shoot(target.GetComponent<Collider2D>());
                lastShotTime = Time.time;
            }
            // 3
            Vector3 direction = gameObject.transform.position - target.transform.position;
            gameObject.transform.rotation = Quaternion.AngleAxis(
                Mathf.Atan2(direction.y, direction.x) * 180 / Mathf.PI,
                new Vector3(0, 0, 1));
        }
    }
    // 1
    void OnEnemyDestroy(GameObject enemy)//當敵人被摧毀
    {
        enemiesInRange.Remove(enemy);//從清單中移除enemy
    }

    void OnTriggerEnter2D(Collider2D other)//當進入觸發器
    {
        // 2
        if (other.gameObject.tag.Equals("Enemy"))//如果other的物件的標籤是"Enemy"
        {
            enemiesInRange.Add(other.gameObject);//將other的物件加入範圍內的敵人清單
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            //宣告del的敵人毀滅委派並設定為other的物件的EnemyDestructionDelegate腳本
            del.enemyDelegate += OnEnemyDestroy;
            //將當敵人被摧毀method註冊到del的敵人委派
        }
    }
    // 3
    void OnTriggerExit2D(Collider2D other)//當離開觸發器
    {
        if (other.gameObject.tag.Equals("Enemy"))//如果other的物件的標籤是"Enemy"
        {
            enemiesInRange.Remove(other.gameObject);//將other的物件移除範圍內的敵人清單
            EnemyDestructionDelegate del = other.gameObject.GetComponent<EnemyDestructionDelegate>();
            //宣告del的敵人毀滅委派並設定為other的物件的EnemyDestructionDelegate腳本
            del.enemyDelegate -= OnEnemyDestroy;
            //將當敵人被摧毀method取消註冊到del的敵人委派
        }
    }
    void Shoot(Collider2D target)//射擊
    {
        GameObject bulletPrefab = monsterData.CurrentLevel.bullet;
        //宣告子彈預製件並設定為怪物資料中的當前等級的子彈物件
        // 1 
        Vector3 startPosition = gameObject.transform.position;//宣告起始位置座標並設定為這個物件的位置座標
        Vector3 targetPosition = target.transform.position;//宣告目標位置座標並設定為目標的位置座標
        startPosition.z = bulletPrefab.transform.position.z;//起始位置座標的z軸設定為子彈預製件的z軸
        targetPosition.z = bulletPrefab.transform.position.z;//目標位置座標的z軸設定為子彈預製件的z軸
        //為了保證子彈的z軸在射擊過程中不會跑掉

        // 2 
        GameObject newBullet = (GameObject)Instantiate(bulletPrefab);
        //宣告新的子彈物件並實例化bulletPrefab
        newBullet.transform.position = startPosition;//新子彈物件的位置座標設定為起始位置座標
        BulletBehavior bulletComp = newBullet.GetComponent<BulletBehavior>();
        //宣告子彈行為腳本並設定為新子彈的子彈腳本元件
        bulletComp.target = target.gameObject;//新子彈腳本目標設定為目標的物件
        bulletComp.startPosition = startPosition;//新子彈腳本的位置座標設定為起始位置座標
        bulletComp.targetPosition = targetPosition;//新子彈腳本的目標座標設定為目標位置座標

        // 3 
        Animator animator = monsterData.CurrentLevel.visualization.GetComponent<Animator>();
        //宣告動畫器並設定為怪物資料的當前怪物等級的可視化圖案的動畫器元件
        animator.SetTrigger("fireShot");//動畫器的觸發器設定為"fireShot"
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        //宣告音源並設定為這個物件的音源元件
        audioSource.PlayOneShot(audioSource.clip);
        //播放音源
    }

}
