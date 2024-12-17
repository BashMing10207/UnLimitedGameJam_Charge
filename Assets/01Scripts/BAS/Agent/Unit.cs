using System;
using UnityEngine;

    public class Unit : Agent
    {
        [SerializeField] private GameObject _isSelectedObj,_isDisabledObj;

    public event Action<bool> OnActiveChange;
    public event Action OnSelectedUpdate;
    public event Action OnDisSelectedUpdate;

        public void SelectVisual(bool enable)
        {
            _isSelectedObj.SetActive(enable);
            _isDisabledObj.SetActive(!enable);
            OnActiveChange?.Invoke(enable);
        }

        public void SelectedUpdate()
        {
            OnSelectedUpdate?.Invoke();
        }
        public void DisSelectedUpdate()
        {
            OnDisSelectedUpdate?.Invoke();
            //유닛AI돌려줄 업데이트.
        }
    }

