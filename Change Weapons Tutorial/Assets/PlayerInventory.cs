using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponSlotUI[] weaponSlotsInUI;

    public List<SOWeapon> allWeaponsInPossession;
    private int currentWeaponIndex;

    [HideInInspector] public SOWeapon currentWeapon;
    
    private void Awake()
    {
        //set current weapon to element 0 of weapon array
        currentWeapon = allWeaponsInPossession[0];
        weaponSlotsInUI[0].weaponImage.sprite = allWeaponsInPossession[0].bulletPrefab.GetComponent<SpriteRenderer>().sprite;
        weaponSlotsInUI[0].highlightPanel.SetActive(true);
    }

    private void Update()
    {
        TrySwitchWeapon();
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
            || weaponIndexToGoTo == allWeaponsInPossession.Count
            || weaponIndexToGoTo == currentWeaponIndex)
            return;

        //update highlighted panel
        weaponSlotsInUI[currentWeaponIndex].highlightPanel.SetActive(false);
        weaponSlotsInUI[weaponIndexToGoTo].highlightPanel.SetActive(true);

        //switch weapon and update current weapon index
        currentWeapon = allWeaponsInPossession[weaponIndexToGoTo];
        currentWeaponIndex = weaponIndexToGoTo;
    }

    public void AddWeapon(SOWeapon weaponToAdd)
    {
        allWeaponsInPossession.Add(weaponToAdd);
        weaponSlotsInUI[allWeaponsInPossession.Count - 1].weaponImage.sprite = weaponToAdd.bulletPrefab.GetComponent<SpriteRenderer>().sprite;
    }
}