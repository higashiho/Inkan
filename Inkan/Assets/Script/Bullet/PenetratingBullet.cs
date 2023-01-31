using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenetratingBullet : BaseBullet
{

    void Update()
    {
        reDestroy();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "wall")
        {
            objectPoolCallBack?.Invoke(this);
        }
    }
}
