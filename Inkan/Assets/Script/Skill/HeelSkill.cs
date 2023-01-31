using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeelSkill : BaseSkills
{
    
    // 構造体
    public static skills HeelSkills;

    
    // 初期化関数
    public void StartHeelSkill()
    {
        HeelSkills.Hp = 4;
        HeelSkills.Attack = 1;
        HeelSkills.Speed = 0.5f;
        HeelSkills.AttackSpeed = 0.3f;
    }
    private void Awake() 
    {
        StartSkillCallback += StartHeelSkill;
    }
}