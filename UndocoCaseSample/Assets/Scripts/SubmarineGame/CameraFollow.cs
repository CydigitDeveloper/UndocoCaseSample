using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10f);

    [Header("Smooth Settings")]
    [SerializeField] private float smoothTime = 0.25f;
    
    private Vector3 _currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        if (!target) return;
        
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
    }
}