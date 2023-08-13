using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // 現在のウェーブ
    [NonSerialized] public int currentWave;


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
    }


    // ウェーブを更新する関数
    private void WaveCompleted()
    {
        currentWave++;
    }

    private void OnEnable()
    {
        Spawner.OnWaveCompleted += WaveCompleted;
    }

    private void OnDisable()
    {
        Spawner.OnWaveCompleted -= WaveCompleted;

    }

}
