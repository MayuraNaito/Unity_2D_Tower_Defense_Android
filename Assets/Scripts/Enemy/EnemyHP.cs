using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHP : MonoBehaviour
{
    // 設定用のHP
    [SerializeField] private float hp = 10f;
    // 現在のHP
    [NonSerialized] public float currentHP;
    // HPを表示するUI
    [SerializeField] private GameObject hpBar;
    // バーのポジション
    [SerializeField] private Transform barPos;
    // HPゲージ画像
    private Image hpBarImage;
    // 被弾した時用デリゲート(デリゲート：関数を入れられる変数)
    public static Action<Enemy> OnEnemyHit;
    // 敵を格納
    private Enemy enemy;
    // アニメーションを格納
    private EnemyAnimations enemyAnimations;

    // 死んだ時の処理
    public static Action OnEnemyDead;


    // Start
    void Start()
    {
        // HPバーの生成用関数の呼び出し
        CreateHealthBar();
        // 敵、アニメーションのコンポーネント格納
        enemy = GetComponent<Enemy>();
        enemyAnimations = GetComponent<EnemyAnimations>();
    }

    // Update
    void Update()
    {
        // 左クリックが押された時
        if (Input.GetMouseButtonDown(0))
        {
            ReduceHP(5);
        }

        // HPバーの表示更新(現在の数値、目的の数値、割合)
        hpBarImage.fillAmount = Mathf.Lerp(hpBarImage.fillAmount,
            currentHP / hp, Time.deltaTime * 10f);
    }


    // HPバーの生成用関数
    private void CreateHealthBar()
    {
        // 生成して親オブジェクトを設定(ゲームオブジェクトとしての設定)
        GameObject newBar = Instantiate(hpBar, barPos.position, Quaternion.identity); // オブジェクトを設定
        newBar.transform.SetParent(transform); // Enemyのオブジェクトを親の子として移動させる

        // newBarから変数に体力バーを格納する(HPバーとしての設定)
        EnemyHPBar healthBar = newBar.GetComponent<EnemyHPBar>();

        // 画像を変数に格納
        hpBarImage = healthBar.hpBarImage;

        // HPの更新
        currentHP = hp;
    }

    // HPを減らす関数
    public void ReduceHP(float damage)
    {
        currentHP -= damage;

        // 被弾アニメーション(デリゲートの呼び出し)
        OnEnemyHit?.Invoke(enemy);

        // HPが0かどうか判定する関数の呼び出し
        DeathCheck();
    }

    // HPが0になっていないか判定する関数
    private void DeathCheck()
    {
        // HPの確認
        if (currentHP <= 0)
        {
            // マイナスの可能性があるのでHPを0にする
            currentHP = 0;

            // 被弾アニメーションを再生させてから死亡
            Invoke("Die", enemyAnimations.StopTime());
        }
    }

    // 死亡時の処理用関数
    private void Die()
    {
        // HPをリセットする関数の呼び出し
        ResetHealth();
        // アクションを呼ぶ
        OnEnemyDead?.Invoke();
        // プールに返す関数の呼び出し
        ObjectPooler.ReturnToPool(gameObject);
    }

    // HPをリセットする関数(敵オブジェクトをプールさせる用)
    public void ResetHealth()
    {
        // HPをMAXにする
        currentHP = hp;
        // HPゲージをMAXにする
        hpBarImage.fillAmount = 1f;

        // オブジェクト非表示
        gameObject.SetActive(false);
    }

}
