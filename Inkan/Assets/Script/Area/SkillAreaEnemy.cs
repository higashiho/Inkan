using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAreaEnemy : MonoBehaviour
{
    [SerializeField]
    private BaseEnemy prefabSkillAreaEnemy;


    // スキルエリアでの敵生成できるかフラグ
    private bool skillEnemySpawn;
    public bool SkillEnemySpawn{get{return skillEnemySpawn;}set {skillEnemySpawn = value;}}

    void Update()
    {
        skillAreaEnemySpawn(UnityEngine.Random.Range(-Const.SKILL_AREA_SPAWN_POINT, Const.SKILL_AREA_SPAWN_POINT),
                                UnityEngine.Random.Range(-Const.SKILL_AREA_SPAWN_POINT, Const.SKILL_AREA_SPAWN_POINT));     
        
    }

    // スキルエリアでのエネミー生成
    private void skillAreaEnemySpawn(float x, float y)
    {
        if (SkillEnemySpawn && Time.frameCount % Const.SPAWN_COUNT[0] == 0) 
        {
            Vector3 pos = new Vector3(x, y, 0.0f);

            //敵を生成
            FactoryEnemy.objectPool.Launch(pos, FactoryEnemy.objectPool.EnemyList, prefabSkillAreaEnemy);
        }
    }

    

}
