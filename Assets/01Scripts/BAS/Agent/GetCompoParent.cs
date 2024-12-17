using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

    public abstract class GetCompoParent : MonoBehaviour
    {
        protected Dictionary<Type, IGetCompoable> _components;
        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IGetCompoable>();
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInitialize();
        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IGetCompoable>(true)
                .ToList().ForEach(component => _components.Add(component.GetType(), component));
        }

        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(component => component.Initialize(this));
        }

        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInitable>()
                .ToList().ForEach(afterInitable => afterInitable.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : IGetCompoable
        {
            if (_components.TryGetValue(typeof(T), out var component))
            {
                return (T)component;
            }

            if (isDerived == false) return default;

            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_components[findType];

            return default;
        }

    }


