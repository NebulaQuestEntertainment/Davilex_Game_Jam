using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public Animator anim;
    public GameObject enemyBody;
    public bool dead;
    public float maxHp;
    private float Hp;

    public GameObject bullet;
    public Transform AttackPos;
    public float timeBetweenShots;
    private float nextShotsTime;

    // Start is called before the first frame update
    void Start()
    {
        Hp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > nextShotsTime)
        {
            Instantiate(bullet, AttackPos.position, Quaternion.identity);
            nextShotsTime = Time.time + timeBetweenShots;
        }

        if (Hp <= 0)
        {
            anim.SetBool("die", true);
        }
    }

    public void TakeDamage(float damage)
    {
        anim.SetTrigger("hit");
        Hp -= damage;
    }

    public void die()
    {
        Destroy(enemyBody);
    }
}
