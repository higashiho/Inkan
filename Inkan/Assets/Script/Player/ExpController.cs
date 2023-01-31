using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpController : MonoBehaviour
{
    [SerializeField]
    private BaseEnemy enemy;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // プレイヤーに当たったらプールに戻る
        if (collision.gameObject.tag == "Player")
        {
            enemy.EnemysTipe = BaseEnemy.enemyTipe.POOL;
        }
    }
}
