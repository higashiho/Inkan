using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColPlayer : MonoBehaviour
{
    [SerializeField]
    private PlayerController player;
    [SerializeField]
    private BulletController bullet;
    [SerializeField]
    private SkillAreaEnemy skillArea;

    // プレイヤースキル更新
    private void playerStateUpdate(float maxHp, float shotTime,BulletController.bulletState state, float moveSpeed)
    {
        player.Hp = maxHp;
        player.HpSlider.maxValue = Const.ATTACK_SKILL_MAX_HP;
        player.HpSlider.maxValue = player.Hp;
        player.HpSlider.value = player.Hp;

        bullet.TimeReset = shotTime;
        // normalステート以外は更新
        if(state != BulletController.bulletState.NOMAL)
            bullet.BulletStatus = state;
            
        player.Speed = moveSpeed;
    }
    
    // 当たり判定処理
    private void OnCollisionEnter2D(Collision2D col) 
    {
        // スピードスキル取得時
        if (col.gameObject.tag == "SpeedSkill")
        {
            bullet.TimeReset = Const.SPEED_SKILL_SHOT_TIME;
            bullet.BulletStatus = BulletController.bulletState.SPEED_BULLET;
        }

        // 攻撃スキル取得時
        if (col.gameObject.tag == "PowerSkill")
        {
            playerStateUpdate(Const.ATTACK_SKILL_MAX_HP, Const.ATTACK_SKILL_SHOT_TIME, 
                                BulletController.bulletState.POWER_BULLET, Const.ATTACK_SKILL_MOVE_SPEED);
        }
        
        // 挙動スピードスキル取得時
        if (col.gameObject.tag == "MoveSpeedSkill")
        {
            playerStateUpdate(Const.MOVE_SKILL_MAX_HP, bullet.TimeReset, 
                                BulletController.bulletState.SPEED_BULLET, Const.MOVE_SKILL_MOVE_SPEED);
        }

        // 防御スキル取得時
        if (col.gameObject.tag == "DefenseSkill")
        {
            
            playerStateUpdate(Const.DEFENSE_SKILL_MAX_HP, Const.DEFENSE_SKILL_SHOT_TIME, 
                                BulletController.bulletState.NOMAL, Const.DEFENSE_SKILL_MOVE_SPEED);
        }

        // ヒールスキル取得時
        if (col.gameObject.tag == "HeelSkill")
        {
            player.Heel = true;
            player.HeelObj.gameObject.SetActive(true);

            playerStateUpdate(Const.HEEL_SKILL_MAX_HP, Const.HEEL_SKILL_SHOT_TIME, 
                                BulletController.bulletState.NOMAL, Const.HEEL_SKILL_MOVE_SPEED);
        }

        // 鉄球スキル取得時
        if (col.gameObject.tag == "BigSkill")
        {
            player.IronBallSkill++;
        }   
        
        // 貫通スキル取得時
        if (col.gameObject.tag == "PenetratingSkill")
        {
            player.PenetratingSkill++;
        }

        // スピードアップスキル取得時
        if(col.gameObject.tag == "SpeedUpSkill")
        {
            player.SpeedUpSkill++;
        }

        // ダメージ処理
        if (!player.OnDamage)
            if(col.gameObject.tag == "Enemy"
            || col.gameObject.tag == "MidBoss" || col.gameObject.tag == "PowerEnemy"
            || col.gameObject.tag == "SkillEnemy" || col.gameObject.tag == "SpeedEnemy")
            {
                Debug.Log("hit Player");
                float s = 100f * Time.deltaTime;
                transform.Translate(Vector3.up * s);
                player.OnDamage = true;

                player.Hp -= col.gameObject.GetComponent<BaseEnemy>().PowerEnemy;
                player.HpSlider.value = player.Hp;
            }
        
        if (player.Hp <= 0)
        {
            Debug.Log("Die");
            SceneManager.LoadScene("EndScene");
        }
        
    } 
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkillArea")
        {
            skillArea.SkillEnemySpawn = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "SkillArea")
        {
            skillArea.SkillEnemySpawn = false;
        }
    }
}
