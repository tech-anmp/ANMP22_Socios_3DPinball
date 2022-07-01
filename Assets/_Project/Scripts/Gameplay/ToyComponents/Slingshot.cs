using UnityEngine;

public class Slingshot : ToyComponentBase
{
	[Header("Physics")]
	// change the slingshot force added to a ball
	[SerializeField]
	private float m_SlingshotForce = 10;
	// Minimum contact velocity between ball and slingshot to apply force
	[SerializeField]
	private float m_MinForce = 1;
	// The maximum force apply to the ball
	[SerializeField]
	private float m_RelativeVelocityMax = 1.0f;

	protected override void Start()
	{
		base.Start();

		ResetToyComponent();
	}

	protected override void OnCollisionEnter(Collision Collision)
	{
		if (!Collision.gameObject.CompareTag(m_CompareTag))
			return;

		if (m_IsActive)
		{
			if (OnHit != null)
				OnHit(this);
		}

		PlayAudioClip();

		Rigidbody rb = Collision.gameObject.GetComponent<Rigidbody>();

		if (rb != null && Collision.relativeVelocity.magnitude > m_MinForce)
		{
			if (Collision.relativeVelocity.magnitude < m_RelativeVelocityMax)
			{
				float t = Collision.relativeVelocity.magnitude;
				// reduce the velocity at the impact. Better feeling with the slingshot
				rb.velocity = new Vector3(rb.velocity.x * .5f, rb.velocity.y * .5f, rb.velocity.z * .5f);
				// add force
				rb.AddForce(transform.forward * m_SlingshotForce * t, ForceMode.VelocityChange);
			}
			else
				rb.AddForce(transform.forward * m_SlingshotForce * m_RelativeVelocityMax, ForceMode.VelocityChange);
		}
	}
}