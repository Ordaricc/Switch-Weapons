using UnityEngine;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public int bulletDamage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().health -= bulletDamage;
            Destroy(gameObject);
        }
    }
}