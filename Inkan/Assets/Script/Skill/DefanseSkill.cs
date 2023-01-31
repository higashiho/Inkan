using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefanseSkill : BaseSkills
{
    // 構造体
    public static skills DefanseSkills;

    
    // 初期化関数
    public void StartDefanseSkills()
    {
        DefanseSkills.Hp = 10;
        DefanseSkills.Attack = 1;
        DefanseSkills.Speed = 0.4f;
        DefanseSkills.AttackSpeed = 0.3f;
    }
    private void Awake() 
    {
        StartSkillCallback += StartDefanseSkills;
    }
}
