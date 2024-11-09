using System.Collections.Generic;
using UnityEngine;
using MY_Framework.Singleton;
using MY_Framework.Res;

namespace MY_Framework.UI
{
    /// <summary>
    /// 在UI实例化时加入字典，销毁时移出字典
    /// </summary>
    public class UIManager:Singleton<UIManager>
    {
        //UI根节点
        GameObject UIRoot;


        public Dictionary<string,BasePanel> panelDic;
        public Dictionary<string,GameObject> prefabDic;
        private Dictionary<string,string> pathDic;
        protected override void Init()
        {
            panelDic=new Dictionary<string, BasePanel>();
            prefabDic=new Dictionary<string, GameObject>();
            pathDic=new Dictionary<string, string>()
            {
                {UIConst.StartPanel,"Assets/AssetPackage/Prefab/UIPanel/StartPanel"},
                {UIConst.LoginPanel,"Assets/AssetPackage/Prefab/UIPanel/LoginPanel"},
                {UIConst.RegisterPanel,"Assets/AssetPackage/Prefab/UIPanel/RegisterPanel"},
                {UIConst.TipsPanel,"Assets/AssetPackage/Prefab/UIPanel/TipsPanel"},
                {UIConst.DressingPanel,"Assets/AssetPackage/Prefab/UIPanel/DressingPanel"},
                {UIConst.SavingPanel,"Assets/AssetPackage/Prefab/UIPanel/SavingPanel"}

            };
            UIRoot=GameObject.Find("UIRoot");
        }

        public BasePanel OpenPanel(string name)
        {
            BasePanel panel=null;
            if(panelDic.TryGetValue(name,out panel))
            {
                Debug.Log("界面已打开"+name);
                return null;
            }

            string path="";
            if(!pathDic.TryGetValue(name,out path))
            {
                Debug.LogError("没有找到"+name+"的UI路径");
                return null;
            }

            GameObject panelPrefab=null;
            if(!prefabDic.TryGetValue(name,out panelPrefab))
            {
                string rPath=PathUtils.UI_PREFAB_PATH+name+".prefab";
                panelPrefab=ResManager.Instance.LoadResource<GameObject>(rPath,PathUtils.UI_AB_PATH,name+".prefab");
                prefabDic.Add(name,panelPrefab);
            }

            GameObject panelGo=GameObject.Instantiate(panelPrefab,UIRoot.transform);
            panelGo.name=name;
            panel=panelGo.GetComponent<BasePanel>();
            panelDic.Add(name,panel);
            panel.OpenPanel(name);
            return panel;
        }

        public bool ClosePanel(string name)
        {
            BasePanel panel=null;
            if(!panelDic.TryGetValue(name,out panel))
            {
                Debug.Log("界面未打开"+name);
                return false;
            }

            panel.ClosePanel();
            return true;
        }




}

public static class UIConst
{
    public const string StartPanel="StartPanel";
    public const string LoginPanel="LoginPanel";
    public const string RegisterPanel="RegisterPanel";
    public const string TipsPanel="TipsPanel";
    public const string DressingPanel="DressingPanel";
    public const string SavingPanel="SavingPanel";
}
}