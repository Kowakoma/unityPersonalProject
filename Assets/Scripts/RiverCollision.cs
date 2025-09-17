using Unity.VisualScripting;
using UnityEngine;

public class RiverCollision : MonoBehaviour
{
    private ScrollingWaterTexture _scrollingWaterTextureScript;
    private float _flowMultiplayer = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scrollingWaterTextureScript = FindAnyObjectByType<ScrollingWaterTexture>();

    }

    void OnCollisionStay(Collision collision)
    {
        Rigidbody Rb = collision.gameObject.GetComponent<Rigidbody>();

        if (collision.gameObject.CompareTag("Log"))
        {
            _flowMultiplayer = 300;
        }
        else if (collision.gameObject.CompareTag("Branch"))
        {
            _flowMultiplayer = 200;
        }
        else
        {
            _flowMultiplayer = 1;
        }

        Rb.AddForce(_scrollingWaterTextureScript.flowSpeed * _flowMultiplayer, ForceMode.Acceleration);
    }
}
