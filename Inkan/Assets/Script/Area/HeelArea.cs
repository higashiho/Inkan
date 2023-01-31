using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeelArea : MonoBehaviour
{
    // 取得用
    [SerializeField]    //ヒールカウントテキスト
    private Text heelCountObject = null; 
    [SerializeField]    //デンジャーテキスト
    private Text dengerObject = null; 
    [SerializeField]    //ヒールスライダー
    private Slider HeelSlider; 
    [SerializeField]
    private PlayerController player;



    private float HeelCount = 10.0f;    //ヒールカウント
    private float HeelPoint = 3;        //ヒールできる回数
    private bool countDown = false;     // カウントダウンできるかフラグ


    //ヒールカウントがなくなった時用
    private float dengerCount = Const.MAX_HEEL_DAMAGE_COUNT;   //ダメージカウント


    void Update()
    {
        heelArea();    
    }

    // ヒールエリア処理
    private void heelArea()
    {
        heelCountObject.text = "HeelAreaPoint :" + HeelPoint;
        HeelSlider.value = HeelPoint;

        //ヒールポイントがある場合回復処理
        if (HeelPoint > 0) 
        {
            heelPoint(UnityEngine.Random.Range(Const.HEEL_WARP_POINT[0],Const.HEEL_WARP_POINT[1]),
                        UnityEngine.Random.Range(Const.HEEL_WARP_POINT[2],Const.HEEL_WARP_POINT[3]));
        }
        //ヒールカウントがなくなったらダメージ処理
        else    
        {
            unHeelPoint();
        }
    }

    // ヒールポイントがある場合のヒールエリアの処理
    private void heelPoint(float x, float y)
    {
        heelCountObject.text = "Heel:" + HeelCount.ToString("N2");
        if (countDown)
        {
            HeelCount -= Time.deltaTime;

            if (HeelCount <= 0)
            {
                player.Hp += Const.HEEL;
                HeelCount = Const.MAX_HEEL_COUNT;
                HeelPoint--;

                // ヒールしたら一定位置間にワープ
                this.transform.position = new Vector3(x, y, 0);
            }
        }
    }

    // ヒールポイントがない時のダメージ処理
    private void unHeelPoint()
    {
        heelCountObject.enabled = false;
        dengerObject.enabled = true;
        dengerObject.text = "Denger:" + dengerCount.ToString("N2");
        if (countDown)
        {
            dengerCount -= Time.deltaTime;
            if (dengerCount <= 0)
            {
                player.Hp -= Const.HEEL_AREA_DAMAGE;
                dengerCount = Const.MAX_HEEL_DAMAGE_COUNT;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HeelArea")
        {
            countDown = true;
            heelCountObject.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "HeelArea")
        {
            countDown = false;
            heelCountObject.enabled = false;
            dengerObject.enabled = false;
            HeelCount = Const.MAX_HEEL_COUNT;
            dengerCount = Const.MAX_HEEL_DAMAGE_COUNT;

        }
    }
}
