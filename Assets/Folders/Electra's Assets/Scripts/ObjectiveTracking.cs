using System.Collections;
using System.Collections.Generic;
using Hamish.Enemy;
using UnityEngine;

public class ObjectiveTracking : MonoBehaviour
{
    public int enemiesAlive = 0;
    public int enemiesKilled = 0;
    public GameObject victoryScreen;

    public void EnemyDeath()
    {
        enemiesKilled++;
        enemiesAlive--;
        if (enemiesAlive == 0)
            StartCoroutine(WinGame());
    }
    private IEnumerator WinGame()
    {
        yield return new WaitForSeconds(1);
        victoryScreen.SetActive(true);
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        UnityEngine.Cursor.visible = true;
    }
    public void UpdateEnemyCount()
    {
        enemiesAlive++;
    }
}
