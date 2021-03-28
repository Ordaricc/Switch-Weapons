using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int attackDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if bullet enters an enemy
        if (collision.CompareTag("Enemy"))
        {
            //deal damage to enemy
            collision.GetComponent<Enemy>().health -= attackDamage;
            Destroy(gameObject);
        }
    }
}