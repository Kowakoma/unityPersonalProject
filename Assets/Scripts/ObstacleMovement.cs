using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] private SpawnManager _spawnManagerScript;
    public GameObject player;

    void Start()
    {
        // Find Spawn Manager script
        _spawnManagerScript = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
    }

    void FixedUpdate()
    {
        // Destroy obstacles out of visible bounds
      if (gameObject.transform.position.y < -15)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // Game over when player collide with obstacle
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);

            _spawnManagerScript.GameOver();
        }
    }
}