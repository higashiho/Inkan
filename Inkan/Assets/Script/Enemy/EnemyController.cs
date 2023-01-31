using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : BaseEnemy
{
    private void Awake() 
    {
        startTag = this.gameObject.tag;
        switch(enemysTipe)
        {
            case enemyTipe.NOMAL_ENEMY:
                enemys.Power = 1;
                enemys.Hp = 2;
                enemys.Speed = 2.5f;
                break;
            case enemyTipe.POWER_ENEMY:
                enemys.Power = 2;
                enemys.Hp = 2;
                enemys.Speed = 2;
                break;
            case enemyTipe.SPEED_ENEMY:
                enemys.Power = 1;
                enemys.Hp = 1;
                enemys.Speed = 3.5f;
                break;
            case enemyTipe.BONAS_ENEMY:
                enemys.Power = 2;
                enemys.Hp = 1;
                enemys.Speed = 2;
                break;
            default:
                break;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        // 取得
        playerObject = GameObject.FindWithTag("Player");
        playerPosition = playerObject.transform.position;
        enemyPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        move();

        reDestroy();

        enemyStatus();
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
            enemysTipe = enemyTipe.EXP;
        }
    }
}
