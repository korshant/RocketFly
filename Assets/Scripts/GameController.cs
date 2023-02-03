using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private TunnelSpawner _tunnelSpawner;
    [SerializeField] private Transform _rocketBaseTransform;
    [SerializeField] private RocketController _rocket;
    [SerializeField] private CameraFollower _follower;
    [SerializeField] private BoxCollider _startGameTrigger;

    private bool _isGameStarted;
    public bool IsGameStarted => _isGameStarted;

    private void OnEnable()
    {
        _rocket.OnRocketCollide += OnRocketCollide;
        _rocket.OnRocketHitTrigger += OnRocketHitTrigger;
    }

    private void OnDisable()
    {
        _rocket.OnRocketCollide -= OnRocketCollide;
        _rocket.OnRocketHitTrigger -= OnRocketHitTrigger;
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
            _follower.IsEnabled = false;
            DOTween.Sequence()
                .AppendInterval(0.5f)
                .AppendCallback(_rocket.EnableFallingMode)
                .AppendInterval(2f)
                .AppendCallback(StartGame);
        } else if (_rocket.FallingSequence != null)
        {
            _rocket.Explode();
            if(_rocket.FallingSequence.active) _rocket.DisableFallingMode();
        }
    }

    public void StartGame()
    {
        print("Load");
        SceneManager.LoadScene(0);
    }
}
