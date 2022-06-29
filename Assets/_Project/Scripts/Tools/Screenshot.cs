using UnityEngine;
using System.Collections.Generic;
using System;

public class Screenshot : MonoBehaviour
{
    [SerializeField]
    private string path;
    private string fileName;
    [SerializeField]
    [Range(1, 5)]
    private int size = 1;

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
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeShot();
        }
#endif
    }

    [ContextMenu("Take Screenshot")]
    private void TakeShot()
    {
        fileName = "screenshot ";
        fileName += System.Guid.NewGuid().ToString() + ".png";

        ScreenCapture.CaptureScreenshot(path + fileName, size);
    }
}