using UnityEngine;
using UnityEngine.SceneManagement;
using RTLOL.Utilities;

public class MenuControls : Singleton<MenuControls>
{
    public void OnPlayButtonPressed()
    {
        Debug.Log("Loading Game Scene");
        SceneManager.LoadScene(2);
    }
}
