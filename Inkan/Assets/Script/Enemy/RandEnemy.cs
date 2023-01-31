using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandEnemy : MonoBehaviour
{
    [SerializeField]
    private BaseEnemy[] prefabEnemy;
    [SerializeField]
    private BaseEnemy prefabMidEnemy;
    [SerializeField]
    private LevelUP playerLevel;
    //ノーマル時のエネミー出現  
    public bool Nomal = true;         
    //エリア外時のエネミー出現      
    public bool Danger = false;    
    //プレイヤーレベル         
    public float PLevel = 0;    
    //ランダムエネミー
    private int number;         

    public TimerController timerController;
    [SerializeField]    //ボスの出現時間代入用
    private int midTimer = 0;             
    //ボスを一体だけ出現させる用
    private bool Once = true;       
    //一回だけ処理を一分後trueにする用
    private float revival = 0;      

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }
   
    // Update is called once per frame
    void Update()
    {

        // エネミー生成処理
        if (playerLevel.PlayerLevel < Const.PLAYER_LEVEL[0])
        {
            spawnEnemy(Const.SPAWN_COUNT[0]);
        }
        else if (playerLevel.PlayerLevel < Const.PLAYER_LEVEL[1])
        {
            spawnEnemy(Const.SPAWN_COUNT[1]);
        }
        else if (playerLevel.PlayerLevel < Const.PLAYER_LEVEL[2])
        {
            spawnEnemy(Const.SPAWN_COUNT[2]);
        }

        // 中ボス生成
        sponeMidBoss(UnityEngine.Random.Range(-Const.MID_SPAWN_POS[0], Const.MID_SPAWN_POS[1]),
                    UnityEngine.Random.Range(-Const.MID_SPAWN_POS[2], -Const.MID_SPAWN_POS[3]));
    }
 
    // 敵の生成
    private void enemySpawn(float x, float y)
    {
        number = Random.Range(0, prefabEnemy.Length);

        Vector3 pos = new Vector3(x, y, 0.0f);

        //敵を生成
        FactoryEnemy.objectPool.Launch(pos,FactoryEnemy.objectPool.EnemyList,prefabEnemy[number]);
    }

    // ステージ範囲外の場合のエネミー生成
    private void dengerEnemy()
    {
        // ステージ範囲外の時
        if (Danger)
        {
            if (transform.position.y > 0.0f)
            {
                if ((Time.frameCount % Const.DANGER_SPAWN_COUNT) == 0)
                {
                    // エネミーを引数場所範囲内に生成
                    enemySpawn(UnityEngine.Random.Range(-Const.DANGER_SPAWN_POS[0], Const.DANGER_SPAWN_POS[0]),
                                UnityEngine.Random.Range( 0.0f, Const.DANGER_SPAWN_POS[1]));
                }
            }
            else if (transform.position.y < 0.0f)
            {
                if (Time.frameCount % Const.DANGER_SPAWN_COUNT == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(-Const.DANGER_SPAWN_POS[0], Const.DANGER_SPAWN_POS[0]),
                                UnityEngine.Random.Range(-Const.DANGER_SPAWN_POS[1], 0.0f));
                }
            }
            else if (transform.position.x < 0.0f)
            {
                if (Time.frameCount % Const.DANGER_SPAWN_COUNT == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(-Const.DANGER_SPAWN_POS[1], 0.0f),
                                UnityEngine.Random.Range(-Const.DANGER_SPAWN_POS[0], Const.DANGER_SPAWN_POS[0]));
                }
            }
            else if (transform.position.x > 0.0f)
            {
                if (Time.frameCount % Const.DANGER_SPAWN_COUNT == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(0.0f, Const.DANGER_SPAWN_POS[1]),
                                UnityEngine.Random.Range(-Const.DANGER_SPAWN_POS[0], Const.DANGER_SPAWN_POS[0]));
                }
            }
        }
    }
    // エネミー生成処理
    private void spawnEnemy(float sponeCount)
    {
        // ステージ範囲内の場合
        if (Nomal)
        {
            if (transform.position.y > 0.0f)
            {
                // sponeCountごとに敵を生成
                if (Time.frameCount % sponeCount == 0)
                {
                    // エネミーを引数場所範囲内に生成
                    enemySpawn(UnityEngine.Random.Range(-Const.SPAWN_POS[3], Const.SPAWN_POS[3]),
                                UnityEngine.Random.Range(-Const.SPAWN_POS[3], 0.0f));
                }
            }
            else if (transform.position.y < 0.0f)
            {
                if (Time.frameCount % sponeCount == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(-Const.SPAWN_POS[3], Const.SPAWN_POS[2]), 
                                UnityEngine.Random.Range(0.0f, Const.SPAWN_POS[3]));
                }
            }
            else if (transform.position.x > 0.0f)
            {
                if (Time.frameCount % sponeCount == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(-Const.SPAWN_POS[3], 0.0f), 
                                UnityEngine.Random.Range(-Const.SPAWN_POS[1], Const.SPAWN_POS[3]));
                }
            }
            else if (transform.position.x < 0.0f)
            {
                if (Time.frameCount % sponeCount == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(0.0f, Const.SPAWN_POS[3]), 
                                UnityEngine.Random.Range(-Const.SPAWN_POS[1], Const.SPAWN_POS[0]));
                }
            }
            else
            {
                if (Time.frameCount % sponeCount == 0)
                {
                    enemySpawn(UnityEngine.Random.Range(-Const.SPAWN_POS[3], Const.SPAWN_POS[2]), 
                                UnityEngine.Random.Range(Const.SPAWN_POS[1], Const.SPAWN_POS[3]));
                }
            }
        }
        // ステージ範囲外の場合
        else
        {
            dengerEnemy();
        }
    }

    // 中ボス生成処理
    private void sponeMidBoss(float x, float y)
    {
        
        midTimer = timerController.Minute;
        // 生成時間が０以外のとき生成処理
        if (midTimer > 0)
        {
            // 三分に一度生成
            if ((midTimer % Const.MID_ENEMY_SPAWN) == 0)
            {
                //敵の位置をランダムに決める
                Vector3 pos = new Vector3(x, y, 0.0f);

                // 一度だけ処理にしないと生成量が増えてしまうため
                if (Once)
                {
                    FactoryEnemy.objectPool.Launch( pos,FactoryEnemy.objectPool.EnemyList, prefabMidEnemy);
                    Once = false;
                }
            }
        }
        // 一分後に生成可能フラグをオンにする
        if (!Once)
        {
            revival += Time.deltaTime;
            if (revival >= Const.MINUTE)
            {
                revival = 0;
                Once = true;
            }
        }
    }
}

