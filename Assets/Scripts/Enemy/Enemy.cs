using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 敵のHP
    [NonSerialized] public EnemyHP enemyHP;
    // 移動速度設定用
    [SerializeField] private float setMoveSpeed = 3f;
    // 移動速度
    private float moveSpeed;
    [NonSerialized] public MovePoint movePoint;
    // 向かうべきポイントの番号
    private int currentMovePointIndex;
    // CurrentPointPositionが呼ばれるたびに後半の処理が行われる
    public Vector3 CurrentPointPosition =>
        movePoint.GetMovePointPosition(currentMovePointIndex);

    // Start
    void Start()
    {
        // 敵のHP格納
        enemyHP = GetComponent<EnemyHP>();

        // 向かうべきポイントを設定
        currentMovePointIndex = 0;
        // 移動スピードの設定
        SetMoveSpeed();
        // 指定のコンポーネントがついているオブジェクトを探して格納(FindObjectOfTypeは処理が重い)
        movePoint = FindObjectOfType<MovePoint>().GetComponent<MovePoint>();
    }

    // Update
    void Update()
    {
        // ポジションを移動させる関数の呼び出し
        Move();

        // 現在の目的ポイントに到着しているか
        if (NextPointReached())
        {
            // 目的地を設定する数値を更新する関数の呼び出し
            UpdatePointIndex();
        }
    }


    // 目的地を設定する数値を更新する関数
    private void UpdatePointIndex()
    {
        // 最後でないならポイントを更新する
        if (currentMovePointIndex < movePoint.points.Length - 1)
        {
            currentMovePointIndex++;
        }
    }

    // 現在の目的ポイントに到着しているか判定する関数
    private bool NextPointReached()
    {
        // 次のポイントまでの残りの距離を変数に格納
        float distance = (transform.position - CurrentPointPosition).magnitude;

        // 距離が近いか判定
        if (distance < 0.1)
        {
            // 到着判定
            return true;
        }

        return false;
    }

    // スピードを格納する関数
    private void SetMoveSpeed()
    {
        moveSpeed = setMoveSpeed;
    }

    // ポジションを移動させる関数
    private void Move()
    {
        // 引数(現在位置、目的の位置、1フレームで進む距離)
        transform.position = Vector3.MoveTowards(
            transform.position,
            CurrentPointPosition,
            moveSpeed * Time.deltaTime);
    }

}
