using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    [SerializeField] private float _speed;
    [SerializeField] private float _drag;
    [SerializeField] private float _horizontalInput;
    [SerializeField] private float _verticalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerRb.linearDamping = _drag;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        Vector3 force = new Vector3(_horizontalInput, 0, _verticalInput) * _speed;
        _playerRb.AddForce(force, ForceMode.Force);
    }
}
