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
        playerTransV3 = new Vector3 (playerTrans.position.x, playerTrans.position.y, playerTrans.position.z);
        Movement();
        Attack();
        Vector3 dir = playerTransV3 - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Movement()
    {


        Hmovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        Vmovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Hmovement, transform.position.y + Vmovement, transform.position.z);
    }

    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }
    }
}
