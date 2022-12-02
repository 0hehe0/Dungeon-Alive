using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class portal : MonoBehaviour
{

    public Transform goToPos;

    private Transform playerPos;

    public Vector3 playerPosV3;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosV3 = playerPos.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerBody")
        {
            playerPos.transform.position = new Vector3(2, 4, 0);
        }
    }
}
