using UnityEngine;
using DG.Tweening;

public static class CanvasGroupExtensions
{
    public static void SetActive(this CanvasGroup group, bool active)
    {
        group.alpha = active ? 1f : 0f;
        group.interactable = active;
        group.blocksRaycasts = active;
    }

    public static void ToggleVisibility(this CanvasGroup group, bool isVisible, float tweenTime = 0f)
    {
        if (tweenTime <= 0f || Mathf.Approximately(tweenTime, 0f))
        {
            group.alpha = isVisible ? 1f : 0f;
            group.blocksRaycasts = isVisible;
            group.interactable = isVisible;
        }
        else
        {
            if (!isVisible)
            {
                group.blocksRaycasts = isVisible;
                group.interactable = isVisible;
            }

            group.DOFade(isVisible ? 1f : 0f, tweenTime).OnComplete(() =>
            {
                if (isVisible)
                {
                    group.blocksRaycasts = isVisible;
                    group.interactable = isVisible;
                }
            });
        }
    }
}