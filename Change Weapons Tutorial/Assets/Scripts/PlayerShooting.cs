using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public SOWeapon[] allWeaponsInPossession;
    private int currentWeaponIndex;

    private SOWeapon currentWeapon;
    public float attackTimer;

    private void Awake()
    {
        //set current weapon to element 0 of weapon array
        currentWeapon = allWeaponsInPossession[0];
    }

    private void Update()
    {
        TrySwitchWeapon();
        TryShoot();
    }

    private void TrySwitchWeapon()
    {
        //we calculate which weapon we're gonna load before we load it
        int weaponIndexToGoTo = currentWeaponIndex;
        if (Input.GetKeyDown(KeyCode.Q))
            weaponIndexToGoTo--;
        else if (Input.GetKeyDown(KeyCode.E))
            weaponIndexToGoTo++;

        //if that weapon doesn't exist, because it's outside the weapon array
        //or it's the same weapon we have now, we return
        if (weaponIndexToGoTo < 0 
            || weaponIndexToGoTo == allWeaponsInPossession.Length 
            || weaponIndexToGoTo == currentWeaponIndex)
            return;

        //switch weapon and update current weapon index
        currentWeapon = allWeaponsInPossession[weaponIndexToGoTo];
        currentWeaponIndex = weaponIndexToGoTo;
    }

    private void TryShoot()
    {
        if (attackTimer > 0)
            attackTimer -= Time.deltaTime;

        //if cooldown is ready, take player input and shoot
        if (attackTimer <= 0 && Input.GetKeyDown(KeyCode.L))
        {
            //reset the attack timer based on our current weapon's info
            attackTimer = currentWeapon.attackTimer;
            Shoot();
        }
    }

    private void Shoot()
    {
        //fire current weapon's bullet
        GameObject spawnedBullet = Instantiate(currentWeapon.bulletPrefab, transform.position, Quaternion.identity);
        //give it a velocity to the right, with the speed based on the current weapon
        spawnedBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(currentWeapon.bulletSpeed, 0);
        //set the bullet's script damage value based on our current weapon
        spawnedBullet.GetComponent<Bullet>().attackDamage = currentWeapon.attackDamage;
    }
}