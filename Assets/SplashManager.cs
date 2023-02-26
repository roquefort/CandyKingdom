using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using RTLOL.Utilities;
using DG.Tweening;  

public class SplashManager : Singleton<SplashManager>
{

    [SerializeField] private DOTweenAnimation logoAnim;

    void Start()
    {
        var tween = logoAnim.GetTweens().FirstOrDefault();
        tween.OnComplete(TweenComplete);
    }

    private void TweenComplete()
    {
        Debug.Log("anim finish");
        SceneManager.LoadScene(1);
    }
}
