using Assets.Scripts;
using UnityEngine;

public class RocketHeightTracker : MonoBehaviour
{
    public delegate void HeightReachedHandler(float height);
    public event HeightReachedHandler OnHeightReached;

    [SerializeField] 
    private GameConfig _gameConfig;
    
    private float _heightThreshold;
    private float _lastHeight;


    public void Reset()
    {
        _lastHeight = 0f;
    }
    
    private void Awake()
    {
        _heightThreshold = _gameConfig.tunnelSectionHeight;
    }

    private void Start()
    {
        _lastHeight = transform.position.y;
    }

    private void Update()
    {
        float currentHeight = transform.position.y;
        if (currentHeight - _lastHeight > _heightThreshold)
        {
            _lastHeight = currentHeight;
            OnHeightReached?.Invoke(currentHeight);
        }
    }
}