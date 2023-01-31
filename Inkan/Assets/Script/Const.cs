using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Const : MonoBehaviour
{
    // エネミー生成用定数
    public const float DANGER_SPAWN_COUNT = 60.0f;                              // エリア外での生成速度
    public const float ENEMY_MINSPAWN_POS_X = 40.0f;                            // ランダム生成x座標最小値
    public const int MID_ENEMY_SPAWN = 3;                                       // 中ボス生成間隔
    public const float MID_MAX_HP = 30;                                         // 中ボスのＨｐ最大値
    public static readonly float[] PLAYER_LEVEL = {10.0f,20.0f,30.0f};          // プレイヤーレベル比較用
    public static readonly float[] SPAWN_COUNT = {120.0f,110.0f,80.0f};         // スポーン速度
    public static readonly float[] DANGER_SPAWN_POS = {20.0f, 100.0f};          // エリア外でのスポーン範囲
    public static readonly float[] SPAWN_POS = {12.0f, 15.0f, 19.0f, 40.0f};    // エリア内でのスポーン範囲
    public static readonly float[] MID_SPAWN_POS = {27.0f, 14.0f, 2.0f, 35.0f}; // 中ボスの生成位置


    // エリア用定数
    public const float MAX_DAMAGE_COUNT = 10.0f;                                    // ダメージ受けるまでの時間    
    public const float UNAREA_DAMAGE = 2;                                           // エリア外での受けるダメージ  
    public const float HEEL_AREA_DAMAGE = 2;                                        // ヒールポイントがない時のダメージ
    public const float HEEL = 1;                                                    // ヒールエリア回復量
    public const float MAX_HEEL_COUNT = 10.0f;                                      // ヒールエリア回復時間
    public const float MAX_HEEL_DAMAGE_COUNT = 2.0f;                                // ヒールエリアでのダメージ間隔
    public const int MAX_AREA_POINT = 100;                                          // ポイントエリアでのポイント加算
    public const float MAX_POINT_COUNT = 10.0f;                                     // ポイントエリア加算間隔
    public const float SKILL_AREA_SPAWN_POINT = 15.0f;                              // スキルエリアでのエネミー生成場所
    public static readonly float[] HEEL_WARP_POINT = {-30.0f,18.0f,20.0f,49.0f};    // ヒール後のワープポイント
    public static readonly float[] POINT_AREA_ENEMY_SPAWN_POS =                     // ポイントエリアでのエネミー生成場所
    {-11.0f, 11.0f,-46.0f, 36.0f,-2.0f, 11.0f,35.0f, 45.0f};


    // プレイヤー用定数
    public const float MAX_HP = 5;                      // hp最大値
    public const float SKILL_MAX = 2;                   //スキルのマックス所持数
    public const int MAX_BULLETS = 10;                  //弾の弾数
    public const float RELOADING_SPEED = 10.0f;          // リロードスピード
    public const float CHANGE_SPEED = 3.0f;             // 弾がなくなった時の変化スピード
    public const float INSTANCE_SKILL_POS = 10.0f;      // 初期スキル生成範囲
    public const float IRON_BALL_SPEED = 5.0f;          // 鉄球弾のスピード
    public const int IRON_BALL_BULLETS = 10;            // 鉄球弾の弾数
    public const int PENTERATING_BULLETS = 20;          // 貫通弾の弾数
    public const float SPEED_SKILL_SHOT_TIME = 0.2f;    // スピードスキル時の攻撃間隔
    public const float SPEED_SKILL_UP_SPEED = 2.0f;     // スピードアップスキルでのスピードアップ量
    public const float ATTACK_SKILL_MAX_HP = 4.0f;      // アタックスキルでのHp
    public const float ATTACK_SKILL_SHOT_TIME = 0.5f;   // アタックスキルでの攻撃間隔
    public const float ATTACK_SKILL_MOVE_SPEED = 4.0f;  // アタックスキルでの移動スピード
    public const float MOVE_SKILL_MAX_HP = 3.0f;        // 移動スピードスキルでのHp
    public const float MOVE_SKILL_MOVE_SPEED = 6.0f;   // 移動スピードスキルでの移動スピード
    public const float DEFENSE_SKILL_MAX_HP = 7.0f;     // 防御スキルでのHｐ
    public const float DEFENSE_SKILL_SHOT_TIME = 0.4f; // 防御スキルでの攻撃間隔
    public const float DEFENSE_SKILL_MOVE_SPEED = 3.5f; // 防御スキルでの移動スピード
    public const float HEEL_SKILL_MAX_HP = 4.0f;        // ヒールスキルでのHp
    public const float HEEL_SKILL_SHOT_TIME = 0.4f;    // ヒールスキルでの攻撃間隔
    public const float HEEL_SKILL_MOVE_SPEED = 4.5f;    // ヒールスキルでの移動スピード
    public const float LEVEL_MAX = 100;                 // レベル最大値
    public const float SKILL_POS = 15.0f;               // スキルアイテム生成範囲
    public const int LEVEL_POINT = 500;                 // レベルアップ時の加算点
    public const int MID_EXP_POINT = 1000;              // 中ボスの経験値取得時加算点
    public const int MID_EXP_UP = 10;                   // 中ボスの経験値加算量              
    public const int ENEMY_EXP_POINT = 100;             // 通常経験値取得時加算点
    public static readonly int[] LEVEL_BORDER =         // 経験値量変化範囲
        {10, 20, 30};


    // 時間管理関係
    public const float MINUTE = 60.0f;       // 一分
    public const int TIME_POINT = 300;       // 一分ごとのポイント
    public const int ON_10 = 10;             // 10以下数字判定用
}
