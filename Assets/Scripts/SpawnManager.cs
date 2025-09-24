using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Vector3 _playerSpawnPos = new Vector3(0, 2, 0);
    [SerializeField] private float _xSpawnBound = 10.0f;
    [SerializeField] private float _zSpawnBound = 10.0f;
    [SerializeField] private float _startDelay = 1.0f;
    [SerializeField] private float _repeatRate = 0.2f;
    [SerializeField] private float _ySpawnObstaclePos = 2.0f;
    [SerializeField] private float _zSpawnObstaclePos = 40.0f;
    [SerializeField] private GameObject[] obstacles;
    public GameObject playerPrefab;
    public bool isGameOver;

    void Start()
    {
        Instantiate(playerPrefab, _playerSpawnPos, transform.rotation);
        InvokeRepeating("SpawnObstacle", _startDelay, _repeatRate);
        StartCoroutine(FishNotActive());
        Debug.Log("Fish Not Active");
    }

    Vector3 GenerateSpawnPosition()
    {
        float xSpawnPos = Random.Range(-_xSpawnBound, _xSpawnBound);
        return new Vector3(xSpawnPos, _ySpawnObstaclePos, _zSpawnObstaclePos);
    }

    Vector3 GenerateFishSpawnPosition()
    {
        float xFishSpawnPos = Random.Range(-_xSpawnBound, _xSpawnBound);
        float zFishSpawnPos = Random.Range(-_zSpawnBound, _zSpawnBound);
        return new Vector3(xFishSpawnPos, 0, zFishSpawnPos);
    }

    float GenerateRandomActiveFishTime()
    {
        return Random.Range(3, 10);
    }

    float GenerateRandomNotActiveFishTime()
    {
        return Random.Range(5, 30);
    }

    IEnumerator FishActive()
    {
        yield return new WaitForSeconds(GenerateRandomActiveFishTime());

        StartCoroutine(FishNotActive());
        Debug.Log("Fish Not Active");
    }

    IEnumerator FishNotActive()
    {
        yield return new WaitForSeconds(GenerateRandomNotActiveFishTime());

        StartCoroutine(FishActive());
        Debug.Log("Fish Active");
    }

    void SpawnObstacle()
    {
        if (isGameOver == false)
        {
            Instantiate(GetRandomObstacle(), GenerateSpawnPosition(), transform.rotation);
        }
    }

    void SpawnFish()
    {
        if (isGameOver == false)
        {
            //
        }
    }

    public GameObject GetRandomObstacle()
    {
        if (obstacles == null || obstacles.Length == 0)
        {
            Debug.LogError("No prefabs assigned!");
            return null;
        }

        int randomIndex = Random.Range(0, obstacles.Length);
        return obstacles[randomIndex];
    }
}
