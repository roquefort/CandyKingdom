using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScreenCaptureHandler : MonoBehaviour
{
    public static ScreenCaptureHandler m_instance;
   
    private string m_screenshotPath;

    [SerializeField][Tooltip("Factor by which to increase resolution. - Unity Docs")] private int m_superSize;

    private void Awake()
    {
        m_screenshotPath = Application.dataPath + "/Screenshots/";
        m_instance = this;
    }

    public void CaptureScreenshot()
    {
        DirectoryInfo dir = new DirectoryInfo(m_screenshotPath);
        ScreenCapture.CaptureScreenshot(m_screenshotPath + "Screenshot_" + dir.GetFiles("*.png").Length + ".png", m_superSize);
    }
}
