using System;
using UnityEngine;
using System.Collections;
using System.IO;

public class CachingLoadExample : MonoBehaviour
{
    public string BundleURL;
    public string AssetName;
    public int version;
    public int pesen;
    void Start()
    {
        StartCoroutine(DownloadAndCache());
    }
    
    public WWW www;
    string tempText, guitext;
    bool downloadComplete=false;
    void Update()
    {

        // progress
        if (!downloadComplete) {
            int percent = (int)(www.progress * 100);

            guitext = percent.ToString() + "%";
            if (!guitext.Equals(tempText))
            {
                Debug.Log(guitext);
                tempText = guitext;

            }
            
        }
        

    }
    public string saveTo = ".";
    IEnumerator DownloadAndCache()
    {
        // Wait for the Caching system to be ready
        while (!Caching.ready)
            yield return null;
        

        // Load the AssetBundle file from Cache if it exists with the same version or download and store it in the cache
       using ( www = WWW.LoadFromCacheOrDownload(BundleURL+"", version))
        {
            yield return www;

            downloadComplete = true;
            AssetBundle bundle;
            if (!string.IsNullOrEmpty(www.error))
            {
              
                throw new Exception("WWW download had an error:" + www.error);

            }

            else
            {
                bundle = www.assetBundle;
                Debug.Log(bundle.name);


                for (int i = 0; i < bundle.GetAllAssetNames().Length; i++)
                {
                    Debug.Log(bundle.GetAllAssetNames()[i]);
                }

                if (AssetName == "")
                    Instantiate(bundle.mainAsset);
                else
                    Instantiate(bundle.LoadAsset(AssetName));
                // Unload the AssetBundles compressed contents to conserve memory
                Debug.Log(bundle.name);
                bundle.Unload(false);
            }
          

} // memory is freed from the web stream (www.Dispose() gets called implicitly)
    }
}
