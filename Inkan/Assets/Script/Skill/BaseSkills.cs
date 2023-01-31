using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// スライダーvalue設定用構造体
public struct skills
{
    // HPスライダー値
    public float Hp;
    // 攻撃力スライダー値
    public float Attack;
    // 移動速度スライダー値
    public float Speed;
    // 攻撃スピードスライダー値
    public float AttackSpeed;
}

public class BaseSkills : MonoBehaviour
{
    // 初期化イベント格納用
    public static UnityAction StartSkillCallback;
}
