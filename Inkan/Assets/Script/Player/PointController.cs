using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointController : MonoBehaviour
{
    private int Score = 0;
    [SerializeField]
    private Text scoreObj = null; //スコアテキスト
    // Start is called before the first frame update
    void Start()
    {
        Score = LevelUP.Point;
        Score += PointArea.PlusPoint;
    }

    // Update is called once per frame
    void Update()
    {
        scoreObj.text = "Scores:" + Score;
    }
}
