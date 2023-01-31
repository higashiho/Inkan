using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUP : MonoBehaviour
{
    [SerializeField]    //レベルアップスキル生成
    public GameObject[] skillPrefabs;      
    [SerializeField]    //EXPスライダー
    private Slider expSlider;    
    [SerializeField]    //Levelテキスト
    private Text levelObj = null; 
    [SerializeField]
    private TimerController timerController;


    // プレイヤーのレベル
    public int PlayerLevel{get;private set;} = 1;
    

    // 所持経験値
    private int exp = 0;
    // 必要経験値
    private int requiredExp = 10; 

    // 生成するスキル
    private int number;    
    public static int Point{get; private set;} = 0;

    // Start is called before the first frame update
    void Start()
    {
        expSlider.maxValue = requiredExp;
    }


    // レベルUI
    private void levelPurint()
    {
        levelObj.text = "Level:" + PlayerLevel;
        if (PlayerLevel == Const.LEVEL_MAX)
        {
            levelObj.text = "LevelMax";
        }

    }

    // レベルアップ処理
    private void levelUp(float x, float y)
    {
        PlayerLevel++;
        exp = 0;
        expSlider.value = exp;
        Point += Const.LEVEL_POINT;
        number = Random.Range(0, skillPrefabs.Length);
        Vector3 pos = new Vector3(x, y, 0.0f);

        //ランダムにスキル生成
        Instantiate(skillPrefabs[number], pos, Quaternion.identity);
    }

    // レベル判定
    private void levelMove()
    {
        switch(PlayerLevel)
        {
            case int i when i > Const.LEVEL_BORDER[2]:
                if (exp >= requiredExp)
                {
                    levelUp(UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS),
                            UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS));
                }
                break;
            case int i when i > Const.LEVEL_BORDER[1]:
                if (exp >= requiredExp)
                {
                    if(PlayerLevel == Const.LEVEL_BORDER[2])   
                    {
                        requiredExp += requiredExp;
                        expSlider.maxValue = requiredExp;
                    }
                    levelUp(UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS),
                            UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS));
                }
                break;
            case int i when i > Const.LEVEL_BORDER[0]:
                if (exp >= requiredExp)
                {
                    if(PlayerLevel == Const.LEVEL_BORDER[1]) 
                    {
                        requiredExp += requiredExp;
                        expSlider.maxValue = requiredExp;
                    }  
                    levelUp(UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS),
                            UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS));               
                }
                break;
            case int i when i > 0:
                if (exp >= requiredExp)
                {
                    if(PlayerLevel == Const.LEVEL_BORDER[0])
                    {   
                        requiredExp += requiredExp;
                        expSlider.maxValue = requiredExp;
                    }
                    levelUp(UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS),
                            UnityEngine.Random.Range(-Const.SKILL_POS, Const.SKILL_POS));                
                }
                
                break;
            default: 
                break;
        }
        levelPurint();
    }

    // 一分ごとにポイントアップ
    public void TimePointUp()
    {
        Point += Const.TIME_POINT;
    }

    // 当たり判定処理
    private void OnCollisionEnter2D(Collision2D col) 
    {

        if(col.gameObject.tag == "MidEXP")
        {
            Debug.Log("Get MidEXP");
            exp += Const.MID_EXP_UP;
            expSlider.value = exp;
            Point += Const.MID_EXP_POINT;
            levelMove();
        }
        if (col.gameObject.tag == "EXP")
        {
            Debug.Log("Get EXP");
            exp++;
            expSlider.value = exp;
            Point += Const.ENEMY_EXP_POINT;
            levelMove();
        }
    }
}

