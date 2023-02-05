using Assets.Scripts;
using UnityEngine;

public class RocketHeightTracker : MonoBehaviour
{
    public delegate void HeightReachedHandler(float height);
    public event HeightReachedHandler OnHeightReached;

    [SerializeField] 
    private GameConfig _gameConfig;
    
    private float _heightThreshold;
    private float lastHeight;

    private void Awake()
    {
        _heightThreshold = _gameConfig.heightTreshold;
    }

    private void Start()
    {
        lastHeight = transform.position.y;
    }

    private void Update()
    {
        float currentHeight = transform.position.y;
        if (currentHeight - lastHeight > _heightThreshold)
        {
            lastHeight = currentHeight;
            OnHeightReached?.Invoke(currentHeight);
        }
    }
}