using System.Collections;
using UnityEngine;

public class SandeVistanRenderer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _targetSpriteRenderer, _prefab;
    [SerializeField]
    private float _lifeTime = 0.5f;
    private float _duration = 0f;

    public void SetDuration(float duration)
    {
        _duration = duration; 
    }

    private void FixedUpdate()
    {
        if (_duration > 0f)
        {
            _duration -= Time.fixedDeltaTime;
            Instantiate(_prefab, transform.position, transform.rotation).sprite = _targetSpriteRenderer.sprite; //������ Ư: Instatiate�� ���� Ȱ����.
        }
    }

    private IEnumerator DelayedDelete(GameObject prefab)
    {
        yield return new WaitForSeconds(_lifeTime);
        Destroy(prefab );
    }
}
