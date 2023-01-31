using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BaseBullet
{

    
    void Update()
    {
        reDestroy();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "SpeedEnemy"
         || other.gameObject.tag == "PowerEnemy" || other.gameObject.tag == "SkillEnemy"
         || other.gameObject.tag == "wall" || other.gameObject.tag == "MidBoss")
        {
            bulletHp--;
            if(bulletHp <= 0)
                objectPoolCallBack.Invoke(this);
        }
    }
}
