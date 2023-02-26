using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Canvas))]
public class CanvasHelper : MonoBehaviour
{
    public static UnityEvent onOrientationChange = new UnityEvent();
    public static UnityEvent onResolutionChange = new UnityEvent();
    private static bool IsLandscape { get; set; }

    private static readonly List<CanvasHelper> helpers = new List<CanvasHelper>();

    private static bool m_screenChangeVarsInitialized = false;
    private static ScreenOrientation m_lastOrientation = ScreenOrientation.Portrait;
    private static Vector2 m_lastResolution = Vector2.zero;
    private static Rect m_lastSafeArea = Rect.zero;

    private Canvas m_canvas;
    private RectTransform m_rectTransform;

    [SerializeField] private RectTransform safeAreaTransform;

    void Awake()
    {
        if (!helpers.Contains(this))
            helpers.Add(this);

        m_canvas = GetComponent<Canvas>();
        m_rectTransform = GetComponent<RectTransform>();

        safeAreaTransform = transform.Find("SafeArea") as RectTransform;

        if (!m_screenChangeVarsInitialized)
        {
            m_lastOrientation = Screen.orientation;
            m_lastResolution.x = Screen.width;
            m_lastResolution.y = Screen.height;
            m_lastSafeArea = Screen.safeArea;

            m_screenChangeVarsInitialized = true;
        }
    }

    void Start()
    {
        ApplySafeArea();
    }

    void Update()
    {
        if (helpers.Count == 0 || helpers[0] != this)
            return;

        if (Screen.orientation != m_lastOrientation)
            OrientationChanged();

        if (Screen.safeArea != m_lastSafeArea)
            SafeAreaChanged();

        if (!Mathf.Approximately(Screen.width,m_lastResolution.x) || !Mathf.Approximately(Screen.height, m_lastResolution.y))
            ResolutionChanged();
    }

    void ApplySafeArea()
    {
        if (safeAreaTransform == null)
            return;

        var safeArea = Screen.safeArea;

        var anchorMin = safeArea.position;
        var anchorMax = safeArea.position + safeArea.size;
        var pixelRect = m_canvas.pixelRect;
        anchorMin.x /= pixelRect.width;
        anchorMin.y /= pixelRect.height;
        anchorMax.x /= pixelRect.width;
        anchorMax.y /= pixelRect.height;

        safeAreaTransform.anchorMin = anchorMin;
        safeAreaTransform.anchorMax = anchorMax;

        // Debug.Log(
        // "ApplySafeArea:" +
        // "\n Screen.orientation: " + Screen.orientation +
        // #if UNITY_IOS
        // "\n Device.generation: " + UnityEngine.iOS.Device.generation.ToString() +
        // #endif
        // "\n Screen.safeArea.position: " + Screen.safeArea.position.ToString() +
        // "\n Screen.safeArea.size: " + Screen.safeArea.size.ToString() +
        // "\n Screen.width / height: (" + Screen.width.ToString() + ", " + Screen.height.ToString() + ")" +
        // "\n canvas.pixelRect.size: " + canvas.pixelRect.size.ToString() +
        // "\n anchorMin: " + anchorMin.ToString() +
        // "\n anchorMax: " + anchorMax.ToString());
    }

    void OnDestroy()
    {
        if (helpers != null && helpers.Contains(this))
            helpers.Remove(this);
    }

    private static void OrientationChanged()
    {
        //GlobalLogger.Log("Orientation changed from " + lastOrientation + " to " + Screen.orientation + " at " + Time.time);

        m_lastOrientation = Screen.orientation;
        m_lastResolution.x = Screen.width;
        m_lastResolution.y = Screen.height;

        IsLandscape = m_lastOrientation == ScreenOrientation.LandscapeLeft || m_lastOrientation == ScreenOrientation.LandscapeRight || m_lastOrientation == ScreenOrientation.LandscapeLeft;
        onOrientationChange.Invoke();
    }

    private static void ResolutionChanged()
    {
        if (Mathf.Approximately(m_lastResolution.x, Screen.width) && Mathf.Approximately(m_lastResolution.y, Screen.height))
            return;

        //Debug.Log("Resolution changed from " + lastResolution + " to (" + Screen.width + ", " + Screen.height + ") at " + Time.time);

        m_lastResolution.x = Screen.width;
        m_lastResolution.y = Screen.height;

        IsLandscape = Screen.width > Screen.height;
        onResolutionChange.Invoke();
    }

    private static void SafeAreaChanged()
    {
        if (m_lastSafeArea == Screen.safeArea)
            return;

        //Debug.Log("Safe Area changed from " + lastSafeArea + " to " + Screen.safeArea.size + " at " + Time.time);

        m_lastSafeArea = Screen.safeArea;

        for (int i = 0; i < helpers.Count; i++)
        {
            helpers[i].ApplySafeArea();
        }
    }

    private static Vector2 GetCanvasSize()
    {
        return helpers[0].m_rectTransform.sizeDelta;
    }

    public static Vector2 GetSafeAreaSize()
    {
        foreach (var helper in helpers)
        {
            if (helper.safeAreaTransform != null)
            {
                return helper.safeAreaTransform.sizeDelta;
            }
        }

        return GetCanvasSize();
    }
}