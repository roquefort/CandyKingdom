using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteExtensions
{
    public static void ResizeSpriteToScreen(GameObject theSprite, Camera theCamera, int fitToScreenWidth, int fitToScreenHeight)
    {
        SpriteRenderer sr = theSprite.GetComponent<SpriteRenderer>();

        theSprite.transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = (float)(theCamera.orthographicSize * 2.0);
        float worldScreenWidth = (float)(worldScreenHeight / Screen.height * Screen.width);

        if (fitToScreenWidth != 0)
        {
            Vector2 sizeX = new Vector2(worldScreenWidth / width / fitToScreenWidth, theSprite.transform.localScale.y);
            theSprite.transform.localScale = sizeX;
        }

        if (fitToScreenHeight != 0)
        {
            Vector2 sizeY = new Vector2(theSprite.transform.localScale.x, worldScreenHeight / height / fitToScreenHeight);
            theSprite.transform.localScale = sizeY;
        }
    }

    public static Vector2 GetScreenDimensions(Camera camera)
    {
        if (camera != null)
        {
            float worldScreenHeight = (float)(camera.orthographicSize * 2.0);
            float worldScreenWidth = (float)(worldScreenHeight / Screen.height * Screen.width);

            return new Vector2(worldScreenWidth, worldScreenHeight);
        }
        else
        {
            Debug.LogError("error - invalid camera");
            return Vector2.zero;
        }

    }
}