using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Vector3 _playerSpawnPos = new Vector3(0, 2, 0);
    private float _xSpawnBound = 22.0f;
    private float _startDelay = 1.0f;
    private float _repeatRate = 0.2f;
    private PlayerController _playerControllerScript;
    public GameObject redCubePrefab;
    public GameObject playerPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(playerPrefab, _playerSpawnPos, transform.rotation);

        _playerControllerScript = FindAnyObjectByType<PlayerController>();

        InvokeRepeating("SpawnRedCube", _startDelay, _repeatRate);
    }

    // Update is called once per frame
    void Update()
    {

    }

    Vector3 GenerateSpawnPosition()
    {
        float xSpawnPos = Random.Range(-_xSpawnBound, _xSpawnBound);
        return new Vector3(xSpawnPos, 2, 15);
    }

    void SpawnRedCube()
    {
        if (_playerControllerScript.isGameOver == false)
        {
            Instantiate(redCubePrefab, GenerateSpawnPosition(), transform.rotation);
        }
    }
}
