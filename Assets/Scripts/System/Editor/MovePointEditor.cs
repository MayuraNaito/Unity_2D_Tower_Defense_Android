using UnityEngine;
using UnityEditor; // エディタの拡張機能

// https://docs.unity3d.com/ja/560/ScriptReference/Editor.html
// CustomEditor属性はどのコンポーネントのエディタとして動作させるかをUnityに知らせる。
[CustomEditor(typeof(MovePoint))]
public class MovePointEditor : Editor
{
    // 変数に格納
    MovePoint MovePoint => target as MovePoint;

    // EditorでシーンビューにHandleやGUIを出して調整作業を効率化
    [System.Obsolete]
    private void OnSceneGUI()
    {
        // 色を指定
        Handles.color = Color.green;

        for (int i = 0; i < MovePoint.points.Length; i++)
        {
            // EndChangeCheckとの間でシーン内での変化がないのかを確認
            EditorGUI.BeginChangeCheck();

            // ポイントの位置を確認
            Vector3 currentWaypoint = MovePoint.points[i];

            // ハンドルを生成して変数に格納
            // (snapはスナップ移動する際の移動量；スナップ移動はCtrl押しながら移動させるとできる)
            // 引数(生成したいポジション、回転、サイズ、ハンドルの形)
            Vector3 newWaypoint = Handles.FreeMoveHandle(currentWaypoint,
                Quaternion.identity,
                0.7f,
                new Vector3(0.3f, 0.3f, 0.3f), Handles.SphereHandleCap);

            // ハンドル番号の生成 GUIStyleはGUIの設定
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 20;
            textStyle.normal.textColor = Color.red;
            // 表示する位置の設定
            Vector3 textPos = Vector3.down * 0.35f + Vector3.right * 0.35f;

            // ラベエルの発生位置、表示内容、表示GUIの設定
            Handles.Label(MovePoint.points[i] + textPos, $"{i + 1}", textStyle);

            EditorGUI.EndChangeCheck();

            // 上位内容が変更されたらtrue
            if (EditorGUI.EndChangeCheck())
            {
                // 保存：これをしないと ctrl + z で戻すことができない
                Undo.RecordObject(target, "Move");

                // 動かしたハンドル位置の反映
                MovePoint.points[i] = newWaypoint;
            }
        }
    }

}
