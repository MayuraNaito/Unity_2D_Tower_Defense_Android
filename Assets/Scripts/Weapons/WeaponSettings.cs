using UnityEngine;

[CreateAssetMenu(menuName = "Weapon")]
public class WeaponSettings : ScriptableObject
{
    // 武器のプレファブ
    public GameObject TurretPrefab;
    // コスト
    public int TurretShopCost;
    // UI表示用の絵
    public Sprite TurretSprite;



}
