using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    // 生成するアイテム
    [SerializeField] private GameObject poolObject;
    // 生成数
    [SerializeField] private int poolSize = 10;
    // 生成したものを管理するリスト
    private List<GameObject> pool;
    // 親オブジェクトを格納
    private GameObject poolContainer;



    // Awake
    private void Awake()
    {
        // インスタンス化
        pool = new List<GameObject>();
        // オブジェクト生成して名前をつけて変数に格納
        poolContainer = new GameObject($"Pool - {poolObject.name}");
        // プールオブジェクトの作成
        CreatePooler();
    }


    // poolSize分オブジェクトを生成する関数
    private void CreatePooler()
    {
        // poolSize分ループ
        for (int i = 0; i < poolSize; i++)
        {
            // 生成したオブジェクトをリストに格納
            pool.Add(CreateObject());
        }
    }

    // poolObjectを生成して呼んだ場所に返す関数
    private GameObject CreateObject()
    {
        // オブジェクトを生成して変数に格納
        GameObject newInstance = Instantiate(poolObject);
        // 親の設定
        newInstance.transform.SetParent(poolContainer.transform);
        // 非表示(必要になった時に表示したいから)
        newInstance.SetActive(false);

        return newInstance;
    }

    // プールからオブジェクトを引き出す関数
    public GameObject GetObjectFromPool()
    {
        // リストに格納されている数分ループ
        for (int i = 0; i < pool.Count; i++)
        {
            // プール内の非表示オブジェクトだったら
            if (!pool[i].activeInHierarchy)
            {
                // 呼び出した場所に返す
                return pool[i];
            }
        }

        // 足りない場合は生成して返す
        return CreateObject();
    }

    // プールにオブジェクトを返却する関数
    public static void ReturnToPool(GameObject instance)
    {
        // 非表示
        instance.SetActive(false);
    }

}
