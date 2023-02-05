using UnityEngine;

namespace RocketFly.Scripts
{
    public class CameraFollower : MonoBehaviour
    {
        [SerializeField]
        private float _smoothSpeed = 0.125f;
        [SerializeField] 
        private Vector3 _offset;

        private Transform _target;
        private Transform _initialTransform;
        private bool _isEnabled = false;

        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                print("CameraFollower is On " + _isEnabled);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetPosition(Vector3 pos)
        {
            transform.position = pos;
        }

        private void LateUpdate()
        {
            if (_isEnabled)
            {
                Vector3 desiredPosition = _target.position + _offset;
                Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);
                transform.position = new Vector3(transform.position.x, smoothedPosition.y, transform.position.z);
            }
        }
    }
}