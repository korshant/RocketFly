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
    private void OnRocketCollide()
    {
        if (_isGameStarted)
        {
            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    _rocket.IsEnabled = false;
                    _isGameStarted = false;
                    _follower.IsEnabled = false;
                })
                .AppendInterval(3f)
                .AppendCallback(StartGame);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(0);
    }
}
