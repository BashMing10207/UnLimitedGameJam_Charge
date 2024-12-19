using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandeVistanRenderer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _targetSpriteRenderer, _prefab;
    private List<BashPair<SpriteRenderer,float>> _spriteLsit = new List<BashPair<SpriteRenderer,float>>();
    [SerializeField]
    private float _lifeTime = 0.5f;
    private float _duration = 0f;
    [SerializeField]
    private Gradient _gradient;

    public void SetDuration(float duration)
    {
        _duration = duration; 
    }

    private void FixedUpdate()
    {
        if (_duration > 0f)
        {
            _duration -= Time.fixedDeltaTime;
            SpriteRenderer sprite = Instantiate(_prefab, transform.position, transform.rotation); //������ Ư: Instatiate�� ���� Ȱ����.
            sprite.transform.localScale = transform.localScale;
            sprite.sprite = _targetSpriteRenderer.sprite;
            _spriteLsit.Add(new BashPair<SpriteRenderer,float>(sprite,_lifeTime));
        }
        if (_spriteLsit.Count > 0)
        {
            for (int i = 0; i < _spriteLsit.Count; i++)
            {
                _spriteLsit[i].First.color = _gradient.Evaluate(1-(_spriteLsit[i].Second/_lifeTime));
                _spriteLsit[i].Second -= Time.fixedDeltaTime;

                if (_spriteLsit[i].Second <= 0f)
                {
                    Destroy(_spriteLsit[i].First);
                    _spriteLsit.RemoveAt(i); 
                    i--; 
                }
            }
        }
    }

}
