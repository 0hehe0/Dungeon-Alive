using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 playerTransV3;
    public float speed;
    public float escapeSpeed;
    public bool isAttacking;
    public float distance;
    public float attackTimeCheck;
    public float attackCD;
    public GlobalVary GV;
    public int level = 0;
    public GameObject weapon;
    public GameObject shield;
    public float rand;

    public Vector3 shakeRate = new Vector2(.001f, .001f);
    public float shakeTime = 0.5f;
    public float shakeDertaTime = 0.1f;

    public Renderer ren;

    public int enemyState;

    public float colorChange = 2f;
    public Color colorStart = Color.white;
    public Color colorEnd = Color.red;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector2.Distance(transform.position, playerTrans.position);

        rand = Random.value;

        ren = GetComponent<Renderer>();

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
        else if (GV.enemyKillCountSword > 10 && GV.enemyKillCountSword < 30 && GV.enemyKillCountGun == 0 && distance < 10)
        {
            enemyState = 2;
            LookAt();

            ren.material.color = Color.red;

            /*
            if (colorChange >= 0f)
            {

                Color.Lerp(colorStart, Color.red, Mathf.PingPong(Time.time, 1f));

                colorChange -= Time.deltaTime;
            }
            */
        }
        else if (GV.enemyKillCountSword > 30 && GV.enemyKillCountSword < 50 && GV.enemyKillCountGun == 0 && distance < 10)
        {
            enemyState = 3;
            LookAt();
        }
        else if (GV.enemyKillCountGun > 0 && GV.enemyKillCountGun < 30)
        {
            enemyState = 4;
            ren.material.color = Color.blue;
        }
        else if (GV.enemyKillCountGun > 30)
        {
            enemyState = 5;
        }
        else if (level > 2 && GV.enemyKillCountTotal == 0)
        {
            enemyState = 0;
            LookAt();
            ren.material.color = Color.green;
        }
        else
        {
            enemyState = -1;
        }
    }

    void Attack()
    {
        if (enemyState == -1)
        {
            LookAt();
        }
        else if (enemyState == 1)
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

            if (transform.position.x > playerTransV3.x && transform.position.y > playerTransV3.y && distance < 10)
            {
                transform.position = new Vector3(transform.position.x + escapeSpeed * Time.deltaTime, transform.position.y + escapeSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.x > playerTransV3.x && transform.position.y < playerTransV3.y && distance < 10)
            {
                transform.position = new Vector3(transform.position.x + escapeSpeed * Time.deltaTime, transform.position.y - escapeSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.x < playerTransV3.x && transform.position.y > playerTransV3.y && distance < 10)
            {
                transform.position = new Vector3(transform.position.x - escapeSpeed * Time.deltaTime, transform.position.y + escapeSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.x < playerTransV3.x && transform.position.y < playerTransV3.y && distance < 10)
            {
                transform.position = new Vector3(transform.position.x - escapeSpeed * Time.deltaTime, transform.position.y - escapeSpeed * Time.deltaTime, transform.position.z);
            }
        }
        else if (enemyState == 5)
        {
            LookAt();
            weapon.SetActive(false);
            shield.SetActive(false);
            StartCoroutine(Shake_Coroutine());
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlayerWeapon")
        {
            GV.SwordCount();
            Destroy(this.gameObject);
        }
    }

    public IEnumerator Shake_Coroutine()
    {
        var oriPosition = gameObject.transform.position;
        for (float i = 0; i < shakeTime; i += shakeDertaTime)
        {
            gameObject.transform.position = oriPosition +
                Random.Range(-shakeRate.x, shakeRate.x) * Vector3.right +
                Random.Range(-shakeRate.y, shakeRate.y) * Vector3.up;
            yield return new WaitForSeconds(shakeDertaTime);
        }
        gameObject.transform.position = oriPosition;
    }
}
