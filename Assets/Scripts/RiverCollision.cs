using UnityEngine;

public class RiverCollision : MonoBehaviour
{
    private ScrollingWaterTexture _scrollingWaterTextureScript;
    [SerializeField] private float _logFlowMultiplayer = 300f;
    [SerializeField] private float _branchFlowMultiplayer = 150f;
    [SerializeField] private float _playerFlowMultiplayer = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scrollingWaterTextureScript = FindAnyObjectByType<ScrollingWaterTexture>();

    }

    void OnCollisionStay(Collision collision)
    {
        Rigidbody Rb = collision.gameObject.GetComponent<Rigidbody>();

        // Local var!!!
        float flowMultiplayer;

        if (collision.gameObject.CompareTag("Log"))
        {
            flowMultiplayer = _logFlowMultiplayer;
        }
        else if (collision.gameObject.CompareTag("Branch"))
        {
            flowMultiplayer = _branchFlowMultiplayer;
        }
        else
        {
            flowMultiplayer = _playerFlowMultiplayer;
        }

        Rb.AddForce(_scrollingWaterTextureScript.flowSpeed * flowMultiplayer, ForceMode.Force);
    }
}
