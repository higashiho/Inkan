using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        cameraMove();
    }

    // カメラの挙動
    private void cameraMove()
    {
        var playerPos = player.transform.position;
        var cameraPos = this.transform.position;
        //カメラとplayerの位置を同じにする
        transform.position = new Vector3(playerPos.x, playerPos.y, cameraPos.z);
    }

}