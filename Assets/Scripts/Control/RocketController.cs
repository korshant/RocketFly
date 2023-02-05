using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Control
{
    public class RocketController : MonoBehaviour
    {

        private const float DEFAULT_THRUST_FORCE_VALUE = 800f;
        public delegate void RocketTriggerEvent();
        public delegate void RocketCollideEvent(Collision collision);
        public event RocketTriggerEvent OnRocketHitTrigger;
        public event RocketCollideEvent OnRocketCollide;

        [SerializeField] 
        private GameConfig _gameConfig;
        [SerializeField] 
        private ParticleSystem[] _particleSystems;
        [SerializeField] 
        private AudioSource _audioSource;
        [SerializeField] 
        private AudioSource _explosionSource;
        [SerializeField] 
        private ParticleSystem[] _explosionParticleSystems;

        private float _shipSpeed;
        private Sequence _fallingSequence;
        private Rigidbody _rigidBody;
        private bool _isEnabled = true;
        private bool _isLaunched = false;

        public float ShipSpeed
        {
            get => _shipSpeed;
            set => _shipSpeed = DEFAULT_THRUST_FORCE_VALUE * value;
        }

        public Sequence FallingSequence
        {
            get => _fallingSequence;
            private set => _fallingSequence = value;
        }
        public bool IsLaunched
        {
            get => _isLaunched;
            set
            {
                _isLaunched = value;
            }
        }
        public bool IsEnabled
        {
            get => _isEnabled;
            set
            {
                _isEnabled = value;
                if(!_isEnabled) StopThrust();
            }
        }

        private void Awake()
        {
            _shipSpeed = _gameConfig.rocketSpeed * DEFAULT_THRUST_FORCE_VALUE;
        }
    
        private void Start ()
        {
            foreach (var particleSystem in _explosionParticleSystems)
            {
                particleSystem.Stop();
            }
            _rigidBody = GetComponent<Rigidbody>();
            _isEnabled = true;
        }
        private void FixedUpdate () {
            if (_isEnabled)
            {
                Thrust();
                if(_isLaunched) Rotate();
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            OnRocketCollide?.Invoke(collision);
        }
        private void OnTriggerEnter()
        {
            OnRocketHitTrigger?.Invoke();
        }

        private void Thrust()
        {
            float thrustThisFrame = _shipSpeed * Time.deltaTime;

            if (Input.GetMouseButton(0))
            {
                _rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
                if(!_audioSource.isPlaying) _audioSource.Play();
                foreach (var particleSystem in _particleSystems)
                {
                    particleSystem.Play();
                }
            }
            else
            {
                StopThrust();
            }
        }

        private void StopThrust()
        {
            if(_audioSource.isPlaying) _audioSource.Stop();
            
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Stop();
            }
        }

        private void Rotate()
        {
            _rigidBody.freezeRotation = true;

            if (Input.GetMouseButton(0))
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = transform.position.z - Camera.main.transform.position.z;
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
                Vector3 direction = (worldPos - transform.position).normalized;
                direction.y = 0.5f;
                Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
                transform.DORotateQuaternion(targetRotation, 0.5f);
            }

            _rigidBody.freezeRotation = false;
        }
   
        public void Explode()
        {
            _explosionSource.Play();
           
            foreach (var particleSystem in _explosionParticleSystems)
            {
                particleSystem.Play();
            }
        }

        public void EnableFallingMode()
        {
            Quaternion targetRotation1 = new Quaternion(0f, 0f, 1f,0f);
            _fallingSequence = DOTween.Sequence()
                .Append(transform.DORotateQuaternion(targetRotation1, 2f));
        }

        public void DisableFallingMode()
        {
            if (_fallingSequence != null) if(_fallingSequence.active) _fallingSequence.Kill();
        }
    }
}