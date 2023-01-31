using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    [SerializeField]
    private Text timeObj = null;
    [SerializeField]
    private LevelUP level;

    //秒
    [SerializeField]
    private float totalTime = 0; 
    //イント変換表示用
    private int seconds = 0;  
    //分  
    public int Minute{get;private set;} = 0;     


    // Update is called once per frame
    void Update()
    {
        timerMove();
    }

    // タイマー挙動
    private void timerMove()
    {
        // 60秒ごとに
        if (totalTime >= Const.MINUTE)
        {
            totalTime = 0;
            Minute++;
            level.TimePointUp();
        }
        totalTime += Time.deltaTime;
        seconds = (int)totalTime;

        // 10以下の数字の場合０を表示
        if (seconds < Const.ON_10)
        {
            timeObj.text = "Time  " + Minute + ":0" + seconds;
        }
        else
        {
            timeObj.text = "Time  " + Minute + ":" + seconds;
        }
    }
}
