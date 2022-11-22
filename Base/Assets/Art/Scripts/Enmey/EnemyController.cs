using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 playerTransV3;
    public float speed;
    public float Hmovement;
    public float Vmovement;
    public bool isAttacking;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerTransV3 = new Vector3 (transform.position.x, transform.position.y, playerTrans.position.z);
        Movement();
        Attack();
        transform.LookAt(playerTransV3);
    }

    void Movement()
    {


        Hmovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        Vmovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Hmovement, transform.position.y + Vmovement, transform.position.z);
    }

    void Attack()
    {

    }
}
