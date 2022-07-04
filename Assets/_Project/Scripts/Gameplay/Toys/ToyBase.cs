using System;
using UnityEngine;

public abstract class ToyBase : MonoBehaviour
{
    [Header("Score")]
    [SerializeField]
    private int m_Points = 100;

    [Header("Audio")]
    [SerializeField]
    private AudioClip m_AudioClip;

    private AudioSource m_AudioSource;

    protected bool m_IsActivated;

    public Action<ToyBase, int> OnSendPoints;

    public bool IsActivated { get => m_IsActivated; }

    protected virtual void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
    }

    protected virtual void SendPoints()
    {
        if (OnSendPoints != null)
            OnSendPoints(this, m_Points);
    }

    public abstract void Activate();

    public abstract void DeActivate();

    public abstract void ResetToy();

    public void PlayAudioClip()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
    }
}