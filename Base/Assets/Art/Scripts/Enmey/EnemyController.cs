using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 playerTransV3;
    public float speed;
    public bool isAttacking;
    public float distance;
    public float attackTimeCheck;
    public float attackCD;
    public GlobalVary GV;
    public int level = 0;
    public GameObject weapon;
    public float rand;

    public int enemyState;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector2.Distance(transform.position, playerTrans.position);

        rand = Random.value;
    }

    // Update is called once per frame
    void Update()
    {
        playerTransV3 = new Vector3(playerTrans.position.x, playerTrans.position.y, playerTrans.position.z);
        distance = Vector2.Distance(transform.position, playerTrans.position);

        State();
        Attack();
    }

    void LookAt()
    {
        Vector3 dir = playerTransV3 - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void State()
    {
        if (GV.enemyKillCountTotal == 0 && level <= 2)
        {
            enemyState = 1;
            LookAt();
        }
        else if (GV.enemyKillCountSword > 10 && GV.enemyKillCountSword < 30 && GV.enemyKillCountGun == 0)
        {
            enemyState = 2;
            LookAt();
        }
        else if (GV.enemyKillCountSword > 30 && GV.enemyKillCountSword < 50 && GV.enemyKillCountGun == 0)
        {
            enemyState = 3;
            LookAt();
        }
        else if (GV.enemyKillCountGun > 0 && GV.enemyKillCountGun < 30)
        {
            enemyState = 4;
        }
        else if (GV.enemyKillCountGun > 30)
        {
            enemyState = 5;
        }
        else if (level > 2 && GV.enemyKillCountTotal == 0)
        {
            enemyState = 0;
            LookAt();
        }
    }

    void Attack()
    {
        if (enemyState == 1)
        {
            attackTimeCheck -= Time.deltaTime;

            if (distance < 2f)
            {
                if (attackTimeCheck <= 0f)
                {
                    isAttacking = true;
                    attackTimeCheck = attackCD;
                }
                else
                {
                    isAttacking = false;
                }
            }
        }
        else if (enemyState == 2)
        {
            if (rand >= .5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerTrans.position, speed * Time.deltaTime);
            }

            attackTimeCheck -= Time.deltaTime;

            if (distance < 2f)
            {
                speed = 0;

                if (attackTimeCheck <= 0f)
                {
                    isAttacking = true;
                    attackTimeCheck = attackCD;
                }
                else
                {
                    isAttacking = false;
                }
            }
            else
            {
                speed = 3;
            }

        }
        else if (enemyState == 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTrans.position, speed * Time.deltaTime);
            attackTimeCheck -= Time.deltaTime;

            if (distance < 2f)
            {
                speed = 0;

                if (attackTimeCheck <= 0f)
                {
                    isAttacking = true;
                    attackTimeCheck = attackCD;
                }
                else
                {
                    isAttacking = false;
                }
            }
            else
            {
                speed = 3;
            }

        }
        else if (enemyState == 4)
        {
            Vector3 dir = transform.position - playerTransV3;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        }
        else if (enemyState == 5)
        {

        }
        else if (enemyState == 0)
        {
            weapon.SetActive(false);
        }
        else
        {
            isAttacking = false;
        }
    }
}
