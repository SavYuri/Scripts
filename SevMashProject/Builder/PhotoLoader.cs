using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;


public static class PhotoLoader 
{
    public static IEnumerator LoadImage(Image image, string fileName, string path)
    {
        fileName += ".png";
        string filePath = Path.Combine(Application.streamingAssetsPath, path, fileName);
        byte[] pngBytes = System.IO.File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        yield return texture.LoadImage(pngBytes);
        image.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
