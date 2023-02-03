using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;
    [Range(-13f,0f)]
    [SerializeField] private float _startingYPos;

    private bool _isEnabled = false;
    private bool _isLaunched = false;

    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = value;
    }

    private void LateUpdate()
    {
        if (!_isLaunched && target.position.y > _startingYPos)
        {
            _isLaunched = true;
            _isEnabled = true;
        }
        if (_isEnabled)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = new Vector3(transform.position.x, smoothedPosition.y, transform.position.z);
        }
    }
}