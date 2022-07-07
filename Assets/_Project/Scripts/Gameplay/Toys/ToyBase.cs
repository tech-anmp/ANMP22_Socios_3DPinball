using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class ToyBase : MonoBehaviour
{
    [Header("Difficulty")]
    [SerializeField]
    protected bool m_ResetOnBallReset;
    [SerializeField]
    protected bool m_ResetOnCompleted;

    [Header("Score")]
    [SerializeField]
    protected bool m_SendPoints = true;
    [SerializeField]
    protected int m_Points = 100;

    [Header("Audio")]
    [SerializeField]
    protected AudioClip m_AudioClip;

    protected AudioSource m_AudioSource;

    protected bool m_IsActivated;

    public Action<ToyBase, int> OnSendPoints;

    public bool IsActivated { get => m_IsActivated; }
    public bool RestartOnBallReset { get => m_ResetOnBallReset; }
    public bool RestartOnCompleted { get => m_ResetOnCompleted; }


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

    public abstract void Restart();

    public void PlayAudioClip()
    {
        m_AudioSource.PlayOneShot(m_AudioClip);
    }
}