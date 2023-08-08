using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // 弾の移動速度
    [SerializeField] private float moveSpeed = 10f;
    // ダメージ発生距離
    [SerializeField] private float damageDistance = 0.1f;
    // ターゲット格納
    private Enemy enemyTarget;
    // ダメージ
    private float damage;
    // 弾を管理するコンポーネント
    private WeaponControl bulletControl;


    // Start
    private void Start()
    {
        
    }

    // Update
    void Update()
    {
        // 攻撃する対象がいるか判定
        if (enemyTarget != null)
        {
            // 弾を動かす関数の呼び出し
            MoveBullet();
        }
    }

    // 弾を動かす関数
    private void MoveBullet()
    {
        // 現在地から目的地まで一定速度で移動(現在地、目的地、1フレームで進む距離)
        transform.position = Vector2.MoveTowards(transform.position,
            enemyTarget.transform.position,
            moveSpeed * Time.deltaTime);

        // 弾と敵の距離を確認する関数の呼び出し
        CheckDistance();

    }

    // 弾と敵の距離を確認して近ければダメージを与える関数
    private void CheckDistance()
    {
        // 敵との距離(magnitude:長さを求める)
        float distanceToTarget = (enemyTarget.transform.position - transform.position).magnitude;

        // 十分近いか判定
        if (distanceToTarget < damageDistance)
        {
            // ダメージを与える
            enemyTarget.enemyHP.ReduceHP(damage);
            // 弾の設定を初期化
            bulletControl.ResetBullet();
            // 当たった弾は削除
            Destroy(gameObject);
        }
    }

    // 攻撃対象を設定する関数
    public void SetTargetEnemy(Enemy enemy)
    {
        enemyTarget = enemy;
    }

    // 弾の初期設定用関数
    public void BulletInitializetion(WeaponControl weaponControl, float damage)
    {
        // 引数を変数に格納
        bulletControl = weaponControl;
        this.damage = damage;
    }

}
