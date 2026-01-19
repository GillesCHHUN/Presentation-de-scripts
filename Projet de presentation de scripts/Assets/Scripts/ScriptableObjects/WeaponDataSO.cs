using UnityEngine;


[CreateAssetMenu(fileName = "WeaponName", menuName = "WeaponSO")]
public class WeaponDataSO : ScriptableObject
{
    public float bulletSpeed;
    public float bulletPerSec;
    public GameObject prefab;
    
}
