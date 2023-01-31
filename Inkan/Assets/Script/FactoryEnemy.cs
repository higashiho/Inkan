using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryEnemy : MonoBehaviour
{
    
    [SerializeField]    // 弾格納リスト
    private List<BaseEnemy> enemyList = new List<BaseEnemy>(15);
    public List<BaseEnemy> EnemyList{get{return enemyList;}private set{enemyList = value;}}

    // 自身を格納
    public static FactoryEnemy objectPool{get;private set;}

    private void Awake() 
    {
        objectPool = this;
    }

    // 生成関数　第一引数：座標　
    // 第三引数：Listでの生成　第四引数：表示したいオブジェクト
    public BaseEnemy Launch
    (Vector3 _pos, List<BaseEnemy> tmpList, BaseEnemy obj)
    {
        BaseEnemy tmpObj = null;
        // オブジェクトを取り出す
        tmpObj = judgObj(obj);
    
        // オブジェクトがnullで帰ってきたら新規作成
        if (tmpObj == null) 
        {
            tmpObj = Instantiate(obj, _pos, Quaternion.identity,transform);
            tmpList.Add(tmpObj);
        }

        // イベントに回収処理が入っていない場合新しく入れる
        if(tmpObj.objectPoolCallBack == null)  
            tmpObj.objectPoolCallBack = Collect;
        // 座標設定
        showInStage(_pos, tmpObj);
        // 表示
        tmpObj.gameObject.SetActive(true);
        //呼び出し元に渡す
        return tmpObj;
    }

    
    // 生成されるオブジェクトが何か判断
    private BaseEnemy judgObj(BaseEnemy target)
    {
        BaseEnemy tmpObj = null;

        foreach(BaseEnemy obj in EnemyList)
        {   
            // 非アクティブかつ表示したいオブジェクトか確認
            if(!obj.gameObject.activeSelf && obj.gameObject.tag == target.gameObject.tag)
                tmpObj = obj;
        }

        return tmpObj;
        
    }

     // 回収処理　第一引数：格納するList 第二引数：回収されるオブジェクト
    public static void Collect(BaseEnemy obj)
    {
        obj.TimeManage = 0;
        //ゲームオブジェクトを非表示
        obj.gameObject.SetActive(false);
    }

    // 座標設定関数 第一引数：生成する座標 第二引数：生成されるオブジェクト
    private void showInStage(Vector3 _pos, BaseEnemy obj)
    {
        obj.transform.position = _pos;
    }
}