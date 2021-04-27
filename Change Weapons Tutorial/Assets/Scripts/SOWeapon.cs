using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Create Scriptable/Weapon")]
public class SOWeapon : ScriptableObject
{
    public GameObject bulletPrefab;
    public float attackTimer = 1;
    public int attackDamage = 1;
    public float bulletSpeed = 9;

    public int magSize = 3;
    public float reloadTime = 2;
}