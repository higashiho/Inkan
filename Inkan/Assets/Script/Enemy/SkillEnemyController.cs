using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEnemyController : BaseEnemy
{

    private void Awake() 
    {
        startTag = this.gameObject.tag;
        enemys.Power = 2;
        enemys.Hp = 1;
        enemys.Speed = 3;

    }
    // Start is called before the first frame update
    void Start()
    {
        // 取得
        playerObject = GameObject.FindWithTag("Player");


        // 初期化
        playerPosition = playerObject.transform.position;
        enemyPosition = transform.position;
        startTag = this.gameObject.tag;

    }

    // Update is called once per frame
    void Update()
    {
        move();

        reDestroy();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "BigBullet" || other.gameObject.tag == "PenetratingBullet"
            || other.gameObject.tag == "Bullet" || other.gameObject.tag == "SpeedBullet"
            || other.gameObject.tag == "PowerBullet")
        {
            Debug.Log("Hit");
            hp -= other.gameObject.GetComponent<BaseBullet>().BulletPower;
        }
        if (hp <= 0)
        {
            objectPoolCallBack?.Invoke(this);
        }
    }
}
