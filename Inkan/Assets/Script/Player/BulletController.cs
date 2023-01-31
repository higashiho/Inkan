using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletController : MonoBehaviour
{
    [SerializeField]    // 通常弾
    public BaseBullet bullet;
    [SerializeField]    // 攻撃弾
    public BaseBullet attackBullet;
    [SerializeField]    // スピード弾
    public BaseBullet speedBullet;
    [SerializeField]    // 鉄球弾
    public BaseBullet ironBallBullet;
    [SerializeField]    // 貫通弾
    public BaseBullet penetratingBullet;

    // 弾速
    private float speed;            

    [SerializeField]    // スキル量
    private int skillBullets;
    public int SkillBullets{get {return skillBullets;}set {skillBullets = value;}}
    [SerializeField]    //弾数
    private float bullets;
    public float Bullets{get {return bullets;}set {bullets = value;}}    
    [SerializeField]    //弾数ゲージ
    private Slider shotSlider;  

    //　Playerの状態
    public enum bulletState
    {
        NOMAL,
        POWER_BULLET,
        SPEED_BULLET
    }
    [SerializeField]
    private bulletState bulletStatus;
    public bulletState BulletStatus{get {return bulletStatus;}set {bulletStatus = value;}}

    //スキル用ステート
    public enum skillBulletState
    {
        NOMAL,
        IRON_BALL,
        PENTERATING
    }
    [SerializeField]
    private skillBulletState skillBulletStatus;
    public skillBulletState SkillBulletStatus{get {return skillBulletStatus;}set {skillBulletStatus = value;}}


    [SerializeField]    //　弾text
    private Text bulletObj = null; 
    [SerializeField]    //弾数
    private float bulletNum = 10;          

    //弾の発射間隔
    private float timeReset;    
    public float TimeReset{get {return timeReset;}set {timeReset = value;}}        
    //弾の発射間隔
    private float time;               

    //弾を打てるかどうか
    private bool shotPoint = true;  
     //一回だけ処理
    private bool onlyOnce = true;   


    [SerializeField]    //スキルスライダー
    private Slider skillSlider; 
    public Slider SkillSlider{get {return skillSlider;}set {skillSlider = value;}}
    [SerializeField]    //スキルテキスト
    private Text skillObj = null; 


    private void Awake() 
    {
    }

    void Start()
    {
        // 初期化
        bulletStatus = bulletState.NOMAL;
        skillBulletStatus = skillBulletState.NOMAL;
        speed = 10.0f;  
        bullets = Const.MAX_BULLETS;
        shotSlider.maxValue = Const.MAX_BULLETS;
        shotSlider.value = bullets;
        time = 0;
        TimeReset = 0.3f;

    }

    void Update()
    {
        bulletShot();

        noBullet();

        skillBullet();
        
    }  
    
    // 弾の挙動
    private void bulletMove(BaseBullet Ball)
    {
        BaseBullet clone = FactoryBullet.objectPool.Launch(this.transform.position, FactoryBullet.objectPool.BulletList, Ball);

        // クリックした座標の取得（スクリーン座標からワールド座標に変換）
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 向きの生成（Z成分の除去と正規化）
        Vector3 shotForward = Vector3.Scale((mouseWorldPos - transform.position), new Vector3(1, 1, 0)).normalized;

        // 弾に速度を与える
        clone.GetComponent<Rigidbody2D>().velocity = shotForward * speed;
    }

    // 発射挙動
    private void bulletShot()
    {
        bulletObj.text = "bullet:" + bulletNum;
        skillObj.text = "Skillpoint:" + SkillBullets;


        skillSlider.value = SkillBullets;
        

        time += Time.deltaTime;
        if (time > TimeReset)
        {
            if (Input.GetMouseButton(0) && shotPoint) 
            {
                switch(bulletStatus)
                {
                case bulletState.NOMAL:
                    bulletMove(bullet);
                    break;
                case bulletState.POWER_BULLET:
                    bulletMove(attackBullet);
                    break;
                case bulletState.SPEED_BULLET:
                    bulletMove(speedBullet);
                    break;
                default:
                    break;
                }
                
                bullets--;
                shotSlider.value = bullets;
                time = 0;
                bulletNum--;

                if (bullets <= 0)
                {
                    shotPoint = false;
                }
            }
        }

    }
    
    // 弾数がなくなった場合
    private void noBullet()
    {
        if (!shotPoint)
        {
            bullets += Const.RELOADING_SPEED * Time.deltaTime;
            shotSlider.value = bullets;
            // 一度のみスピードを下げる
            if (onlyOnce)
            {
                PlayerController playerController = GetComponent<PlayerController>();
                playerController.Speed -= Const.CHANGE_SPEED;
                onlyOnce = false;
            }

            // バレットが最大値になったら初期化
            if (bullets > Const.MAX_BULLETS)
            {
                bullets = Const.MAX_BULLETS;
                bulletNum = Const.MAX_BULLETS;
                shotSlider.maxValue = Const.MAX_BULLETS;
                if (!onlyOnce)
                {
                    PlayerController playerController = GetComponent<PlayerController>();
                    playerController.Speed += Const.CHANGE_SPEED;
                    onlyOnce = true;
                }
                shotPoint = true;
            }
        }

    }

    // スキル弾処理
    private void skillBullet()
    {
        if (Input.GetMouseButtonDown(1))
        {
            switch(skillBulletStatus)
            {
                case skillBulletState.IRON_BALL:

                    speed = Const.IRON_BALL_SPEED;
                    bulletMove(ironBallBullet);


                    SkillBullets--;
                    skillSlider.value = SkillBullets;
                    break;
                    
                case skillBulletState.PENTERATING:
                    bulletMove(penetratingBullet);

                    SkillBullets--;
                    skillSlider.value = SkillBullets;
                    break;
                
                default:
                    break;
                
            }
            if (SkillBullets <= 0)
            {
                skillBulletStatus = skillBulletState.NOMAL;
            }
        }
    }
}