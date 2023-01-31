using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedSkill : BaseSkills
{
    // 構造体
    public static skills SpeedSkills;

    
    // 初期化関数
    public void StartSpeedSkill()
    {
        SpeedSkills.Hp = 4;
        SpeedSkills.Attack = 0.8f;
        SpeedSkills.Speed = 0.5f;
        SpeedSkills.AttackSpeed = 1;
    }
    private void Awake() 
    {
        StartSkillCallback += StartSpeedSkill;
    }
}
