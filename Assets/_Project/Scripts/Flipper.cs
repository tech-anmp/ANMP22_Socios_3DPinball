using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    [SerializeField]
    private float m_PressedPosition = 20;
    [SerializeField]
    private float m_Time = 0.5f;

    private bool m_IsInRestPos = true;
    private Vector3 m_RestRotation;

    private void Start()
    {
        m_IsInRestPos = true;
        m_RestRotation = transform.localRotation.eulerAngles;
    }

    public void Spring()
    {
        if (m_IsInRestPos)
        {
            m_IsInRestPos = false;
            StartCoroutine(StartRot());
        }
    }

    private IEnumerator StartRot()
    {
        Tween rotStartTween = transform.DOLocalRotate(new Vector3(m_RestRotation.x, m_PressedPosition, m_RestRotation.z), m_Time, RotateMode.Fast);
        yield return rotStartTween.WaitForCompletion();

        Tween rotEndTween = transform.DOLocalRotate(new Vector3(m_RestRotation.x, m_RestRotation.y, m_RestRotation.z), m_Time, RotateMode.Fast);
        yield return rotEndTween.WaitForCompletion();

        m_IsInRestPos = true;
    }
}
