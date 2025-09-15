using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private Rigidbody _obstacleRb;
    private ScrollingWaterTexture _scrollingWaterTextureScript;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scrollingWaterTextureScript = FindAnyObjectByType<ScrollingWaterTexture>();

        _obstacleRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      //  transform.Translate(_scrollingWaterTextureScript.flowSpeed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (gameObject.transform.position.y < -9)
        {
            Destroy(gameObject);
        }
    }
}
