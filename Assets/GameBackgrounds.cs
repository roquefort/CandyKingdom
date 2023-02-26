using System.Collections.Generic;
using UnityEngine;

public class GameBackgrounds : MonoBehaviour
{
    public List<GameObject> backgrounds = new();

    void Start()
    {
        var indexToShow = Random.Range(0, backgrounds.Count);
        for (int i = 0; i < backgrounds.Count; i++)
        {
            backgrounds[i].gameObject.SetActive(i == indexToShow);
        }
    }
}
