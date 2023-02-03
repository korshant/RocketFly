using DG.Tweening;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public delegate void RocketTriggerEvent();
    public delegate void RocketCollideEvent(Collision collision);
    public event RocketTriggerEvent OnRocketHitTrigger;
    public event RocketCollideEvent OnRocketCollide;
    
    [SerializeField] 
    private ParticleSystem[] _particleSystems;
    [SerializeField] 
    private AudioSource _audioSource;
    [SerializeField] 
    private float shipThrust = 100f;
    [SerializeField] 
    private ParticleSystem[] _explosionParticleSystems;

    private Sequence seq;
    private Rigidbody rigidBody;
    private bool _isEnabled = true;
    
    public Sequence FallingSequence
    {
        get => seq;
        private set => seq = value;
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

    private void Start ()
    {
        foreach (var particleSystem in _explosionParticleSystems)
        {
            particleSystem.Stop();
        }
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
   
   private void OnCollisionEnter(Collision collision)
   {
       OnRocketCollide?.Invoke(collision);
   }

   private void OnTriggerEnter()
   {
       OnRocketHitTrigger?.Invoke();
   }

   public void Explode()
   {
       foreach (var particleSystem in _explosionParticleSystems)
       {
           particleSystem.Play();
       }
   }

   public void EnableFallingMode()
   {
       Quaternion targetRotation1 = new Quaternion(0f, 0f, 1f,0f);
       seq = DOTween.Sequence()
           .Append(transform.DORotateQuaternion(targetRotation1, 2f));
   }

   public void DisableFallingMode()
   {
       if (seq != null) if(seq.active) seq.Kill();
       
   }
}