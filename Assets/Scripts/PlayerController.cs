using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _drag;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody _playerRb;
    private bool _isOnGround;
    private bool _jumpRequested;
    public bool isGameOver;
    private float _horizontalInput;
    private float _verticalInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _playerRb.linearDamping = _drag;
    }

    // Update is called once per frame
    void Update()
    {
        // Input of movement on plane
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");

        // Input of jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        // Physics of movement on plane
        Vector3 force = new Vector3(_horizontalInput, 0, _verticalInput) * _speed;
        _playerRb.AddForce(force, ForceMode.Force);

        // IsOnGround check
        _isOnGround = Physics.Raycast(transform.position, Vector3.down, _groundCheckDistance, _groundLayer);

        // Physics of jump
        if (_jumpRequested && _isOnGround)
        {
            _playerRb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            _jumpRequested = false;
            _isOnGround = false;
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Red"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            isGameOver = true;
        }
        else if (other.gameObject.CompareTag("Black"))
        {
            Destroy(gameObject);
            isGameOver = true;
        }
    }
}
