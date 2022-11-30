using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVary : MonoBehaviour
{
    public int enemyKillCountGun = 0;
    public int enemyKillCountSword = 0;
    public int enemyKillCountTotal = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyKillCountTotal = enemyKillCountGun + enemyKillCountSword;
    }

    public void SwordCount()
    {
        enemyKillCountSword++;
    }

    public void GunCount()
    {
        enemyKillCountGun++;
    }
}
