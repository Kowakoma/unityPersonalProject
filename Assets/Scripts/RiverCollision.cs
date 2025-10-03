using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class RiverCollision : MonoBehaviour
{
    [Header("Flow force settings")]
    [SerializeField] private float _logFlowMultiplayer = 300f;
    [SerializeField] private float _branchFlowMultiplayer = 150f;
    [SerializeField] private float _playerFlowMultiplayer = 100f;
    private ScrollingWaterTexture _scrollingWaterTextureScript;
    private Dictionary<Collider, Rigidbody> _rigidbodyCache = new(); // Dictionary for obstacles rigidbody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _scrollingWaterTextureScript = FindAnyObjectByType<ScrollingWaterTexture>();
    }

    void OnCollisionStay(Collision collision)
    {
        Rigidbody rb = GetCachedRigidbody(collision.collider);

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

        rb.AddForce(_scrollingWaterTextureScript.flowSpeed * flowMultiplayer, ForceMode.Force);
    }

    private Rigidbody GetCachedRigidbody(Collider collider)
    {
        // check is required value in cache?
        if (!_rigidbodyCache.TryGetValue(collider, out Rigidbody rb))
        {
            // get not cached rigidbody from collider
            rb = collider.GetComponent<Rigidbody>();
            
            // add rigidbody to cache
            _rigidbodyCache[collider] = rb;
        }
        return rb;
    }
}
