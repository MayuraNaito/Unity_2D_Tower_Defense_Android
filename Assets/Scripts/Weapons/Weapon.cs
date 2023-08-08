using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 武器の攻撃範囲
    public float attackRange = 3f;
    // エネミーを格納するリスト
    private List<Enemy> enemies;
    // 攻撃範囲内にいるターゲットを1体格納
    [NonSerialized] public Enemy currentEnemyTarget;

    // Start
    void Start()
    {
        // プレイ開始時に数値を合わせる
        GetComponent<CircleCollider2D>().radius = attackRange;
        // リストの初期化、インスタンス生成
        enemies = new List<Enemy>();
    }

    // Update
    void Update()
    {
        // ターゲットを取得
        GetCurrentTarget();
    }


    // ターゲットを取得する関数
    private void GetCurrentTarget()
    {
        // リストに敵がいない時
        if (enemies.Count <= 0)
        {
            // 設定をnullにする
            currentEnemyTarget = null;
            return;
        }

        // リストから設定
        currentEnemyTarget = enemies[0];
    }

    // ギズモの描写を変更する関数
    private void OnDrawGizmos()
    {
        // 円のギズモ(発生位置、どのくらいの位置)
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    // コライダーにぶつかった時に呼ばれる関数(ぶつかった相手の情報:collision)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 攻撃範囲に入った時の処理
        if (collision.CompareTag("Enemy"))
        {
            // 円に入った敵をリストに敵を格納
            Enemy enemy = collision.GetComponent<Enemy>();
            enemies.Add(enemy);
        }
    }

    // コライダーから離れた時に呼ばれる関数(ぶつかった相手の情報:collision)
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 攻撃範囲から出た時の処理
        if (collision.CompareTag("Enemy"))
        {
            // 円から出た敵をリストに敵を格納
            Enemy enemy = collision.GetComponent<Enemy>();

            // リストの中に引数の要素があるか判定
            if (enemies.Contains(enemy))
            {
                // いたらリストから削除
                enemies.Remove(enemy);
            }
        }
    }

}
