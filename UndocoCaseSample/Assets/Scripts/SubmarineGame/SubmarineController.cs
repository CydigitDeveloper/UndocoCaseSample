using UnityEngine;

public class SubmarineController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 10f;

    [Header("Idle Bobbing")]
    [SerializeField] private float bobbingAmount = 0.5f;
    [SerializeField] private float bobbingSpeed = 2f;
    
    private Rigidbody _rb;
    private SubmarineInputManager _submarineInputManager;
    private Quaternion _targetRotation;
    private float _defaultY;

    private bool _isGameActive = true;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _submarineInputManager = GetComponent<SubmarineInputManager>();
        _defaultY = transform.position.y;
    }

    void Update()
    {
        if (!_isGameActive) return;
        
        Rotation();
    }

    void FixedUpdate()
    {
        if (!_isGameActive)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }
        
        Movement();
    }

    private void Movement()
    {
        Vector2 finalVelocity = _submarineInputManager.moveInput * speed;
        
        if (Mathf.Abs(_submarineInputManager.moveInput.y) < 0.1f)
        {
            float bobbingValue = Mathf.Cos(Time.time * bobbingSpeed) * bobbingAmount;
            finalVelocity.y = bobbingValue;
        }
        
        _rb.linearVelocity = finalVelocity;
    }
    
    private void Rotation()
    {
        if (_submarineInputManager.moveInput.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(_submarineInputManager.moveInput.y, _submarineInputManager.moveInput.x) * Mathf.Rad2Deg;
            _targetRotation = Quaternion.Euler(0, 0, angle);
            
            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void SetActiveState(bool state)
    {
        _isGameActive = state;
        if (!state && _rb != null) _rb.linearVelocity = Vector2.zero;
    }
}