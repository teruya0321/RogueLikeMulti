using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraTest : MonoBehaviour
{
    // カメラについている方のスクリプト。あんまいらない気もする
    public Transform target;
    // 中心となるオブジェクトの位置情報

    public TargetTest targetF;
    // 中心のオブジェクトにくっついてる関数。毎回取得するのめんどくさいからpublicだけどまとめてもいいかもしれない

    void Update()
    {
        Vector3 pos = Vector3.Lerp(transform.position,target.position, Time.deltaTime * 2);
        // カメラをなめらかに移動させるために変数に目的の座標と現在の座標を代入する

        pos.y = targetF.Height() + 5 - targetF.players.Count;
        // 高さを決める。プレイヤーの数によって高さの下限を減らす

        transform.position = pos;
        // 高さが決まったらカメラの位置に代入する
    }
}
