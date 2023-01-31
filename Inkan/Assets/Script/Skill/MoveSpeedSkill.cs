using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSpeedSkill : BaseSkills
{
    
    
    // 構造体
    public static skills MoveSpeedSkills;

    
    // 初期化関数
    public void StartMoveSpeedSkill()
    {
        MoveSpeedSkills.Hp = 3;
        MoveSpeedSkills.Attack = 0.8f;
        MoveSpeedSkills.Speed = 1;
        MoveSpeedSkills.AttackSpeed = 0.5f;
    }
    private void Awake() 
    {
        StartSkillCallback += StartMoveSpeedSkill;
    }  
}
