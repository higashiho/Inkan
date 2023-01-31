using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStrat : MonoBehaviour
{
    [SerializeField]
    private ButtonController button;

    [SerializeField]    // スキルの生成オブヘクト
    private GameObject[] skillObj = new GameObject[5];

    public void OnClickStartButton()
    {
        var pos = new Vector3(UnityEngine.Random.Range(-Const.INSTANCE_SKILL_POS,Const.INSTANCE_SKILL_POS),
                        UnityEngine.Random.Range(-Const.INSTANCE_SKILL_POS,Const.INSTANCE_SKILL_POS), 0);
        switch(button.skillItemStatus)
        {
            case ButtonController.skillItemState.ATTACK_SKILL:
                Instantiate(skillObj[0],pos,Quaternion.identity);
                break;
            case ButtonController.skillItemState.DEFANSE_SKILL:
                Instantiate(skillObj[1],pos,Quaternion.identity);
                break;
            case ButtonController.skillItemState.SPEED_SKILL:
                Instantiate(skillObj[2],pos,Quaternion.identity);
                break;
            case ButtonController.skillItemState.MOVE_SPEED_SKILL:
                Instantiate(skillObj[3],pos,Quaternion.identity);
                break;
            case ButtonController.skillItemState.HEEL_SKILL:
                Instantiate(skillObj[4],pos,Quaternion.identity);
                break;
            default:
                break;
        }
        SceneManager.LoadScene("BattleScene");
    }
}


