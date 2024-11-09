
using MY_Framework.Singleton;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
#endif

namespace MY_Framework.Res
{

    public class ResManager : SingletonMono<ResManager>
    {

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="_name">资源在AssetPackage下的路径</param>
        /// <typeparam name="T">资源类型</typeparam>
        /// <returns></returns>
        public T LoadResource<T>(string _path,string _abName,string _resName) where T : UnityEngine.Object
        {

        #if UNITY_EDITOR
                if(AssetDatabase.LoadAssetAtPath<T>(_path)==null)
                {
                    Debug.Log("资源不存在"+_path);
                }
                return AssetDatabase.LoadAssetAtPath<T>(_path);
        #else
                return ABMgr.Instance.LoadRes<T>(_abName,_resName);
        #endif

        }
    }

}