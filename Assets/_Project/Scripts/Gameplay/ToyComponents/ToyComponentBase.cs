using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class ToyComponentBase : MonoBehaviour
{
    [Header("Tag")]
    [SerializeField]
    protected string m_CompareTag = "Ball";

    [Header("Score")]
    [SerializeField]
    protected int m_Points = 10;

    [Header("Audio")]
    [SerializeField]
    protected AudioClip m_AudioClip;

    protected bool m_IsActive;
    protected AudioSource m_AudioSource;

    public Action<ToyComponentBase> OnHit;

    public int Points { get => m_Points; }

    protected virtual void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnCollisionEnter(Collision Collision) { }

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