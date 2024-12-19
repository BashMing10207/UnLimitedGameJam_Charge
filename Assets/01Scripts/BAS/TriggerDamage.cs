using UnityEngine;

public class TriggerDamage : MonoBehaviour
{
    [SerializeField] private float _damage;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out IDamageable target))
        {
            target.ApplyDamage(_damage);
        }
    }
}
