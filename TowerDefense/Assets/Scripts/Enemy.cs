using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform myPos;
    
    private SpriteRenderer mySpr;
    private Collider2D enemyCd;
    private Animator anim;

    [SerializeField]
    private Transform[] wayPoints;
    [SerializeField]
    private int target = 0;
    [SerializeField]
    private Transform exit;
    [SerializeField]
    private float navigationUpdate;
    [SerializeField]
    private float enemyHealth;
    [SerializeField]
    private float enemySpeed;
    private bool isDead=false;
    private float navigationTime = 0;

    public bool IsDead {  get { return isDead; } }

    void Start()
    {
        myPos=GetComponent<Transform>();
        mySpr= GetComponent<SpriteRenderer>();
        GameManager.Instance.RegisterEnemy(this); 
        enemyCd = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(wayPoints!=null && !isDead)
        {
            navigationTime += Time.deltaTime;
            if (navigationTime > navigationUpdate)
            {
                if (target < wayPoints.Length)
                {
                    myPos.position = Vector2.MoveTowards(myPos.position, wayPoints[target].position, navigationTime * enemySpeed);
                }
                else
                {
                    myPos.position = Vector2.MoveTowards(myPos.position, exit.position, navigationTime * enemySpeed);
                }
                navigationTime = 0;
            }
            
        }
        

    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Checkpoint")
        {
            target++;
            
        }else if(other.gameObject.tag=="FlipCheckpoint")
        {
            mySpr.flipX = !mySpr.flipX;
            target++;
        }else if(other.gameObject.tag=="Finish")
        {
            GameManager.Instance.UnregisterEnemy(this);
        }else if (other.gameObject.tag == "Projectile")
        {
            Projectile newP=other.gameObject.GetComponent<Projectile>();
            EnemyHit(newP.AttackDamage);
            Destroy(other.gameObject);
        }
    }
    

    public void EnemyHit(int hitPoints)
    {
        if (enemyHealth - hitPoints > 0)
        {
            anim.Play("Hurt");
            enemyHealth -= hitPoints;

        }
            
        else
        {
            anim.SetTrigger("didDie");
            Die();
        }
    }

    public void Die()
    {
        isDead = true;
        enemyCd.enabled = false;
    }
}
