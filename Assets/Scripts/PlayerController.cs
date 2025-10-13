using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _drag;
    [SerializeField] private float _rotationSpeed = 100f;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _reverseRotationMultiplyer = 1f;
    private float rotationMultiplyer = 1f;

    private Rigidbody _playerRb;
    private float _horizontalInput;
    private float _verticalInput;

    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerRb.linearDamping = _drag; // Damping for straight movement
        _playerRb.angularDamping = _drag * 2f; // Double Damping for rotation
        
        // Limit rotation only in y axis
        _playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        // Input controll
        _verticalInput = Input.GetAxis("Vertical");
        _horizontalInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        // Move forward/back
        Vector3 forwardForce = transform.forward * _verticalInput * _speed;
        _playerRb.AddForce(forwardForce, ForceMode.Force);

        // Reverse rotation while moving back
        if (_verticalInput < -0.1)
        {
            rotationMultiplyer = -_reverseRotationMultiplyer;
        }
        else if (_verticalInput > 0.1 || _verticalInput == 0)
        {
            rotationMultiplyer = 1f;
        }
        
        // Rotate boat 
        float rotationAmount = _horizontalInput * _rotationSpeed * rotationMultiplyer * Time.fixedDeltaTime;
        transform.Rotate(0, rotationAmount, 0, Space.World);
    }
}
