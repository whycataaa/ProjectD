using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MY_Framework.Singleton;

namespace MY_Framework.Res
{
/// <summary>
/// AB包资源管理器，外部更方便资源加载
/// </summary>
public class ABMgr : SingletonMono<ABMgr>
{
    //主包
    private AssetBundle mainAB;

    //依赖包获取用的配置文件
    private AssetBundleManifest manifest;

    //AB包不能重复加载，否则会报错
    //字典存储加载过的AB包
    private Dictionary<string,AssetBundle> abDic=new Dictionary<string, AssetBundle>();

    //AB包存放的路径
    private string pathUrl
    {
        get
        {
#if     UNITY_IOS
            return "file://"+Application.dataPath+"/";
#elif   UNITY_ANDROID
            return Application.streamingAssetsPath+"/";
#else
            return Application.streamingAssetsPath+"/";
#endif
        }
    }

    //根据平台决定主包名
    private string mainABName
    {
        get
        {
#if     UNITY_IOS
            return "IOS";
#elif   UNITY_ANDROID
            return "Android";
#else
            return "PC";
#endif
        }
    }

    #region 同步加载
    /// <summary>
    /// 根据AB包名和资源名加载
    /// </summary>
    /// <param name="abName">AB包名</param>
    /// <param name="resName">资源名</param>
    /// <returns></returns>
    public Object LoadRes(string abName,string resName)
    {
        LoadAB(abName);
        //加载资源
        return abDic[abName].LoadAsset(resName);
    }

    /// <summary>
    /// 根据资源类型加载
    /// </summary>
    /// <param name="abName">AB包名</param>
    /// <param name="resName">资源名</param>
    /// <param name="type">资源类型</param>
    /// <returns></returns>
    public Object LoadRes(string abName,string resName,System.Type type)
    {
        LoadAB(abName);
        return abDic[abName].LoadAsset(resName,type);
    }

    /// <summary>
    /// 泛型加载
    /// </summary>
    /// <param name="abName"></param>
    /// <param name="resName"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public T LoadRes<T>(string abName,string resName)where T:Object
    {
        LoadAB(abName);
        return abDic[abName].LoadAsset<T>(resName);
    }

    public void LoadAB(string abName)
    {

        //加载主包
        if (mainAB == null)
        {
            mainAB = AssetBundle.LoadFromFile(pathUrl + mainABName);
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            Debug.Log(mainAB);
            Debug.Log(manifest);
        }

        //获取依赖包信息并加载
        string[] strs = manifest.GetAllDependencies(abName);
        AssetBundle ab = null;
        for (int i = 0; i < strs.Length; i++)
        {
            if (!abDic.ContainsKey(strs[i]))
            {
                ab = AssetBundle.LoadFromFile(pathUrl + strs[i]);
                abDic.Add(strs[i], ab);
            }
        }
        //加载目标AB包
        if (!abDic.ContainsKey(abName))
        {
            ab = AssetBundle.LoadFromFile(pathUrl + abName);
            abDic.Add(abName,ab);
        }
    }

    #endregion

    #region 异步加载

    public void LoadResAsync(string abName,string resName,UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName,resName,callBack));
    }

    public void LoadResAsync(string abName,string resName,System.Type type,UnityAction<Object> callBack)
    {
        StartCoroutine(ReallyLoadResAsync(abName,resName,type,callBack));
    }

    public void LoadResAsync<T>(string abName,string resName,UnityAction<Object> callBack)where T:Object
    {
        StartCoroutine(ReallyLoadResAsync<T>(abName,resName,callBack));
    }

    private IEnumerator ReallyLoadResAsync(string abName,string resName,UnityAction<Object> callBack)
    {
        LoadAB(abName);
        AssetBundleRequest abRequest=abDic[abName].LoadAssetAsync(resName);
        yield return abRequest;
        callBack(abRequest.asset);
    }

    private IEnumerator ReallyLoadResAsync(string abName,string resName,System.Type type,UnityAction<Object> callBack)
    {
        LoadAB(abName);
        AssetBundleRequest abRequest=abDic[abName].LoadAssetAsync(resName,type);
        yield return abRequest;
        callBack(abRequest.asset);
    }

    private IEnumerator ReallyLoadResAsync<T>(string abName,string resName,UnityAction<Object> callBack)where T:Object
    {
        LoadAB(abName);
        AssetBundleRequest abRequest=abDic[abName].LoadAssetAsync<T>(resName);
        yield return abRequest;
        callBack(abRequest.asset as T);
    }

    #endregion


    /// <summary>
    /// 单个包卸载
    /// </summary>
    /// <param name="abName">AB包名</param>
    public void UnLoadAB(string abName)
    {
        abDic[abName].Unload(false);
        abDic.Remove(abName);
    }



    /// <summary>
    /// 卸载所有AB包
    /// </summary>
    public void ClearAllAB()
    {
        AssetBundle.UnloadAllAssetBundles(false);
        abDic.Clear();
        mainAB=null;
        manifest=null;
    }
}
}