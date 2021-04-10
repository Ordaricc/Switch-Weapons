using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public float attackTimer;
    
    private void Update()
    {
        TryShoot();
    }

    private void TryShoot()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        //if cooldown is ready, take player input and shoot
        if (attackTimer <= 0 && Input.GetKeyDown(KeyCode.L))
        {
            //reset the attack timer based on our current weapon's info
            attackTimer = playerInventory.currentWeapon.attackTimer;
            Shoot();
        }
    }

    private void Shoot()
    {
        //fire current weapon's bullet
        GameObject spawnedBullet = Instantiate(playerInventory.currentWeapon.bulletPrefab, transform.position, Quaternion.identity);
        //give it a velocity to the right, with the speed based on the current weapon
        spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerInventory.currentWeapon.bulletSpeed, 0);
        //set the bullet's script damage value based on our current weapon
        spawnedBullet.GetComponent<Bullet>().attackDamage = playerInventory.currentWeapon.attackDamage;
    }
}