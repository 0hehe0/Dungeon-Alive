using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliticianController : MonoBehaviour
{
    public Transform playerTrans;
    public Vector3 playerTransV3;

    public float speed;
    public float escapeSpeed;

    public float distance;

    public bool isAttacking;
    public float attackTimeCheck;
    public float attackCD;

    public GlobalVary GV;
    public GameObject weapon;
    public GameObject shield;
    public Renderer ren;

    public int level = 0;

    public int enemyState;

    // Start is called before the first frame update
    void Start()
    {
        distance = Vector2.Distance(transform.position, playerTrans.position);

        ren = GetComponent<Renderer>();

        ren.material.color = Color.blue;

        weapon.SetActive(false);
        shield.SetActive(false);

        escapeSpeed = 4;
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

    // 0 -> Friendly
    // 1 -> Fight
    // 2 -> Escape
    // 3 -> Frightened
    void State()
    {
        if (GV.enemyKillCountTotal == 0)
        {
            enemyState = 0;
            LookAt();
        }
        else if (GV.politicianKillCount > 0 && GV.enemyKillCountGun == 0)
        {
            ren.material.color = Color.red;

            weapon.SetActive(true);
            shield.SetActive(true);

            if (distance < 15)
            {
                enemyState = 1;
                LookAt();
            }
        }
        else if (GV.enemyKillCountGun > 0 && GV.enemyKillCountGun < 3)
        {
            enemyState = 2;
            ren.material.color = Color.blue;
        }
        else if (GV.enemyKillCountGun >= 3)
        {
            enemyState = 3;
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
        else if (enemyState == 2)
        {
            Vector3 dir = transform.position - playerTransV3;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            if (transform.position.x > playerTransV3.x && transform.position.y > playerTransV3.y && distance < 13)
            {
                transform.position = new Vector3(transform.position.x + escapeSpeed * Time.deltaTime, transform.position.y + escapeSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.x > playerTransV3.x && transform.position.y < playerTransV3.y && distance < 13)
            {
                transform.position = new Vector3(transform.position.x + escapeSpeed * Time.deltaTime, transform.position.y - escapeSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.x < playerTransV3.x && transform.position.y > playerTransV3.y && distance < 13)
            {
                transform.position = new Vector3(transform.position.x - escapeSpeed * Time.deltaTime, transform.position.y + escapeSpeed * Time.deltaTime, transform.position.z);
            }
            else if (transform.position.x < playerTransV3.x && transform.position.y < playerTransV3.y && distance < 13)
            {
                transform.position = new Vector3(transform.position.x - escapeSpeed * Time.deltaTime, transform.position.y - escapeSpeed * Time.deltaTime, transform.position.z);
            }
        }
        else if (enemyState == 3)
        {
            LookAt();
            weapon.SetActive(false);
            shield.SetActive(false);
            StartCoroutine(Shake(1f, .03f));
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
            GV.PoliticianCount();
            GV.SwordCount();
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Bullet")
        {
            GV.PoliticianCount();
            GV.GunCount();
            Destroy(this.gameObject);
        }
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x + originalPos.x, y + originalPos.y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPos;

    }
}
