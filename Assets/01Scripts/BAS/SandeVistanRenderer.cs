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
            SpriteRenderer sprite = Instantiate(_prefab, transform.position, transform.rotation); //게임잼 특: Instatiate를 적극 활용함.
            sprite.sprite = _targetSpriteRenderer.sprite;
            _spriteLsit.Add(new BashPair<SpriteRenderer,float>(sprite,_lifeTime));
        }

        if (_spriteLsit.Count > 0)
        {
            foreach(BashPair<SpriteRenderer, float> sprit in _spriteLsit)
            {
                sprit.First.color = _gradient.Evaluate(sprit.Second);
                sprit.Second -= Time.fixedDeltaTime;

                if (sprit.Second < 0f)
                {
                    Destroy(sprit.First);
                    _spriteLsit.Remove(sprit);
                }
            }
        }
    }

}
