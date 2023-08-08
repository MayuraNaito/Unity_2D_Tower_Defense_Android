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

    // Start
    void Start()
    {
        CreateHealthBar();
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
            // 非表示にする
            gameObject.SetActive(false);
        }
    }

}
