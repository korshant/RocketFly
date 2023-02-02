using UnityEngine;
public class CenterOfGravity : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField]
    private Transform centerOfGravity;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (centerOfGravity)
        {
            rb.centerOfMass = centerOfGravity.localPosition;
        }
    }
}
