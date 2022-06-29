using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownGame : MonoBehaviour
{
    [SerializeField]
    private float slowDownTimeScale = 0.5f;

    private bool isSlowedDown;

    [SerializeField]
    private bool m_dontDestroyOnLoad;

    private void Awake()
    {
        if (m_dontDestroyOnLoad)
            DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.S))
        {
            isSlowedDown = !isSlowedDown;

            if (isSlowedDown)
            {
                Time.timeScale = slowDownTimeScale;
                DOTween.timeScale = Time.timeScale;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
            else
            {
                Time.timeScale = 1.0f;
                DOTween.timeScale = Time.timeScale;
                Time.fixedDeltaTime = 0.02F * Time.timeScale;
            }
        }
#endif
    }
}
