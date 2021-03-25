using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public SOWeapon[] weaponsInPossesion;
    private int currentWeaponIndex;

    private SOWeapon currentWeapon;
    public float attackTimer;

    private void Awake()
    {
        currentWeapon = weaponsInPossesion[0];
    }

    private void Update()
    {
        //take input and try switch weapon
        TrySwitchWeapon();

        //decrease attack timer and get input to shoot
        TryShoot();
    }

    private void TrySwitchWeapon()
    {
        //take input
        int newWeaponIndex = currentWeaponIndex;
        if (Input.GetKeyDown(KeyCode.E))
            newWeaponIndex++;
        else if (Input.GetKeyDown(KeyCode.Q))
            newWeaponIndex--;

        if (newWeaponIndex == currentWeaponIndex || newWeaponIndex < 0 || newWeaponIndex == weaponsInPossesion.Length)
            return;

        //switch weapon
        currentWeapon = weaponsInPossesion[newWeaponIndex];
        currentWeaponIndex = newWeaponIndex;
        attackTimer = currentWeapon.attackTimer;
    }

    private void TryShoot()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        //take player inputs
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (attackTimer <= 0)
            {
                attackTimer = currentWeapon.attackTimer;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        //shoot currentweapon's bullet
        GameObject spawnedBullet = Instantiate(currentWeapon.bulletPrefab, transform.position, Quaternion.identity);
        spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(9, 0);
        spawnedBullet.GetComponent<Bullet>().bulletDamage = currentWeapon.attackDamage;
    }
}