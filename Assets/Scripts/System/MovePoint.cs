using UnityEngine;

public class MovePoint : MonoBehaviour
{
    // ポイント格納配列
    public Vector3[] points;


    // ギズモ表示する関数
    private void OnDrawGizmos()
    {
        // 配列に格納されている数値分処理する
        for (int i = 0; i < points.Length; i++)
        {
            // 色を指定
            Gizmos.color = Color.blue;
            // ポジション、半径
            Gizmos.DrawWireSphere(points[i], 0.5f);
        }
    }

    // 引数で渡された数値を同じポイントの位置を返す関数
    public Vector3 GetMovePointPosition(int index)
    {
        // 敵の移動用
        return points[index];
    }

}
