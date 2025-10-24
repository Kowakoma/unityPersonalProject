using UnityEngine;

public class RiverCollision : MonoBehaviour
{
    [Header("Flow force settings")]
    [SerializeField] private float _logFlowMultiplayer = 300f;
    [SerializeField] private float _branchFlowMultiplayer = 150f;
    [SerializeField] private float _playerFlowMultiplayer = 1000f;
    private ScrollingWaterTexture _scrollingWaterTextureScript;

    void Start()
    {
        _scrollingWaterTextureScript = FindAnyObjectByType<ScrollingWaterTexture>();
    }

    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = collision.collider.GetComponent<Rigidbody>();
        if (rb == null) return;

        float flowMultiplayer = 0f;

        if (collision.gameObject.CompareTag("Log"))
        {
            flowMultiplayer = _logFlowMultiplayer;
        }
        else if (collision.gameObject.CompareTag("Branch"))
        {
            flowMultiplayer = _branchFlowMultiplayer;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            flowMultiplayer = _playerFlowMultiplayer;
        }

        rb.AddForce(_scrollingWaterTextureScript.flowSpeed * flowMultiplayer, ForceMode.Force);
    }
}
