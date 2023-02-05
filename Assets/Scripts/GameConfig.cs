using UnityEngine;

namespace RocketFly.Scripts
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [Header("Rocket Properties")]
        [Tooltip("Multiplier to the force produced by a rocket engine that moves the rocket.")]
        [Range(0, 2f)]
        public float rocketSpeed = 1f;
        public Vector3 rocketStartPos;
        public GameObject _rocketPrefab;

        [Space(10)] [Header("Tunnel Properties")]
        [Tooltip("Tunnel sections used to randomly generate tunnel.")]
        public GameObject[] randomTunnelSections;
        
        [Tooltip("First tunnel section")]
        public GameObject firstTunnelSection;
        
        [Tooltip("Single sections height. Should be similar to all the sections. Used to specify Y shift for sections placement")]
        public float tunnelSectionHeight;

        [Header("Camera Properties")]
        public Vector3 cameraStartPos;

    }
}
