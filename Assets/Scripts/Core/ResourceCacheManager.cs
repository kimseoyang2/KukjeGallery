using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using System.IO;
using UnityEngine.Networking;

public class ResourceCacheManager : SingletonBehavior<ResourceCacheManager>
{
    public TempTexturesDic texturesDic;
    public OriginalTexturesUriDic originalTexturesUriDic;
    public OriginalTexturesDic originalTextures;

    private void Start ()
    {
        StartCoroutine(SetOriginalTextures());
    }

    private IEnumerator SetOriginalTextures()
    {
        string url;

        for(int i = 0; i < originalTexturesUriDic.Keys.Count; i++)
        {
            string fileName = originalTexturesUriDic[(TextureType)i];
            url = Path.Combine(Application.streamingAssetsPath, fileName);

            byte[] imgData;
            Texture2D tex = new Texture2D(2, 2);

            //Check if we should use UnityWebRequest or File.ReadAllBytes
            if (url.Contains("://") || url.Contains(":///"))
            {
                UnityWebRequest www = UnityWebRequest.Get(url);
                yield return www.SendWebRequest();
                imgData = www.downloadHandler.data;
            }
            else
            {
                imgData = File.ReadAllBytes(url);
            }
            Debug.Log(imgData.Length);

            //Load raw Data into Texture2D 
            tex.LoadImage(imgData);

            originalTextures.Add((TextureType)i, tex);
        }
    }
}

[Serializable]
public class TempTexturesDic : SerializableDictionaryBase<TextureType, Texture> { }

[Serializable]
public class OriginalTexturesUriDic : SerializableDictionaryBase<TextureType, string> { }

[Serializable]
public class OriginalTexturesDic : SerializableDictionaryBase<TextureType, Texture> { }

public enum TextureType { test01, test02 }
