using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 現在のウェーブ
    [NonSerialized] public int currentWave;
    // 実際に値を変更させる体力用変数
    [NonSerialized] public int currentLife;
    // 体力の初期設定だけに使う数値
    [SerializeField] private int life = 10;


    // シングルトン
    public static LevelManager instance;


    // Awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start
    void Start()
    {
        // 現在のウェーブを設定
        currentWave = 1;
        // 体力を設定
        currentLife = life;
    }


    // ウェーブを更新する関数
    private void WaveCompleted()
    {
        currentWave++;
    }

    // オブジェクトが表示された時に呼ばれる
    private void OnEnable()
    {
        // イベントに関数を登録
        Spawner.OnWaveCompleted += WaveCompleted;
        Enemy.OnReachedGoal += ReduceLifes;
    }

    // オブジェクトが非表示になった時に呼ばれる
    private void OnDisable()
    {
        // イベントから関数を削除
        Spawner.OnWaveCompleted -= WaveCompleted;
        Enemy.OnReachedGoal -= ReduceLifes;

    }

    // ライフを減らして体力を確認する関数
    private void ReduceLifes()
    {
        currentLife--;

        // 体力が0以下でないか
        if (currentLife <= 0)
        {
            currentLife = 0;

            // ゲームオーバー処理
            Debug.Log("ゲームオーバー");
        }
    }

}
