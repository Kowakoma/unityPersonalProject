using Unity.VisualScripting;
using UnityEngine;

public class RiverCollision : MonoBehaviour
{
    private ScrollingWaterTexture _scrollingWaterTextureScript;
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
            flowMultiplayer = 300;
        }
        else if (collision.gameObject.CompareTag("Branch"))
        {
            flowMultiplayer = 200;
        }
        else
        {
            flowMultiplayer = 1;
        }

        Rb.AddForce(_scrollingWaterTextureScript.flowSpeed * flowMultiplayer, ForceMode.Acceleration);
    }
}
