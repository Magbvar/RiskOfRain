using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{    
    public int EnemyActive;
   

    public int stage;
    public GameObject[] Enemy;
    public Transform Player;
   
    // Start is called before the first frame update
    public void Start()
    {        
        stage = 0;
        EnemyActive = 0;
        
    }

    // Update is called once per frame
    public void Update()
    {
        if (EnemyActive <= 0)
        {
            
            EnemySpawner();
        }   
    }

    public void EnemyUpdater(int update)
    {
        EnemyActive -= update;
        
    }

    void EnemySpawner()
    {
        stage++;
        
        for (int i = 0; i < stage * 4; i++)
        {
            Vector3 randomPosition;
            do
            {
                float randomx = Random.Range(-45, 45);
                float randomy = Random.Range(0, 20);
                float randomz = Random.Range(-45, 45);
                randomPosition = new Vector3(randomx, randomy, randomz);
            }

            while (Vector3.Distance(randomPosition, Player.position) < 10f);

            GameObject randomEnemy = Enemy[Random.Range(0, Enemy.Length)];

            Instantiate(randomEnemy, randomPosition, Quaternion.identity);    
            EnemyActive++;


            
        }
    }

    public void Restart()
    {  
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
