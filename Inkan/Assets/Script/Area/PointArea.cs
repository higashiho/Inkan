using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointArea : MonoBehaviour
{
    [SerializeField]    //pointテキスト
    private Text pointObject = null; 
    [SerializeField]    //pointPlusテキスト
    private Text pointPlusObject = null; 
    [SerializeField]    //視野規制
    private SpriteRenderer maskObj = null; 
    [SerializeField]
    private BaseEnemy prefabEnemy;


    private float pointCount = 10.0f;   //pointカウント
    private int point = 0;
    public static int PlusPoint{get;private set;} = 0;
    private bool countDown = false;

    private bool pointEnemySpawn = false;

    private void Start()
    {
        pointObject.enabled = false;
        pointPlusObject.enabled = false;
        maskObj.enabled = false;
    }

    void Update()
    {
        // 通常挙動
        pointArea();
        // エネミー生成
        spawnPointAreaEnemy();
    }

    // ポイントエリア処理
    private void pointArea()
    {
        pointObject.text = "" + pointCount.ToString("N2");
       
       // ポイントエリアにいるフラグが経ったらカウントを始める
        if (countDown)
        {
            pointCount -= Time.deltaTime;
            if (pointCount <= 0)
            {
                // カウントが一定値以下になったらポイントアップ
                point += Const.MAX_AREA_POINT;
                pointCount = Const.MAX_POINT_COUNT;
                pointPlusObject.enabled = true;
            }
        }
        
        pointPlusObject.text = "Point+" + point;
    }

    // エネミー生成
    private void instanceEnemy(float x,float y)
    {
        Vector3 pos = new Vector3(x, y, 0.0f);

        //敵を生成
        FactoryEnemy.objectPool.Launch(pos, FactoryEnemy.objectPool.EnemyList, prefabEnemy);
    }

    // ポイントエリアエネミー生成
    private void spawnPointAreaEnemy()
    {

        if (pointEnemySpawn && Time.frameCount % Const.SPAWN_COUNT[0] == 0)
        {
            instanceEnemy(UnityEngine.Random.Range(Const.POINT_AREA_ENEMY_SPAWN_POS[0],Const.POINT_AREA_ENEMY_SPAWN_POS[1]),
                            UnityEngine.Random.Range(Const.POINT_AREA_ENEMY_SPAWN_POS[2],Const.POINT_AREA_ENEMY_SPAWN_POS[3]));
            instanceEnemy(UnityEngine.Random.Range(Const.POINT_AREA_ENEMY_SPAWN_POS[4],Const.POINT_AREA_ENEMY_SPAWN_POS[5]),
                            UnityEngine.Random.Range(Const.POINT_AREA_ENEMY_SPAWN_POS[6],Const.POINT_AREA_ENEMY_SPAWN_POS[7]));
        }
    }

    // 以下当たり判定
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PointArea")
        {
            countDown = true;
            pointEnemySpawn = true;
            pointObject.enabled = true;
            maskObj.enabled = true;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "PointArea")
        {
            countDown = false;
            pointEnemySpawn = false;
            pointObject.enabled = false;
            pointPlusObject.enabled = false;
            maskObj.enabled = false;
            pointCount = Const.MAX_POINT_COUNT;
            PlusPoint = point;
            point = 0;
        }
    }
}
