using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTest : MonoBehaviour
{
    // カメラの位置調整をするためのスクリプト。CameraTestもあるけどこっちがほぼメイン

    public List<Transform> players = new List<Transform>();
    // 現在のプレイヤー数を数えるためのリスト。10/10時点でGameManagerにまとめたほうがいいと考えている。

    float height;
    // カメラの高さ

    private void Update()
    {
        Vector3 pos = Vector3.zero;
        // 計算用のローカルVector3を設定

        foreach(Transform t in players)
        {
            pos += t.position;
            // プレイヤーの座標をposに足していく
        }

        Vector3 center = pos / players.Count;
        // 足した座標をプレイヤーの数で割って全員の位置の平均を求める

        transform.position = center;
        // 求めた平均の座標をオブジェクトに適用する

        height = 0;
        // 一度高さをリセットする

        foreach(Transform t in players)
        {
            Vector3 tPos = new Vector3(t.position.x, t.position.y, t.position.z * 16 / 9);
            // プレイヤーの位置を計算する。カメラの縦横比の関係でめんどくさい計算式になっている。書いた人もあんまわかってない
            float f = Vector3.Distance(center, tPos);
            // プレイヤーの位置と中心の位置の差を比べる

            if (f < 0) f *= -1;
            // 差の値がマイナスになったらプラスに変える

            height = Mathf.Max(height,f);
            // 中心から一番離れたプレイヤーを探す。
        }
    }

    public float Height()
    {
        return height;
        // 関数が呼ばれたら高さの値を返す
    }
}
