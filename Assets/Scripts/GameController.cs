using Assets.Scripts.Control;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        private bool _isGameStarted;

        [SerializeField] 
        private GameConfig _gameConfig;
    
        [SerializeField]
        private TunnelSpawner _tunnelSpawner;

        [SerializeField]
        private RocketController _rocket;

        [SerializeField]
        private CameraFollower _cameraFollower;

        [SerializeField]
        private BoxCollider _startGameTrigger;

        [SerializeField]
        private LaunchTracker _launchTracker;
    
        public bool IsGameStarted => _isGameStarted;

        public void StartGame()
        {
            // reset states
            // spawn player
            // spawn blocks
            SceneManager.LoadScene(0);
        }

        private void Awake()
        {
            _rocket.ShipSpeed = _gameConfig.rocketSpeed;
        }

        private void ResetComponents()
        {
        
        }
    
        private void OnEnable()
        {
            _launchTracker.OnRocketLaunch += LaunchTrackerOnOnRocketLaunch;
            _rocket.OnRocketCollide += OnRocketCollide;
            _rocket.OnRocketHitTrigger += OnRocketHitTrigger;
        }

        private void OnDisable()
        {
            _launchTracker.OnRocketLaunch -= LaunchTrackerOnOnRocketLaunch;
            _rocket.OnRocketCollide -= OnRocketCollide;
            _rocket.OnRocketHitTrigger -= OnRocketHitTrigger;
        }

        private void LaunchTrackerOnOnRocketLaunch()
        {
            _rocket.IsLaunched = true;
        }

        private void OnRocketHitTrigger()
        {
            _isGameStarted = true;
        }

        private void OnRocketCollide(Collision collision)
        {
            if (_isGameStarted)
            {
                _isGameStarted = false;

                _rocket.Explode();
                _rocket.IsEnabled = false;
                _isGameStarted = false;
                _cameraFollower.IsEnabled = false;
                DOTween.Sequence()
                    .AppendInterval(0.5f)
                    .AppendCallback(_rocket.EnableFallingMode)
                    .AppendInterval(2f)
                    .AppendCallback(StartGame);
            }
            else if (_rocket.FallingSequence != null)
            {
                _rocket.Explode();
                if (_rocket.FallingSequence.active)
                {
                    _rocket.DisableFallingMode();
                }
            }
        }
    }
}

