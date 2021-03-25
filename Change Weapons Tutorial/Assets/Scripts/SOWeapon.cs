using UnityEngine;

[CreateAssetMenu(fileName = "New weapon", menuName = "Create Scriptable/Weapon")]
public class SOWeapon : ScriptableObject
{
    public GameObject bulletPrefab;
    public float attackTimer = 1;
    public int attackDamage = 1;
}