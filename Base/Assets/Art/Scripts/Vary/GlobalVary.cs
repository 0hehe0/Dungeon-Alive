using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVary : MonoBehaviour
{

    
    public int enemyKillCountGun;
    public int enemyKillCountSword;
    public int enemyKillCountTotal; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyKillCountTotal = enemyKillCountGun + enemyKillCountSword;
    }
}
