using UnityEngine;

    public abstract class Manager<T> : GetCompoParent where T : Manager<T>
    {
        private static T _instance = null;
        public static T Instance
        {
            get
            {
                if (_instance is null) _instance = Initialize();
                return _instance;
            }
        }
        private static T Initialize()
        {
            //CreateInstance
            GameObject gameObject = new("Singleton_" + typeof(T).Name);
            T result = gameObject.AddComponent<T>();
            return result;
        }

        protected override void Awake()
        {
            base.Awake();
            if (_instance != null)
            {
                Debug.LogError("twoSingletons_" + typeof(T).Name);
                Destroy(gameObject);
                return;
            }
            print("-AwakeInit-" + typeof(T).Name + "_" + gameObject.name);
            _instance = this as T;
        }
        protected virtual void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
    }
