using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float Hmovement;
    public float Vmovement;
    public bool isAttacking;
    public GlobalVary GV;
    public portal pt;
    private bool move = true;

    public GameObject B1;
    public GameObject B2;

    public GameObject sensor;

    public GameObject End1;
    public GameObject End2;
    public GameObject End3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Attack();
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        RoomX();
    }

    void Movement()
    {
        Hmovement = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        Vmovement = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + Hmovement, transform.position.y + Vmovement, transform.position.z);
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isAttacking = true;
        }
        else
        {
            isAttacking = false;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyWeapon")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "GunDrop")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "B1")
        {
            B1.SetActive(true);
        }

        if (collision.gameObject.tag == "B2")
        {
            B2.SetActive(true);
        }
        if (collision.gameObject.tag == "E1")
        {
            Time.timeScale = 0;
            if (GV.enemyKillCountGun >= 1)
            {
                End1.SetActive(true);
            }
            else if (GV.enemyKillCountTotal >= 1 && GV.enemyKillCountSword >= 1)
            {
                End2.SetActive(true);
            }
            else if (GV.enemyKillCountTotal == 0)
            {
                End3.SetActive(true);
            }
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "B1")
        {
            B1.SetActive(false);
        }

        if (collision.gameObject.tag == "B2")
        {
            B2.SetActive(false);
        }

  
    }

    void RoomX()
    {
        if (GV.enemyKillCountSword > 5)
        {
            if (move)
            {
                pt.goToPos = transform.position;
                transform.position = new Vector3(-142.5f, -21f, 0);
                move = false;
            }
        }
    }
}
