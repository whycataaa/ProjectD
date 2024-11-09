using UnityEngine;

namespace MY_Framework.Singleton
{
    /// <summary>
    /// 泛型单例（在Awake函数中创建实例）
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMono <T> : MonoBehaviour where T : SingletonMono<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if(instance==null)
                {
                    instance=FindObjectOfType<T>();
                    if(instance==null)
                    {
                        GameObject go=new GameObject(typeof(T).Name);
                        instance=go.AddComponent<T>();
                    }
                }
                return instance;
            }
        }
        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance=this as T;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

}