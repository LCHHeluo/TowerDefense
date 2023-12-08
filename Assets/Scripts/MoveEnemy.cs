using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour {
    [HideInInspector]
    public GameObject[] waypoints;//宣告路徑點陣列
    private int currentWaypoint = 0;//宣告當前路徑點位編號
    private float lastWaypointSwitchTime;//宣告敵人經過該路徑點的時間
    public float speed = 1.0f;//宣告敵人速度
    // Use this for initialization
    void Start () {
        lastWaypointSwitchTime = Time.time;//設定為目前時間的時間點
    }
	
	// Update is called once per frame
	void Update () {
        // 1 
        Vector3 startPosition = waypoints[currentWaypoint].transform.position;//宣告起始座標並設為當前路徑點座標
        Vector3 endPosition = waypoints[currentWaypoint + 1].transform.position;//宣告終座標置並設為下一個路徑點座標
        // 2 
        float pathLength = Vector3.Distance(startPosition, endPosition);//宣告路徑長度並設定為起始點減去終點
        float totalTimeForPath = pathLength / speed;//宣告起點到終點所花費的時間並設定為路徑長度除以敵人速度
        float currentTimeOnPath = Time.time - lastWaypointSwitchTime;//宣告在當前路徑所花費的時間並設定為目前的時間點減去敵人經過該路徑點的時間
        gameObject.transform.position = Vector2.Lerp(startPosition, endPosition, currentTimeOnPath / totalTimeForPath);
        //這個物件的transform的座標設定為在當前路徑所花費的時間和起點到終點所花費時間比率應該要在起始位置和終點位置兩點間的哪個座標
        // 3 
        if (gameObject.transform.position.Equals(endPosition))//如果這個物件的transform的座標等於終點座標
        {
            if (currentWaypoint < waypoints.Length - 2)//如果當前路徑位編號小於路徑點陣列長度-2(-1是因為陣列從0開始算，另一個-1是因為最後一個路徑點不能算也沒有下一個路徑點了)
            {
                // 3.a 
                currentWaypoint++;//路徑點位編號+1
                lastWaypointSwitchTime = Time.time;//設定為目前時間的時間點
                RotateIntoMoveDirection();
            }
            else//如果不是代表當前路徑點等於最後路徑點
            {
                // 3.b 
                Destroy(gameObject);//摧毀物件

                AudioSource audioSource = gameObject.GetComponent<AudioSource>();//宣告音效來源物件並設定為這個物件的AudioSource
                AudioSource.PlayClipAtPoint(audioSource.clip, transform.position);//播放音效，音效是audioSource.clip，播放位置是這個物件的座標
                // TODO: deduct health
            }
        }
    }
    private void RotateIntoMoveDirection()//旋轉到移動方向
    {
        //1
        Vector3 newStartPosition = waypoints[currentWaypoint].transform.position;//宣告新的起點座標並設定為當前路徑點位編號的座標
        Vector3 newEndPosition = waypoints[currentWaypoint + 1].transform.position;//宣告新的終點座標並設定為當前路徑點的下一個點位編號的座標
        Vector3 newDirection = (newEndPosition - newStartPosition);//宣告新的向量並設定為新的終點座標減去新的起點座標
        //2
        float x = newDirection.x;//宣告X並設定為新的向量的X座標
        float y = newDirection.y;//宣告Y並設定為新的向量的Y座標
        float rotationAngle = Mathf.Atan2(y, x) * 180 / Mathf.PI;//宣告旋轉角度並設定為YX的正切(弧度)*180\pi轉換成角度 
        //3
        GameObject sprite = gameObject.transform.Find("Sprite").gameObject;//宣告圖像並設定為這個物件的圖像的物件
        sprite.transform.rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);//圖像的旋轉角度設定為繞著Vector3.forward(Z軸)旋轉rotationAngle角度
    }
}
