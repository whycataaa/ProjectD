using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformHelper
{
    /// <summary>
    /// 查找子物体（递归查找）  where T : UnityEngine.Object
    /// </summary>
    /// <param name="trans">父物体</param>
    /// <param name="targetName">子物体的名称</param>
    /// <returns>找到的相应子物体</returns>
    public static T FindDeepTransform<T>(this Transform trans, string targetName) where T : Component
    {
        Transform child = trans.Find(targetName);
        T t = null;
        if (child != null)
        {
            t = child.GetComponent<T>();
            return t;
        }
        for (int i = 0; i < trans.childCount; i++)
        {
            Transform parent = FindDeepTransform<Transform>(trans.GetChild(i), targetName);
            if (parent != null)
            {
                t = parent.GetComponent<T>();
                return t;
            }
        }
        return t;
    }

    /// <summary>
    /// 查找所有需要类型的子物体（具体查找逻辑交给单独的递归方法）
    /// </summary>
    /// <typeparam name="T">需要查找的类型</typeparam>
    /// <param name="trans">父物体</param>
    /// <param name="targetName">查找的组件对应的名字</param>
    /// <returns>返回所有名字为targetName并且包含查找组件的组件</returns>
    public static List<T> FindDeepTransforms<T>(this Transform trans, string targetName) where T : Component
    {
        List<T> t = new List<T>();

        Find(trans, targetName, ref t);

        return t;
    }
    /// <summary>
    /// 以递归的方式查找子物体，将指定类型的组件保存起来
    /// </summary>
    /// <typeparam name="T">需要查找的类型</typeparam>
    /// <param name="trans">父物体</param>
    /// <param name="targetName">查找的组件对应的名字</param>
    /// <param name="t">找到相应物体</param>
    private static void Find<T>(Transform trans, string targetName, ref List<T> t) where T : Component
    {
        int number = trans.childCount;
        while (number > 0)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                if (trans.GetChild(i).name == targetName)
                {
                    if (trans.GetComponent<T>())
                        t.Add(trans.GetChild(i).GetComponent<T>());
                }
                if (trans.GetChild(i).childCount > 0) Find<T>(trans.GetChild(i), targetName, ref t);
                number--;
            }
        }
    }
}

