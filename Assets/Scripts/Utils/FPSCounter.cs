using UnityEngine;
using TMPro;

namespace RTLOL.Utilities
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_text;

        private int m_fpsAccumulator = 0;
        private int m_currentFps;
        private float m_fpsNextPeriod = 0;

        private const float s_fpsMeasurePeriod = 0.5f;
        private const string s_display = "{0} fps";


        private void Start()
        {
            m_fpsNextPeriod = Time.realtimeSinceStartup + s_fpsMeasurePeriod;
        }


        private void Update()
        {
            // measure average frames per second
            m_fpsAccumulator++;
            if (Time.realtimeSinceStartup > m_fpsNextPeriod)
            {
                m_currentFps = (int)(m_fpsAccumulator / s_fpsMeasurePeriod);
                m_fpsAccumulator = 0;
                m_fpsNextPeriod += s_fpsMeasurePeriod;
                m_text.text = string.Format(s_display, m_currentFps);
            }

            if (m_currentFps > 30)
                m_text.color = Color.green;
            else if (m_currentFps > 15)
                m_text.color = Color.cyan;
            else
                m_text.color = Color.red;
        }
    }
}