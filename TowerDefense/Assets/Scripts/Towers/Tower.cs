using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Projectile projectile;
    [SerializeField] private float timeBetweenAttacks;
    [SerializeField] private float attackRadius;
    [SerializeField]
    private Enemy targetEnemy = null;
    [SerializeField]
    private float attackCounter;
    [SerializeField]
    private bool isAttacking=false;

    void Start()
    {
        
    }

    void Update()
    {
        attackCounter -= Time.deltaTime;
        if (targetEnemy == null || targetEnemy.IsDead)
        {
            Enemy newEnemy = GetNearestEnemyInRange();
            if (newEnemy != null && Vector2.Distance(transform.localPosition, newEnemy.transform.localPosition) <= attackRadius)
            {
                targetEnemy = newEnemy;
            }

        }
        else
        {
            if (attackCounter <= 0)
            {
                
                attackCounter = timeBetweenAttacks;
                isAttacking = true;
            }
            

            if (Vector2.Distance(transform.localPosition, targetEnemy.transform.localPosition) > attackRadius)
                targetEnemy = null;
        }

       
    }

    void FixedUpdate()
    {
        if (isAttacking == true)
        {
            Attack();  
        }
    }

    public void Attack()
    {
        isAttacking = false;
        Projectile newProjectile = Instantiate(projectile) as Projectile;
        newProjectile.transform.localPosition = transform.localPosition;
        if (targetEnemy == null)
        {
            Destroy(newProjectile);
        }
        else
        {
            StartCoroutine(MoveProjectile(newProjectile));
        }
    }

    IEnumerator MoveProjectile(Projectile projectile)
    {
        while(GetTargetDistance(targetEnemy) > 0.2f && projectile!=null && targetEnemy!=null)
        {
            var dir= targetEnemy.transform.localPosition-transform.localPosition ;
            var angleDirection=Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);
            projectile.transform.localPosition = Vector2.MoveTowards(projectile.transform.localPosition, targetEnemy.transform.localPosition, projectile.ProjectileSpeed * Time.deltaTime);
            yield return null;
        }

        if (projectile != null || targetEnemy==null)
            Destroy(projectile);
    }


    private float GetTargetDistance(Enemy thisEnemy)
    {
        if(thisEnemy==null)
        {
            thisEnemy= GetNearestEnemyInRange();
            if (thisEnemy == null)
                return 0f;
        }

        return Mathf.Abs(Vector2.Distance(transform.localPosition,thisEnemy.transform.localPosition));
    }
    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange=new List<Enemy>();
        foreach(Enemy enemy in GameManager.Instance.EnemyList)
        {
            if(Vector2.Distance(transform.localPosition, enemy.transform.localPosition) <= attackRadius)
                enemiesInRange.Add(enemy);
        }
        return enemiesInRange;
    }

    private Enemy GetNearestEnemyInRange()
    {
        Enemy nearestEnemy = null;
        float smallestDistance = float.PositiveInfinity;
        foreach(Enemy enemy in GetEnemiesInRange())
        {
            if (Vector2.Distance(transform.localPosition, enemy.transform.localPosition) < smallestDistance)
            {
                smallestDistance = Vector2.Distance(transform.localPosition, enemy.transform.localPosition);
                nearestEnemy = enemy;
            }    
        }
        return nearestEnemy;

    }
}
