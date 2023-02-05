using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu]
    public class GameConfig : ScriptableObject
    {
        [Header("Rocket Properties")]
        [Tooltip("Multiplier to the force produced by a rocket engine that moves the rocket.")]
        [Range(0, 2f)]
        public float rocketSpeed = 1f;

        [Space(10)] [Header("Tunnel Properties")]
        [Tooltip("A tunnel section with a launching platform and no collision detection")]
        public GameObject firstTunnelSection;
        
        [Tooltip("Tunnel sections used to randomly generate tunnel")]
        public GameObject[] randomTunnelSections;
        
        [Tooltip("Section height. Used to specify Y shift for sections placement")]
        public float tunnelSectionHeight;
        
        [Tooltip("The amount of rockets Y travel before next Section is being spawned/placed")]
        public float spawnDistanceGap;

        [Tooltip("The height difference that needs to be reached for the rocket's height event to fire")]
        public float heightTreshold;

    }
}
