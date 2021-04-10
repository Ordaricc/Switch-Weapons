using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public SOWeapon weaponToAddToPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerInventory>().AddWeapon(weaponToAddToPlayer);
            Destroy(gameObject);
        }
    }
}