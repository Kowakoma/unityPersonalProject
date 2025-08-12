//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class RedCubeMovement : MonoBehaviour
{
    private Rigidbody _redCubeRb;
    [SerializeField] private float _speed;
    public GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _redCubeRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector3.back * _speed);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Blue"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Black"))
        {
            Destroy(gameObject);
        }
    }

}
