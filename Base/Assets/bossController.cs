using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 playerTransV3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTransV3 = new Vector3(playerTrans.position.x, playerTrans.position.y, playerTrans.position.z);

        Vector3 dir = playerTransV3 - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90f, Vector3.forward);
    }
}
