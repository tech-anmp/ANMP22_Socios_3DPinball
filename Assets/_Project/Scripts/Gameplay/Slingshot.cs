using System;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
	// change the slingshot force added to a ball
	[SerializeField]
	private float m_SlingshotForce = 10;

	[SerializeField]
	private string m_CompareTag = "Ball";

	// Minimum contact velocity between ball and slingshot to apply force
	[SerializeField]
	private float m_MinForce = 1;
	// The maximum force apply to the ball
	[SerializeField]
	private float m_RelativeVelocityMax = 1.0f;

	[SerializeField]
	private AudioClip m_AudioClip;

	private bool m_IsActive;
	private AudioSource m_AudioSource;

	public Action<Slingshot> OnHit;

	private void Start()
	{
		m_AudioSource = GetComponent<AudioSource>();
		ResetSlingshot();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (!collision.gameObject.CompareTag(m_CompareTag))
			return;

		if (m_IsActive)
		{
			if (OnHit != null)
				OnHit(this);
		}

		m_AudioSource.PlayOneShot(m_AudioClip);

		Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

		if (rb != null && collision.relativeVelocity.magnitude > m_MinForce)
		{
			if (collision.relativeVelocity.magnitude < m_RelativeVelocityMax)
			{
				float t = collision.relativeVelocity.magnitude;
				// reduce the velocity at the impact. Better feeling with the slingshot
				rb.velocity = new Vector3(rb.velocity.x * .5f, rb.velocity.y * .5f, rb.velocity.z * .5f);
				// add force
				rb.AddForce(transform.forward * m_SlingshotForce * t, ForceMode.VelocityChange);
			}
			else
				rb.AddForce(transform.forward * m_SlingshotForce * m_RelativeVelocityMax, ForceMode.VelocityChange);
		}
	}

	public void ResetSlingshot()
	{
		
	}

	public void SetActive(bool IsActive)
	{
		m_IsActive = IsActive;
	}
}