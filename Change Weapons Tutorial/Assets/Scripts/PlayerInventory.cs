using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PlayerShooting playerShooting;

    public WeaponSlotUI[] weaponSlotsInUI;

    public List<WeaponInfo> allWeaponsInPossessionInfo;
    private int currentWeaponIndex;
    [HideInInspector] public WeaponInfo currentWeaponInfo;
    
    private void Awake()
    {
        //set current weapon to element 0 of weapon array
        currentWeaponInfo = allWeaponsInPossessionInfo[0];
        weaponSlotsInUI[0].weaponImage.sprite = allWeaponsInPossessionInfo[0].SOweapon.bulletPrefab.GetComponent<SpriteRenderer>().sprite;
        weaponSlotsInUI[0].highlightPanel.SetActive(true);
        currentWeaponInfo.bulletsLeft = currentWeaponInfo.SOweapon.magSize;

        weaponSlotsInUI[0].bulletsText.gameObject.SetActive(true);
        weaponSlotsInUI[0].bulletsText.text = currentWeaponInfo.bulletsLeft + "/" + currentWeaponInfo.SOweapon.magSize;
    }

    private void Update()
    {
        TrySwitchWeapon();
    }

    private void TrySwitchWeapon()
    {
        if (playerShooting.isReloading)
            return;

        //we calculate which weapon we're gonna load before we load it
        int weaponIndexToGoTo = currentWeaponIndex;
        if (Input.GetKeyDown(KeyCode.Q))
            weaponIndexToGoTo--;
        else if (Input.GetKeyDown(KeyCode.E))
            weaponIndexToGoTo++;

        //if that weapon doesn't exist, because it's outside the weapon array
        //or it's the same weapon we have now, we return
        if (weaponIndexToGoTo < 0
            || weaponIndexToGoTo == allWeaponsInPossessionInfo.Count
            || weaponIndexToGoTo == currentWeaponIndex)
            return;

        //update highlighted panel
        weaponSlotsInUI[currentWeaponIndex].highlightPanel.SetActive(false);
        weaponSlotsInUI[weaponIndexToGoTo].highlightPanel.SetActive(true);

        //switch weapon and update current weapon index
        currentWeaponInfo = allWeaponsInPossessionInfo[weaponIndexToGoTo];
        currentWeaponIndex = weaponIndexToGoTo;
    }

    public bool TryAddWeapon(SOWeapon weaponToAdd)
    {
        if (allWeaponsInPossessionInfo.Count == weaponSlotsInUI.Length)
        {
            return false;
        }
        else
        {
            WeaponInfo newWeaponInfo = new WeaponInfo();
            newWeaponInfo.SOweapon = weaponToAdd;
            newWeaponInfo.bulletsLeft = weaponToAdd.magSize;
            allWeaponsInPossessionInfo.Add(newWeaponInfo);
            weaponSlotsInUI[allWeaponsInPossessionInfo.Count - 1].weaponImage.sprite = weaponToAdd.bulletPrefab.GetComponent<SpriteRenderer>().sprite;

            weaponSlotsInUI[allWeaponsInPossessionInfo.Count - 1].bulletsText.gameObject.SetActive(true);
            weaponSlotsInUI[allWeaponsInPossessionInfo.Count - 1].bulletsText.text = newWeaponInfo.bulletsLeft + "/" + newWeaponInfo.SOweapon.magSize;
            return true;
        }
    }

    public void UseBullet()
    {
        currentWeaponInfo.bulletsLeft--;
        weaponSlotsInUI[currentWeaponIndex].bulletsText.text = currentWeaponInfo.bulletsLeft + "/" + currentWeaponInfo.SOweapon.magSize;
    }

    public void ReloadWeapon()
    {
        currentWeaponInfo.bulletsLeft = currentWeaponInfo.SOweapon.magSize;
        weaponSlotsInUI[currentWeaponIndex].bulletsText.text = currentWeaponInfo.bulletsLeft + "/" + currentWeaponInfo.SOweapon.magSize;
    }
}

[System.Serializable]
public class WeaponInfo
{
    public SOWeapon SOweapon;
    public int bulletsLeft;
}