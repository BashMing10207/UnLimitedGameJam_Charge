using UnityEngine;
using UnityEngine.UI;

public class DashSetting : MonoBehaviour
{
    [SerializeField] private PlayerManagerSO _playerManagerSo;
    [SerializeField] private Image _check;
    private bool _isMouseDash = false;

    public void Select()
    {
        _isMouseDash = !_isMouseDash;
        _check.gameObject.SetActive(_isMouseDash);
        
        if (_isMouseDash == true)
        {
            _playerManagerSo.Player.dashType = PlayerDashType.MouseDir;
        }
        else
        {
            _playerManagerSo.Player.dashType = PlayerDashType.InputDir;
        }
    }
}
