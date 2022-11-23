using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] buffs;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        StartCoroutine(EnemySpawnCooldown());
        StartCoroutine(BuffsSpawnCooldown());
    }

    // Update is called once per frame
    public void StartSpawnRoutine()
    {
        StartCoroutine(EnemySpawnCooldown());
        StartCoroutine(BuffsSpawnCooldown());
    }

    IEnumerator EnemySpawnCooldown()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-8.18f, 8.18f), 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.5f);
        }
    }

    IEnumerator BuffsSpawnCooldown()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(buffs[Random.Range(0, 3)], new Vector3(Random.Range(-8.18f, 8.18f), 6.0f, 0), Quaternion.identity);
            yield return new WaitForSeconds(15.0f);
        }
    }
}
