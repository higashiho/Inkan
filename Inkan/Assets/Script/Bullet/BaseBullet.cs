using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseBullet : MonoBehaviour
{
    [SerializeField]    // 消える時間
    protected float deleteTime;
    // 時間計測
    protected float time;
    public float TimeManage{get{return time;}set {time = value;}}

    [SerializeField]    // 弾のダメージ
    protected float bulletPower;
    public float BulletPower{get {return bulletPower;} private set{bulletPower = value;}}

    [SerializeField]    // 弾の耐久力
    protected float bulletHp;

    public UnityAction<BaseBullet> objectPoolCallBack;

    
    // 一定時間後に格納
    protected void reDestroy()
    {
        time += Time.deltaTime;

        if(time >= deleteTime)
            objectPoolCallBack?.Invoke(this);
    }
    
}
