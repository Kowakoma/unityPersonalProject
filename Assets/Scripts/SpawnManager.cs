using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private Vector3 _playerSpawnPos = new Vector3(0, 2, 0);
    [SerializeField] private float _xSpawnBound = 10.0f;
    [SerializeField] private float _startDelay = 1.0f;
    [SerializeField] private float _repeatRate = 0.2f;
    [SerializeField] private GameObject[] obstacles;
    private GameObject _currentFish;
    public GameObject fishPrefab;
    public Coroutine currentFishState;
    public GameObject playerPrefab;
    public bool isGameOver;
    public bool isFishing;

    void Start()
    {
        Instantiate(playerPrefab, _playerSpawnPos, transform.rotation);
        InvokeRepeating("SpawnObstacle", _startDelay, _repeatRate);
        currentFishState = StartCoroutine(FishCycle());
    }

    Vector3 GenerateSpawnPosition()
    {
        float xSpawnPos = Random.Range(-_xSpawnBound, _xSpawnBound);
        return new Vector3(xSpawnPos, 2.0f, 40.0f);
    }

    Vector3 GenerateFishSpawnPosition()
    {
        float xFishSpawnPos = Random.Range(-_xSpawnBound, _xSpawnBound);
        float zFishSpawnPos = Random.Range(-2, 10);
        return new Vector3(xFishSpawnPos, 0, zFishSpawnPos);
    }

    float GenerateRandomActiveFishTime()
    {
        return Random.Range(10, 20);
    }

    float GenerateRandomNotActiveFishTime()
    {
        return Random.Range(5, 25);
    }

    public IEnumerator FishCycle()
    {
        while (!isGameOver)
        {
            if (!isFishing)
            {
                // No fish
                if (_currentFish != null)
                {
                    Destroy(_currentFish);
                    _currentFish = null;
                }
                Debug.Log("Fish Not Active");

                yield return new WaitForSeconds(GenerateRandomNotActiveFishTime());

                // Fish active
                if (!isGameOver && !isFishing)
                {
                    SpawnFish();
                    Debug.Log("Fish Active");
                    yield return new WaitForSeconds(GenerateRandomActiveFishTime());
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public IEnumerator Fishing()
    {
        if (!isGameOver)
        {
            isFishing = true;
            Debug.Log("Fishing!!!");

            yield return new WaitForSeconds(1);

            Debug.Log("Well done!!!");
            isFishing = false;
            if (_currentFish != null)
            {
                Destroy(_currentFish);
                _currentFish = null;
            }
            if (currentFishState != null)
            {
                StopCoroutine(currentFishState);
            }
            currentFishState = StartCoroutine(FishCycle());
        }
    }

    public void StartFishing()
    {
        currentFishState = StartCoroutine(Fishing());
    }

    public void BreakFishing()
    {
        if (_currentFish != null)
        {
            Destroy(_currentFish);
            _currentFish = null;
        }
        if (currentFishState != null)
        {
            StopCoroutine(currentFishState);
        }
        Debug.Log("Fish got away!");
        isFishing = false;
        currentFishState = StartCoroutine(FishCycle());
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
            _currentFish = Instantiate(fishPrefab, GenerateFishSpawnPosition(), transform.rotation);
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
