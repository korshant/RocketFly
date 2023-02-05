using UnityEngine;

namespace RocketFly.Scripts
{
    public class LaunchTracker : MonoBehaviour
    {
        public delegate void RocketLaunched();
        public event RocketLaunched OnRocketLaunch;

        [Range(-13f, 0f)]
        [SerializeField]
        private float _startingYPos;

        private bool _isLaunched = false;

        public bool IsLaunched
        {
            get => _isLaunched;
            set => _isLaunched = value;
        }

        private void Update()
        {
            if (!_isLaunched && transform.position.y > _startingYPos)
            {
                print("Rocket Launched");
                _isLaunched = true;
                OnRocketLaunch?.Invoke();
            }
        }
    }
}
