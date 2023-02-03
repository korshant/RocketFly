using Unity.VisualScripting;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    [SerializeField] private LaunchTracker _launchTracker;

    private bool _isEnabled = false;

    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = value;
    }

    private void OnEnable()
    {
        _launchTracker.OnRocketLaunch += LaunchTrackerOnOnRocketLaunch;
    }

    private void OnDisable()
    {
        _launchTracker.OnRocketLaunch -= LaunchTrackerOnOnRocketLaunch;
    }
    
    private void LateUpdate()
    {
        if (_isEnabled)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(transform.position.x, smoothedPosition.y, transform.position.z);
        }
    }
    
    private void LaunchTrackerOnOnRocketLaunch()
    {
        _isEnabled = true;
    }
}