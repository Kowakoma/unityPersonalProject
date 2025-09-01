using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoatController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float acceleration = 2f;
    [SerializeField] private float reverseSpeed = 5f;
    [SerializeField] private float rotationSpeed = 1f;
    
    [Header("Water Physics")]
    [SerializeField] private float buoyancyForce = 10f;
    [SerializeField] private float waterDrag = 1f;
    [SerializeField] private float waterAngularDrag = 1f;
    [SerializeField] private float waterHeight = 0f;
    
    private Rigidbody rb;
    private float currentSpeed;
    private bool inWater;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    private void FixedUpdate()
    {
        CheckWaterStatus();
        ApplyBuoyancy();
        HandleMovement();
    }
    
    private void CheckWaterStatus()
    {
        // Простая проверка, находится ли лодка в воде
        inWater = transform.position.y <= waterHeight;
    }
    
    private void ApplyBuoyancy()
    {
        if (inWater)
        {
            // Применяем силу плавучести
            float submersion = Mathf.Clamp01((waterHeight - transform.position.y) / 1f);
            rb.AddForce(Vector3.up * buoyancyForce * submersion, ForceMode.Acceleration);
            
            // Увеличиваем сопротивление в воде
            rb.linearDamping = waterDrag;
            rb.angularDamping = waterAngularDrag;
        }
        else
        {
            rb.linearDamping = 0.1f;
            rb.angularDamping = 0.1f;
        }
    }
    
    private void HandleMovement()
    {
        if (!inWater) return;
        
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        
        // Управление скоростью
        if (moveInput > 0.1f) // Движение вперед
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, acceleration * Time.fixedDeltaTime);
        }
        else if (moveInput < -0.1f) // Движение назад
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -reverseSpeed, acceleration * Time.fixedDeltaTime);
        }
        else // Плавное замедление
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0f, acceleration * 2 * Time.fixedDeltaTime);
        }
        
        // Применение движения
        Vector3 moveForce = transform.TransformDirection(Vector3.forward) * currentSpeed;
        rb.AddForce(moveForce, ForceMode.Acceleration);
        
        // Управление поворотом
        if (Mathf.Abs(currentSpeed) > 0.1f)
        {
            float turnForce = turnInput * rotationSpeed * (currentSpeed / maxSpeed);
            rb.AddTorque(transform.up * turnForce, ForceMode.Acceleration);
        }
    }
    
    // Для визуализации воды в редакторе
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, waterHeight, transform.position.z), 
                           new Vector3(20, 0.1f, 20));
    }
}