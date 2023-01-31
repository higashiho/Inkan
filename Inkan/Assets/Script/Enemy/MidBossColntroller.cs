using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MidBossColntroller : BaseEnemy
{
    [SerializeField]
    private Slider hpSlider; //hpスライダー
    [SerializeField]
    private Text hpText = null; //Hpテキスト
     private void Awake() 
     {
        enemys.StartTag = this.gameObject.tag;
        enemys.Power = 3.0f;
        enemys.Hp = Const.MID_MAX_HP;
        enemys.Speed = 0.5f;

     }
    // Start is called before the first frame update
    void Start()
    {
        // 取得
        hpText = GameObject.Find("MidHpText").GetComponent<Text>();
        hpSlider= GameObject.Find("MidHpSlider").GetComponent<Slider>();
        playerObject = GameObject.FindWithTag("Player");
        playerPosition = playerObject.transform.position;
        enemyPosition = transform.position;

        // 初期化
        hpSlider.maxValue = enemys.Hp;
        hpSlider.value = enemys.Hp;

    }

    // Update is called once per frame
    void Update()
    {
        move();

        reDestroy();

        enemyStatus();
    }
    private void OnDisable() 
    {
        hpText.gameObject.SetActive(false);
                
        hpSlider.gameObject.SetActive(false);
    }
    private void OnEnable() 
    {
        hpText.gameObject.SetActive(true);
                
        hpSlider.gameObject.SetActive(true);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BigBullet" || other.gameObject.tag == "PenetratingBullet"
            || other.gameObject.tag == "Bullet" || other.gameObject.tag == "SpeedBullet"
            || other.gameObject.tag == "PowerBullet")
        {
            Debug.Log("Hit");
            hp -= other.gameObject.GetComponent<BaseBullet>().BulletPower;
            hpSlider.value = hp;
        }
        if (hp <= 0)
        {
            enemysTipe = enemyTipe.EXP;
            hpText.gameObject.SetActive(false);
            hpSlider.gameObject.SetActive(false);

        }
    }
}
