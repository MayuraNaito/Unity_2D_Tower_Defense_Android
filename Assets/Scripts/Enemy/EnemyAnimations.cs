using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    // アニメーション、敵を格納
    private Animator animator;
    private Enemy enemy;


    // Start
    void Start()
    {
        // アニメーション、敵コンポーネントを格納
        animator = GetComponent<Animator>();
        enemy = GetComponent<Enemy>();
    }


    // ヒットアニメーションを再生する関数
    private void PlayHitAnimation()
    {
        animator.SetTrigger("Hit");
    }

    // 移動を止めつつアニメーションを再生する関数
    private void EnemyHit(Enemy enemy)
    {
        if (this.enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }

    // ヒット時の挙動用関数
    private IEnumerator PlayHurt()
    {
        // 移動スピードを0にする関数の呼び出し
        enemy.StopMovement();
        // アニメーションの再生をする関数の呼び出し
        PlayHitAnimation();
        // 再生中は待機する
        yield return new WaitForSeconds(StopTime());
        // 移動を再開
        enemy.SetMoveSpeed();
    }

    // アニメーションの再生時間を返す関数
    public float StopTime()
    {
        // アニメーションの長さ + 調整数値を返す
        return animator.GetCurrentAnimatorClipInfo(0).Length + 0.3f;
    }

    // デリゲートに関数を登録
    // オブジェクトが表示された時に呼び出される関数
    private void OnEnable()
    {
        EnemyHP.OnEnemyHit += EnemyHit;
    }

    // オブジェクトが消えた時に呼び出される関数
    private void OnDisable()
    {
        EnemyHP.OnEnemyHit -= EnemyHit;
    }

}
