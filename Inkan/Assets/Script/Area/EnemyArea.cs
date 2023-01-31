using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyArea : MonoBehaviour
{
    // 取得用
    [SerializeField]    //デンジャーテキスト
    private Text dengerObject = null; 
    [SerializeField]    // エネミー生成
    private RandEnemy randEnemy;
    [SerializeField]
    private PlayerController player;


    //ダメージカウント
    private float dengerCount = Const.MAX_DAMAGE_COUNT;   
    // エリア外にいるか
    private bool countDown = false;

    void Update()
    {
        unArea();
    }

    // エリア外の処理
    private void unArea()
    {
        dengerObject.text = "Denger:" + dengerCount.ToString("N2");
        if (countDown)
        {
            dengerCount -= Time.deltaTime;
            if (dengerCount <= 0)
            {
                player.Hp -= Const.UNAREA_DAMAGE;
                dengerCount = Const.MAX_DAMAGE_COUNT;
            }
        }
    }

    // 以下当たり判定
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Area")
        {
            randEnemy.Nomal = true;
            randEnemy.Danger = false;
            countDown = false;
            dengerObject.enabled = false;
            dengerCount = Const.MAX_DAMAGE_COUNT;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Area")
        {
            randEnemy.Nomal = false;
            randEnemy.Danger = true;
            countDown = true;
            dengerObject.enabled = true;
        }
    }
}
