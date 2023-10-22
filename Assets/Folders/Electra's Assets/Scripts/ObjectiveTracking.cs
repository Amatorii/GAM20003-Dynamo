using System.Collections;
using System.Collections.Generic;
using Hamish.Enemy;
using UnityEngine;

public class ObjectiveTracking : MonoBehaviour
{
    public int enemiesAlive = 0;
    public int enemiesKilled = 0;
    public GameObject endScreen;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyDeath()
    {
        enemiesKilled++;
        enemiesAlive--;
        if (enemiesAlive == 0)
            endScreen.SetActive(true);
    }
    public void UpdateEnemyCount()
    {
        enemiesAlive++;
    }
}
