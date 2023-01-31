using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{
    // どのスキルを生成するか
    public enum skillItemState
    {
        NO_SKILL,
        ATTACK_SKILL,
        DEFANSE_SKILL,
        SPEED_SKILL,
        MOVE_SPEED_SKILL,
        HEEL_SKILL
    }
    public skillItemState skillItemStatus{get;private set;} = skillItemState.NO_SKILL;

    [SerializeField]    // HPスライダー
    private Slider hpSlider;
    [SerializeField]    // 攻撃スライダー
    private Slider attackSlider;
    [SerializeField]    // 移動スピードスライダー
    private Slider speedSlider;
    [SerializeField]    // 攻撃スピードスライダー
    private Slider attackSpeedSlider;

    [SerializeField]    // 説明テキスト
    private Text ExpoObj = null;

    [SerializeField]    // 表示アイテム
    private Image popItemImage = null;

    void Start()
    {
        hpSlider.maxValue = 10;
        attackSlider.maxValue = 2;
        speedSlider.maxValue = 1;
        attackSpeedSlider.maxValue = 1;

        // アイテムの構造体変数初期化
        BaseSkills.StartSkillCallback?.Invoke();
    }

    // アタックスキル
    public void AttackSkillMove()
    {
        skillItemStatus = skillItemState.ATTACK_SKILL;

        hpSlider.value = AttackSkill.AttackSkills.Hp;
        attackSlider.value = AttackSkill.AttackSkills.Attack;
        speedSlider.value = AttackSkill.AttackSkills.Speed;
        attackSpeedSlider.value = AttackSkill.AttackSkills.AttackSpeed;

        ExpoObj.text = "攻撃力は強いが、HPがやや低く移動力と攻撃速度が遅い。";

        // 色変更
        popItemImage.color = Color.red;
    }

    // ディフェンススキル
    public void DefanseSkillMove()
    {
        skillItemStatus = skillItemState.DEFANSE_SKILL;

        hpSlider.value = DefanseSkill.DefanseSkills.Hp;
        attackSlider.value = DefanseSkill.DefanseSkills.Attack;
        speedSlider.value = DefanseSkill.DefanseSkills.Speed;
        attackSpeedSlider.value = DefanseSkill.DefanseSkills.AttackSpeed;

        ExpoObj.text = "HPは高いが、速度と攻撃速度が遅い";

        // 表示アイテム色変更
        var red = 95f / 255f; var green = 65f / 255f;
        popItemImage.color = new Color(red,green,0,1);

    }
    

    // 攻撃スピードスキル
    public void SpeedSkillMove()
    {
        skillItemStatus = skillItemState.SPEED_SKILL;

        hpSlider.value = SpeedSkill.SpeedSkills.Hp;
        attackSlider.value = SpeedSkill.SpeedSkills.Attack;
        speedSlider.value = SpeedSkill.SpeedSkills.Speed;
        attackSpeedSlider.value = SpeedSkill.SpeedSkills.AttackSpeed;

        ExpoObj.text = "攻撃速度は非常に速いが、攻撃力とHPがやや低め。";

        // 表示アイテム色変更
        popItemImage.color = Color.green;
    }
    

    // ヒールススキル
    public void HeelSkillMove()
    {
        skillItemStatus = skillItemState.HEEL_SKILL;

        hpSlider.value = HeelSkill.HeelSkills.Hp;
        attackSlider.value = HeelSkill.HeelSkills.Attack;
        speedSlider.value = HeelSkill.HeelSkills.Speed;
        attackSpeedSlider.value = HeelSkill.HeelSkills.AttackSpeed;

        ExpoObj.text = "全体的に能力値はやや低めだが、HPを回復できるヒールを3回使用できる。Rキーを押すことで使用可能。";
    
        // 表示アイテム色変更
        popItemImage.color = Color.magenta;
    }
    

    // ムーブスピードスキル
    public void MoveSpeedSkillMove()
    {
        skillItemStatus = skillItemState.MOVE_SPEED_SKILL;

        hpSlider.value = MoveSpeedSkill.MoveSpeedSkills.Hp;
        attackSlider.value = MoveSpeedSkill.MoveSpeedSkills.Attack;
        speedSlider.value = MoveSpeedSkill.MoveSpeedSkills.Speed;
        attackSpeedSlider.value = MoveSpeedSkill.MoveSpeedSkills.AttackSpeed;

        ExpoObj.text = "移動速度は速いが、HPが非常に少なく、攻撃力もやや低め。";
    
        // 表示アイテム色変更
        popItemImage.color = Color.yellow;
    }
}
