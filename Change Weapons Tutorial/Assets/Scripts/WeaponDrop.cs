using UnityEngine;

public class WeaponDrop : MonoBehaviour
{
    public SOWeapon weaponToAddToPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.GetComponent<PlayerInventory>().TryAddWeapon(weaponToAddToPlayer))
                Destroy(gameObject);
        }
    }
}