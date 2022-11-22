using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    public PlayerController PC;
    public EnemyController EC;
    public Animator swordEffect;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (PC != null)
        {
            if (PC.isAttacking)
            {
                StartCoroutine(AttackAnim());
            }
        }
        
        if (EC != null)
        {
            if (EC.isAttacking)
            {
                StartCoroutine(AttackAnim());
            }
        }
       
    }

    IEnumerator AttackAnim()
    {
        swordEffect.SetInteger("isAtking", 1);
        yield return new WaitForSeconds(0.4f);
        swordEffect.SetInteger("isAtking", 0);
    }
}
