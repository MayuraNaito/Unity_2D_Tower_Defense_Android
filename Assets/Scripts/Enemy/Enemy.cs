using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 敵のHP
    public EnemyHP enemyHP;

    // Start
    void Start()
    {
        // 敵のHP格納
        enemyHP = GetComponent<EnemyHP>();
    }

    // Update
    void Update()
    {
        
    }
}
