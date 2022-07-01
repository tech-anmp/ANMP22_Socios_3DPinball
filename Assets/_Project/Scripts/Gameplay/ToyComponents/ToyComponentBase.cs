using System;
using UnityEngine;

[RequireComponent(typeof(Collision))]
[RequireComponent(typeof(AudioSource))]
public class ToyComponentBase : MonoBehaviour
{
	[Header("Tag")]
	[SerializeField]
	protected string m_CompareTag = "Ball";

	[Header("Audio")]
	[SerializeField]
	private AudioClip m_AudioClip;

	protected bool m_IsActive;
	private AudioSource m_AudioSource;

	public Action<ToyComponentBase> OnHit;

	protected virtual void Start()
    {
		m_AudioSource = GetComponent<AudioSource>();
	}

	protected virtual void OnCollisionEnter(Collision Collision)
    {

    }

	public void PlayAudioClip()
    {
		m_AudioSource.PlayOneShot(m_AudioClip);
	}

	public virtual void ResetToyComponent() { }

	public virtual void SetActive(bool IsActive)
	{
		m_IsActive = IsActive;
	}
}