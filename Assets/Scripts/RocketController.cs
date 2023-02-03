using DG.Tweening;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public delegate void RocketEvent();
    public event RocketEvent OnRocketHitTrigger;
    public event RocketEvent OnRocketCollide;
    
    [SerializeField] 
    private ParticleSystem[] _particleSystems;
    [SerializeField] 
    private AudioSource _audioSource;
    [SerializeField] 
    private float shipThrust = 100f;

    private Rigidbody rigidBody;
    private bool _isEnabled = true;

    public bool IsEnabled
    {
        get => _isEnabled;
        set => _isEnabled = value;
    }

    private void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        _isEnabled = true;
    }

    private void FixedUpdate () {
        if (_isEnabled)
        {
            Thrust();
            Rotate();
        }
    }

    private void Thrust()
    {
        float thrustThisFrame = shipThrust * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
            if(!_audioSource.isPlaying) _audioSource.Play();
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Play();
            }
        }
        else
        {
            if(_audioSource.isPlaying) _audioSource.Stop();
            
            foreach (var particleSystem in _particleSystems)
            {
                particleSystem.Stop();
            }
        }
    }

   private void Rotate()
    {
        rigidBody.freezeRotation = true;

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

        rigidBody.freezeRotation = false;
    }
   private void OnCollisionEnter()
   {
       OnRocketCollide?.Invoke();
   }

   private void OnTriggerEnter()
   {
       OnRocketHitTrigger?.Invoke();
   }
}