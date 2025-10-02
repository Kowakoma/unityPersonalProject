using UnityEngine;

public class FishBehaviour : MonoBehaviour
{   
    [SerializeField] private SpawnManager _spawnManagerScript;
    public GameObject player;

    void Start()
    {
        // Find Spawn Manager script
        _spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_spawnManagerScript.isFishing)
        {
            _spawnManagerScript.StartFishing();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && _spawnManagerScript.isFishing)
        {
            _spawnManagerScript.BreakFishing();
        }
    }
}