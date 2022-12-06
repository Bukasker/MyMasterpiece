using UnityEngine;

public class Arrow : MonoBehaviour
{

	[SerializeField] private Rigidbody rb;
	private bool hasHit;
    void Update()
    {
	    if (!hasHit)
	    {
		    float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
		    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
		}
    }

    private void OnCollisionEnter(Collision col)
    {
		hasHit = true;
		rb.velocity = Vector3.zero;
		rb.isKinematic = true;
    }
}
