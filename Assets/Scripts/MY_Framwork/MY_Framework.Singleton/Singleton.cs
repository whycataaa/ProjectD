using System;


namespace MY_Framework.Singleton
{
    /// <summary>
    /// 不继承Mono的泛型单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T :Singleton<T>
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if(instance==null)
                {
                    instance=Activator.CreateInstance(typeof(T),true) as T;
                    instance.Init();
                }
                return instance;
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual void Init()
        {

        }
    }

}