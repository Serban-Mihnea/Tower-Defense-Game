using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
    [SerializeField]
    private GameObject spawnPoint;
    [SerializeField]
    private GameObject[] enemies;
    [SerializeField]
    private int maxEnemiesOnScreen;
    [SerializeField]
    private int totalEnemies;
    [SerializeField]
    private int enemiesPerSpawn;
     
    private float waitTime = 0.5f;


    public List<Enemy> EnemyList=new List<Enemy>();

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }   

    IEnumerator SpawnEnemies()
    {
        if (EnemyList.Count < totalEnemies && enemiesPerSpawn > 0)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                int rnd=Random.Range(0,enemies.Length);
                if (EnemyList.Count < maxEnemiesOnScreen)
                {
                    GameObject newEnemy = Instantiate(enemies[rnd]) as GameObject;
                    newEnemy.transform.position = spawnPoint.transform.position;
                }
            }
            yield return new WaitForSeconds(waitTime);
            StartCoroutine(SpawnEnemies());
        }
       
    }

    public void RegisterEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }

    public void UnregisterEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    public void DestroyAllEnemies()
    {       
        foreach(Enemy enemy in EnemyList)
        {
            Destroy(enemy.gameObject);
        }
        EnemyList.Clear();
    }
    


}
