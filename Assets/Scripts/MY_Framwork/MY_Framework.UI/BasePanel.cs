using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MY_Framework.UI
{
    /// <summary>
    /// 所有UI面板的父类
    /// </summary>
    public abstract class BasePanel:MonoBehaviour
    {
        public GameObject PanelGo;
        protected new string name;
        public bool IsActive
        {
            get
            {
                return PanelGo.activeSelf;
            }
        }


        public virtual void OpenPanel(string _name)
        {
            PanelGo=this.gameObject;
            name=_name;
            gameObject.SetActive(true);
        }

        public virtual void ClosePanel()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
            if(UIManager.Instance.panelDic.ContainsKey(name))
            {
                UIManager.Instance.panelDic.Remove(name);
            }
        }
    }
}
