using UnityEngine;

public class WeaponShopManager : MonoBehaviour
{
    // UIプレファブ
    [SerializeField] private GameObject turretCardPrefab;
    // 生成したUIを格納する親オブジェクト
    [SerializeField] private Transform turretPanelContainer;
    // すクリプタブルオブジェクト格納
    [SerializeField] private WeaponSettings[] weapons;


    // Start
    void Start()
    {
        // 武器の数分ループ
        for (int i = 0; i < weapons.Length; i++)
        {
            // UI生成する関数
            CreateWeaponUI(weapons[i]);
        }
    }


    // 武器を生成するボタンUIを作成する関数
    private void CreateWeaponUI(WeaponSettings weaponSettings)
    {
        // インスタンス生成して格納
        GameObject newUI = Instantiate(turretCardPrefab,
            turretPanelContainer.position, Quaternion.identity);
        // 親や大きさを設定
        newUI.transform.SetParent(turretPanelContainer);
        newUI.transform.localScale = Vector3.one;

        // コストや絵をUIに反映
        WeaponUI weaponButton = newUI.GetComponent<WeaponUI>();
        weaponButton.SetupUI(weaponSettings);
    }

}
