using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スポーンモード
public enum SpawnModes
{
    constant,// 一定
    Random// ランダム
}

public class Spawner : MonoBehaviour
{
    // スポーンモード選択
    [SerializeField] private SpawnModes spawnMode = SpawnModes.constant;
    // 最短のスポーン間隔
    [SerializeField] private float minRandomDelay;
    // 最長のスポーン間隔
    [SerializeField] private float maxRandomDelay;
    // 一定モードのスポーン時間
    [SerializeField] private float constantSpawnTime;
    // スポーンさせる数を設定する数
    [SerializeField] private int enemyCount = 10;
    // タイマー変数
    private float spawnTimer;
    // スポーンさせた数(数を追加していく)
    private float spawned;
    // 敵のオブジェクトプール用
    private ObjectPooler pooler;
    // MovePoint格納用(敵に渡すためにここで格納)
    private MovePoint movePoint;


    // Start
    void Start()
    {
        // 敵のプール、ポイントを格納
        pooler = GetComponent<ObjectPooler>();
        movePoint = GetComponent<MovePoint>();
    }

    // Update
    void Update()
    {
        // spawnTimerの時間を減らす(時間がなくなったら敵を生成)
        spawnTimer -= Time.deltaTime;
        // 時間の確認
        if (spawnTimer < 0)
        {
            // 次のスポーンまでの時間を設定
            spawnTimer = GetSpawnDelay();
            // スポーン上限の確認(スポーンさせた数 < スポーンさせる数)
            if (spawned < enemyCount)
            {
                // 生成済みの数を追加
                spawned++;
                // 敵を生成
                SpawnEnemy();
            }
        }
    }


    // 一定orランダムの数値を返す関数
    private float GetSpawnDelay()
    {
        if (spawnMode == SpawnModes.constant)
        {
            return constantSpawnTime;
        }
        else
        {
            // 引数の間からランダムな数値を選んで返す
            return UnityEngine.Random.Range(minRandomDelay, maxRandomDelay);
        }
    }

    // 敵を生成する関数
    private void SpawnEnemy()
    {
        // プールから取得して変数に格納
        GameObject newInstance = pooler.GetObjectFromPool();
        // 敵の初期設定
        SetEnemy(newInstance);
        // 表示
        newInstance.SetActive(true);
    }

    // 敵をセットする関数
    private void SetEnemy(GameObject newInstance)
    {
        // enemyでmovePointを使えるように格納
        Enemy enemy = newInstance.GetComponent<Enemy>();
        enemy.movePoint = movePoint;

        // 生成位置をこのオブジェクトの位置に設定
        enemy.transform.position = transform.position;

        // スピードの設定
        enemy.SetMoveSpeed();
    }

}
