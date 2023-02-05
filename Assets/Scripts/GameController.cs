using DG.Tweening;
using UnityEngine;

namespace RocketFly.Scripts
{
    public class GameController : MonoBehaviour
    {
        private bool _isGameStarted;
        private Rocket _rocket;
        private RocketHeightTracker _heightTracker;
        private LaunchTracker _launchTracker;

        [SerializeField] 
        private GameConfig _gameConfig;
    
        [SerializeField]
        private TunnelSpawner _tunnelSpawner;

        [SerializeField]
        private CameraFollower _cameraFollower;

        public bool IsGameStarted => _isGameStarted;
        
        private void Start()
        {
            StartGame();
        }
        
        private void OnDisable()
        {
            DisableEventsTracking();
        }

        private void EnableEventsTracking()
        {
            _heightTracker.OnHeightReached += OnHeightReached;
            _launchTracker.OnRocketLaunch += OnRocketLaunch;
            _rocket.OnRocketCollide += OnRocketCollide;
            _rocket.OnRocketHitTrigger += OnRocketHitTrigger;
        }

        private void DisableEventsTracking()
        {
            _heightTracker.OnHeightReached -= OnHeightReached;
            _launchTracker.OnRocketLaunch -= OnRocketLaunch;
            _rocket.OnRocketCollide -= OnRocketCollide;
            _rocket.OnRocketHitTrigger -= OnRocketHitTrigger;
        }
        
        private void StartGame()
        {
            SpawnRocket();
            ResetCameraFollower();
            EnableEventsTracking();
            SpawnTunnel();
        }

        private void SpawnTunnel()
        {
            _tunnelSpawner.Configure(_gameConfig);
            _tunnelSpawner.SpawnFirstSection();
            _tunnelSpawner.SpawnRandomSection();
        }
        
        private void SpawnRocket()
        {
            print("SpawnRocket");
            GameObject gameObject =
                Instantiate(_gameConfig._rocketPrefab, _gameConfig.rocketStartPos, Quaternion.identity);
            _rocket = gameObject.GetComponent<Rocket>();
            _heightTracker = gameObject.GetComponent<RocketHeightTracker>();
            _launchTracker = gameObject.GetComponent<LaunchTracker>();
            
            _rocket.ShipSpeed = _gameConfig.rocketSpeed;
        }

        private void Restart()
        {
            print("Game Restart");
            _isGameStarted = false;
            _tunnelSpawner.Reset();
            _rocket.Reset();
            ResetCameraFollower();
            EnableEventsTracking();
        }

        private void ResetCameraFollower()
        {
            _cameraFollower.SetPosition(_gameConfig.cameraStartPos);
        }

        private void OnHeightReached(float height)
        {
            _tunnelSpawner.SpawnRandomSection();
        }

        private void OnRocketLaunch()
        {
            _rocket.IsLaunched = true;
            _cameraFollower.IsEnabled = true;
            _cameraFollower.SetTarget(_rocket.transform);
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
                    .AppendCallback(()=>
                    {
                        _rocket.EnableFallingMode();
                    })
                    .AppendInterval(2f)
                    .AppendCallback(Restart);
            }
            else if (_rocket.FallingSequence != null)
            {
                if (_rocket.FallingSequence.active)
                {
                    _rocket.DisableFallingMode();
                    DisableEventsTracking();
                }
            }
        }
    }
}

