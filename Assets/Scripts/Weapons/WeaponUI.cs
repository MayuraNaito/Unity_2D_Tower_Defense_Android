using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class WeaponUI : MonoBehaviour
{
    // 表示する絵を格納
    [SerializeField] private Image weaponImage;
    // コストを表示するテキストを格納
    [SerializeField] private TextMeshProUGUI weaponCost;
    // 表示している武器の情報を格納
    private WeaponSettings weaponSettings;


    // カードに絵とコストテキストを設定する関数
    public void SetupUI(WeaponSettings weapon)
    {
        // すクリプタブルオブジェクトを格納
        weaponSettings = weapon;
        // 絵とコストテキストを設定
        weaponImage.sprite = weaponSettings.TurretSprite;
        weaponCost.text = weaponSettings.TurretShopCost.ToString();
    }

}
