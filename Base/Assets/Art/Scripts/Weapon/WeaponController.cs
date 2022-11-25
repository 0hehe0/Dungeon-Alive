using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject currentWeapon;
    public GameObject[] weaponList;
    public int weaponNum;

    public GameObject currentShield;
    public GameObject[] shieldList;
    public int shieldNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentWeapon = weaponList[weaponNum];
        currentShield = shieldList[shieldNum];
        SwitchWeapon();
        SwitchShield();
    }

    void SwitchWeapon()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponNum++;
            if (weaponNum > 1)
            {
                weaponNum = 0;
            }
            ShowWeapon();
        }
    }
    void SwitchShield()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            shieldNum++;
            if (shieldNum > 1)
            {
                shieldNum = 0;
            }
            ShowShield();
        }
    }

    void ShowWeapon()
    {
        for (int i = 0; i < weaponList.Length; i++)
        {
            if (i == weaponNum)
                weaponList[i].gameObject.SetActive(true);
            else
                weaponList[i].gameObject.SetActive(false);
        }
    }

    void ShowShield()
    {
        for (int i = 0; i < shieldList.Length; i++)
        {
            if (i == shieldNum)
                shieldList[i].gameObject.SetActive(true);
            else
                shieldList[i].gameObject.SetActive(false);
        }
    }

}
