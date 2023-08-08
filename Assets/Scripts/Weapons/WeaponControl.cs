using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControl : MonoBehaviour
{
    // 弾を生成する位置
    [SerializeField] private Transform bulletSpawnPos;
    // 攻撃準備中の攻撃用の弾
    private Bullet currentBullet;
    // 武器を格納
    private Weapon weapon;
    // 設定用の弾のダメージ
    [SerializeField] private float damage = 2f;
    // アップグレードする際の変更用
    [NonSerialized] public float bulletDamage;
    // 弾の生成用
    public GameObject fireBullet;
    // 弾の発射の間隔
    [SerializeField] private float firingInterval = 2f;
    // 次の弾までの時間
    private float nextFireTime;
    // ディレイ
    [NonSerialized] public float delay;


    // Start
    private void Start()
    {
        // 武器を変数に格納
        weapon = GetComponent<Weapon>();
        // 設定用変数の数値を格納
        bulletDamage = damage;
        delay = firingInterval;
        // 弾をセットする関数の呼び出し
        ReloadBullet();

    }

    // Update
    private void Update()
    {
        // 弾が装填されていない場合
        if (!currentBullet)
        {
            // 弾をセットする関数の呼び出し
            ReloadBullet();
        }
        //else
        //{
        //    // すでに弾がある場合
        //    // 親の設定を削除
        //    currentBullet.transform.parent = null;
        //    // 弾の攻撃対象を設定
        //    currentBullet.SetTargetEnemy(weapon.currentEnemyTarget);
        //}

        // 一定時間たったか判定
        if (Time.time > nextFireTime)
        {
            // 
            if (Shootable())
            {
                // 親の設定を削除
                currentBullet.transform.parent = null;
                // 弾の攻撃対象を設定
                currentBullet.SetTargetEnemy(weapon.currentEnemyTarget);
            }

            // 次の攻撃までの時間を設定
            nextFireTime = Time.time + delay;
        }
    }


    // 射撃可能か判定する関数
    private bool Shootable()
    {
        // 判定してbool値を返す(ウェポンにターゲットがいるか、バレットがリロードされているか、ターゲットのHPが0より大きいか)
        return weapon.currentEnemyTarget != null &&
            currentBullet != null &&
            weapon.currentEnemyTarget.enemyHP.currentHP > 0f;
    }

    // 弾をセットする関数
    private void ReloadBullet()
    {
        // 弾を生成してポジションと親を設定(Instantiate:プレハブを指定)
        GameObject newBullet = Instantiate(fireBullet);
        newBullet.transform.localPosition = bulletSpawnPos.position;
        newBullet.transform.SetParent(bulletSpawnPos);

        // コンポーネントに格納してダメージなどの初期設定
        currentBullet = newBullet.GetComponent<Bullet>();
        // 初期設定用関数の呼び出し
        currentBullet.BulletInitializetion(this, bulletDamage);
    }

    // 装填中の弾の設定を消す関数
    public void ResetBullet()
    {
        currentBullet = null;
    }

}
