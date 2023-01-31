using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackSkill : BaseSkills
{
    // 構造体
    public static skills AttackSkills;

    
    // 初期化関数
    public void StartAttackSkill()
    {
        AttackSkills.Hp = 4;
        AttackSkills.Attack = 2;
        AttackSkills.Speed = 0.4f;
        AttackSkills.AttackSpeed = 0.25f;
    }
    private void Awake() 
    {
        StartSkillCallback += StartAttackSkill;
    }
}
