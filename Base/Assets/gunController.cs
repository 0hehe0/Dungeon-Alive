using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunController : MonoBehaviour
{
    public PlayerController PC;
    public GameObject gunEffect;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (PC.isAttacking)
        {
            StartCoroutine(AttackAnim());
        }
    }

    IEnumerator AttackAnim()
    {
        gunEffect.SetActive(true);
        GameObject bulletObj = Instantiate(bullet, transform.position, transform.rotation);

        yield return new WaitForSeconds(0.4f);

        gunEffect.SetActive(false);
    }
}
