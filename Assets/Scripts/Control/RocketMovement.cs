using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RocketMovement : MonoBehaviour
{
    public delegate void RocketEvent();
    public static event RocketEvent OnRocketHit;
    
    private Rigidbody rigidBody;

    [SerializeField] [Range(0.01f, 10f)] private float coeff = 1f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;

            Vector3 direction = (targetPosition - transform.position).normalized;
            direction.y = 1f;

            rigidBody.AddRelativeForce(Vector3.forward * coeff, ForceMode.Force);
            rigidBody.AddRelativeTorque(direction * coeff, ForceMode.Impulse);
        }
    }
}