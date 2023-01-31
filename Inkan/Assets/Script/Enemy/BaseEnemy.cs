using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Enemys
{
    // タグ
    public string StartTag;
    // 攻撃力
    public float Power;
     // Hp
    public float Hp;
     // 移動速度
    public float Speed;
}
public class BaseEnemy : MonoBehaviour
{   
    // 攻撃力
    [SerializeField]
    protected float powerEnemy;
    public float PowerEnemy{get{return powerEnemy;}private set{powerEnemy = value;}}
 
    // 移動スピード
    [SerializeField]
    protected float speed;
    // HP
    [SerializeField]
    protected float hp;
    [SerializeField]    // 消える時間
    public float deleteTime;
    // 時間計測
    protected float time;
    public float TimeManage{get{return time;}set {time = value;}}

    // 座標
    protected Vector3 playerPosition;
    protected Vector3 enemyPosition;

    // Playerオブジェクト
    protected GameObject playerObject;

    public UnityAction<BaseEnemy> objectPoolCallBack;

    [SerializeField]    // 初期のタグ
    protected string startTag;

    
    [SerializeField]    // 落とす経験値
    protected GameObject exp;
    [SerializeField]    // 自身のオブジェクト
    protected GameObject enemyObj;

    // エネミーの種類
    public enum enemyTipe
    {
        SPEED_ENEMY,
        POWER_ENEMY,
        NOMAL_ENEMY,
        MID_ENEMY,
        SKILL_ENEMY,
        BONAS_ENEMY,
        EXP,
        POOL
    }
    [SerializeField]
    protected enemyTipe enemysTipe;
    public enemyTipe EnemysTipe{get {return enemysTipe;}set {enemysTipe = value;}}
    
    protected Enemys enemys;

    // エネミーの状態
    protected void enemyStatus()
    {
        switch(enemysTipe)
        {
            // エネミー状態のときはエネミーのみ表示
            case enemyTipe.SPEED_ENEMY:
            case enemyTipe.POWER_ENEMY:
            case enemyTipe.NOMAL_ENEMY:
            case enemyTipe.MID_ENEMY:
            case enemyTipe.SKILL_ENEMY:
            case enemyTipe.BONAS_ENEMY:
                enemyObj.SetActive(true);
                exp.SetActive(false);
                break;
            // EXP状態になったら表示を変えてタグを変更
            case enemyTipe.EXP:
                enemyObj.SetActive(false);
                exp.SetActive(true);
                this.gameObject.tag = "EXP";
                break;
            
            case enemyTipe.POOL:
                objectPoolCallBack?.Invoke(this);
                break;
            default:
                break;
        }
    }
    
    // 一定時間後に格納
    protected void reDestroy()
    {
        time += Time.deltaTime;

        if(time >= deleteTime)
            objectPoolCallBack?.Invoke(this);
    }


    void OnDisable() 
    {
        Debug.Log("reset");
        resetEnemy();
    }

    // 挙動
    protected void move()
    {
        
        playerPosition = playerObject.transform.position;
        enemyPosition = transform.position;

        if (playerPosition.x > enemyPosition.x)
        {
            enemyPosition.x = enemyPosition.x + speed * Time.deltaTime;
        }
        else if (playerPosition.x < enemyPosition.x)
        {
            enemyPosition.x = enemyPosition.x - speed * Time.deltaTime;
        }

        if (playerPosition.y > enemyPosition.y)
        {
            enemyPosition.y = enemyPosition.y + speed * Time.deltaTime;
        }
        else if (playerPosition.y < enemyPosition.y)
        {
            enemyPosition.y = enemyPosition.y - speed * Time.deltaTime;
        }

        transform.position = enemyPosition;
    }

    
    // 初期化
    protected void resetEnemy()
    {
        // 二回目以降のステート初期化
        switch(startTag)
        {
            case "Enemy":
                enemysTipe = enemyTipe.NOMAL_ENEMY;
                break;
            case "PowerEnemy":
                enemysTipe = enemyTipe.POWER_ENEMY;
                break;
            case "SpeedEnemy":
                enemysTipe = enemyTipe.SPEED_ENEMY;
                break;
            case "MidBoss":
                enemysTipe = enemyTipe.MID_ENEMY;
                break;
            case "SkillEnemy":
                enemysTipe = enemyTipe.SKILL_ENEMY;
                break;
            case "BonasEnemy":
                enemysTipe = enemyTipe.BONAS_ENEMY;
                break;
            default:
                break;
        }

        // 変数を初期化
        this.gameObject.tag = startTag;
        powerEnemy = enemys.Power;
        hp = enemys.Hp;
        speed = enemys.Speed;
    }
}
