using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;


/// Ver1.0    :   2022/06/30
/// Ver1.1    :   2022/1/10



public class PlayerController : MonoBehaviour
{
    // スピード
    private float speed = 5.0f;
    public float Speed{get {return speed;}set {speed = value;}}

    // プレイヤーのHp
    private float hp;
    public float Hp{get {return hp;}set {hp = value;}}
    //hpスライダー
    public Slider HpSlider{get;private set;} = null; 
    //Hpテキスト
    [SerializeField]
    private Text hpObj = null; 


    //ダメージフラグ
    private bool onDamage = false;
    public bool OnDamage{get {return onDamage;}set {onDamage = value;}}  
    // ダメージクールタイム
    private float timeReset = 3.0f;
    // ダメージ受けてからの時間
    private float time; 


    //heelスキル
    private bool heel = false;
    public bool Heel{get{return heel;}set {heel = value;}} 
    //ヒールテキスト  
    [SerializeField]
    private Text heelObj = null; 
    public Text HeelObj{get {return heelObj;}set {heelObj = value;}}
    // ヒールできる回数
    private int heelPoint = 3;

    // スピードアップスキルステータス管理用
    private enum speedUpState
    {
        NOMAL,
        SPEED_UP,
        SPEED_UP_NOW
    }  
    private speedUpState speedStatus;
    //スピードアップの残り時間
    private float speedCount;    

    [SerializeField]
    private BulletController bulletController;
    //鉄球スキル
    private float ironBallSkill;
    public float IronBallSkill{get {return ironBallSkill;}set {ironBallSkill = value;}}
    //貫通弾スキル
    private float penetratingSkill;
    public float PenetratingSkill{get {return penetratingSkill;}set {penetratingSkill = value;}}  
    //スピードアップスキル
    private float speedUpSkill;
    public float SpeedUpSkill{get {return speedUpSkill;}set {speedUpSkill = value;}}  
    // スキルコールバック
    private UnityAction skillCallback;
    
    [SerializeField]    //スピードアップテキスト  
    private Text speedUpObj = null; 
    [SerializeField]    //skillテキスト
    private Text[] skill1Obj = new Text[3]; 

    private void Awake() 
    {
        // スキル関係代入
        skillCallback += heelSkill;
        skillCallback += penetrating;
        skillCallback += ironSkill;
        skillCallback += speedUp;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 取得
        HpSlider = GameObject.FindWithTag("HpSlider").GetComponent<Slider>();


        // 初期化
        hp = Const.MAX_HP;
        HpSlider.maxValue = Const.MAX_HP;
        time = 0;
        speedStatus = speedUpState.NOMAL;
    }

    // Update is called once per frame
    void Update()
    {
        move();

        printText();

        skills();

        damage();
    }

    // 挙動
    private void move()
    {
        var position = transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            position.x -= speed * Time.deltaTime;

        }
        else if (Input.GetKey(KeyCode.D))
        {
            position.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            position.y += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            position.y -= speed * Time.deltaTime;
        }

        transform.position = position;

    }

    // テキスト表示
    private void printText()
    {
        hpObj.text = "Hp:" + hp;

        heelObj.text = "HeelPoint:\n" + heelPoint +"Use the R key";
        
        speedUpObj.text = "SpeedUpTime:\n" + speedCount.ToString("N2");

        skill1Obj[0].text = "" + IronBallSkill;
        skill1Obj[1].text = "" + PenetratingSkill;
        skill1Obj[2].text = "" + SpeedUpSkill;

    }

    // ヒールスキル
    private void heelSkill()
    {
        if (Heel && Input.GetKeyDown(KeyCode.R))
        {
            if (hp < Const.MAX_HP)
            {
                Debug.Log("Heel");
                hp++;
                HpSlider.value = hp;
                heelPoint--;
            }
            if (heelPoint <= 0)
            {
                Heel = false;
            }
        }
    }

    // 貫通弾スキル
    private void penetrating()
    {
        if (PenetratingSkill > 0 && Input.GetKeyDown(KeyCode.F))
        {
            PenetratingSkill--;
            bulletController.SkillBulletStatus = BulletController.skillBulletState.PENTERATING;
            bulletController.SkillBullets = Const.PENTERATING_BULLETS;
            bulletController.SkillSlider.maxValue = Const.PENTERATING_BULLETS;
        }
    }

    // 鉄球スキル
    private void ironSkill()
    {
        if (IronBallSkill > 0 && Input.GetKeyDown(KeyCode.E))
        {
            IronBallSkill--;
            bulletController.SkillBulletStatus = BulletController.skillBulletState.IRON_BALL;
            bulletController.SkillBullets = Const.IRON_BALL_BULLETS;
            bulletController.SkillSlider.maxValue = Const.IRON_BALL_BULLETS;
        }
    }

    // スピードアップスキル
    private void speedUp()
    {
        switch(speedStatus)
        {
            case speedUpState.NOMAL:
                if (SpeedUpSkill > 0 && Input.GetKeyDown(KeyCode.Q))
                {
                    SpeedUpSkill--;
                    speedUpObj.enabled = true;
                    speedStatus = speedUpState.SPEED_UP;
                }
                break;
            case speedUpState.SPEED_UP:
                speed += Const.SPEED_SKILL_UP_SPEED;
                speedStatus = speedUpState.SPEED_UP_NOW;
                break;
            case speedUpState.SPEED_UP_NOW:
                 speedCount += Time.deltaTime;
                if (speedCount >= Const.MINUTE)
                {
                    speedStatus = speedUpState.NOMAL;
                    speedUpObj.enabled = false;
                }
                break;
            default:
                break;
        }
    }

    // スキル全体
    private void skills()
    {

        skillCallback?.Invoke();
        // スキル所持量が最大値以上になると最大値に戻す
        if (IronBallSkill >= Const.SKILL_MAX 
            || PenetratingSkill >= Const.SKILL_MAX 
            || SpeedUpSkill >= Const.SKILL_MAX)
        {
            IronBallSkill = Const.SKILL_MAX;
        }

    }

    // ダメージ関係
    private void damage()
    {
        if (OnDamage)
        {
            time += Time.deltaTime;
            if (time > timeReset)
            {
                OnDamage = false;
                time = 0;
            }
            /*else
            {
                Debug.Log("invincible");
            }*/
        }
    
        HpSlider.value = Hp;
    }

}