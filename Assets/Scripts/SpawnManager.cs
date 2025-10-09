using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    [Header("Player spawn position")]
    [SerializeField] private Vector3 _playerSpawnPos = new Vector3(0, 2, 0);
    public GameObject playerPrefab;

    [Header("Obstacle spawn settings")]
    [SerializeField] private float _xSpawnBound = 10.0f; 
    [SerializeField] private float _startDelay = 1.0f;
    [SerializeField] private float _repeatRate = 0.2f;
    [SerializeField] private GameObject[] obstacles;

    [Header("Fish settings")]
    [SerializeField] private int _maxActiveFish = 1;
    public GameObject fishPrefab;
    public bool isGameOver;
    public bool isFishing;

    [Header("UI")]
    public UI uIScript;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public Button playButton;
    public Button quitButton;

    private Coroutine _currentFishState;
    private int _currentActiveFishCount = 0;
    private GameObject _currentFish;

    void Start()
    { 
        ToMainMenu();
    }

    public void StartGame()
    {
        isGameOver = false;
        uIScript.DisableMainMenu();
        Instantiate(playerPrefab, _playerSpawnPos, transform.rotation);
        InvokeRepeating("SpawnObstacle", _startDelay, _repeatRate);
        _currentFishState = StartCoroutine(FishCycle());

        Debug.Log("Start game");
    }

    void ToMainMenu()
    {
        Debug.Log("To main menu");
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
                CleanupCurrentFish();
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
            CleanupCurrentFish();
            StopCurrentCoroutine();
            _currentFishState = StartCoroutine(FishCycle());
        }
    }

    public void StartFishing()
    {
        _currentFishState = StartCoroutine(Fishing());
    }

    public void BreakFishing()
    {
        CleanupCurrentFish();
        StopCurrentCoroutine();
        Debug.Log("Fish got away!");
        isFishing = false;
        _currentFishState = StartCoroutine(FishCycle());
    }

    void CleanupCurrentFish()
    {
        if (_currentFish != null)
        {
            Destroy(_currentFish);
            _currentFish = null;
            _currentActiveFishCount--;
        }
    }

    void StopCurrentCoroutine()
    {
        if (_currentFishState != null)
        {
            StopCoroutine(_currentFishState);
            _currentFishState = null;
        }
    }

    void SpawnObstacle()
    {
        if (!isGameOver)
        {
            Instantiate(GetRandomObstacle(), GenerateSpawnPosition(), transform.rotation);
        }
    }

    void SpawnFish()
    {
        if (!isGameOver && _currentActiveFishCount < _maxActiveFish)
        {
            _currentFish = Instantiate(fishPrefab, GenerateFishSpawnPosition(), transform.rotation);
            _currentActiveFishCount++;
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

    public void GameOver()
    {
        CleanupCurrentFish();
        StopCurrentCoroutine();
        isGameOver = true;
        Debug.Log("Game Over!");
        uIScript.EnableGameOverMessage();
        uIScript.DisableGameOverMessage();

        uIScript.EnableMainMenu();
        ToMainMenu();
    }
}
