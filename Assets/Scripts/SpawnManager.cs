using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 _playerSpawnPos = new Vector3(0, 2, 0);
    private float _xSpawnBound = 10.0f;
    private float _startDelay = 1.0f;
    private float _repeatRate = 0.2f;
    private PlayerController _playerControllerScript;
    [SerializeField] private GameObject[] obstacles;
    public GameObject playerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(playerPrefab, _playerSpawnPos, transform.rotation);

        _playerControllerScript = FindAnyObjectByType<PlayerController>();

        InvokeRepeating("SpawnObstacle", _startDelay, _repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 GenerateSpawnPosition()
    {
        float xSpawnPos = Random.Range(-_xSpawnBound, _xSpawnBound);
        return new Vector3(xSpawnPos, 7, 15);
    }

    void SpawnObstacle()
    {
        if (_playerControllerScript.isGameOver == false)
        {
            Instantiate(GetRandomObstacle(), GenerateSpawnPosition(), transform.rotation);
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
