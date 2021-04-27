using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public float attackTimer;
    public bool isReloading;
    public float reloadTime;

    private void Update()
    {
        TryReload();
        TryShoot();
    }

    private void TryReload()
    {
        if (isReloading)
        {
            reloadTime -= Time.deltaTime;
            if (reloadTime <= 0)
            {
                isReloading = false;
                playerInventory.ReloadWeapon();
            }
        }
    }

    private void TryShoot()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        //if cooldown is ready, take player input and shoot
        if (!isReloading && attackTimer <= 0 && Input.GetKeyDown(KeyCode.L))
        {
            //reset the attack timer based on our current weapon's info
            attackTimer = playerInventory.currentWeaponInfo.SOweapon.attackTimer;
            Shoot();
        }
    }

    private void Shoot()
    {
        //fire current weapon's bullet
        GameObject spawnedBullet = Instantiate(playerInventory.currentWeaponInfo.SOweapon.bulletPrefab, transform.position, Quaternion.identity);
        //give it a velocity to the right, with the speed based on the current weapon
        spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(playerInventory.currentWeaponInfo.SOweapon.bulletSpeed, 0);
        //set the bullet's script damage value based on our current weapon
        spawnedBullet.GetComponent<Bullet>().attackDamage = playerInventory.currentWeaponInfo.SOweapon.attackDamage;

        playerInventory.UseBullet();
        if (playerInventory.currentWeaponInfo.bulletsLeft == 0)
        {
            isReloading = true;
            reloadTime = playerInventory.currentWeaponInfo.SOweapon.reloadTime;
        }
    }
}