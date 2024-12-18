using DG.Tweening;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private LayerMask _whatisTarget;
    [SerializeField] private Transform _laserBody, _laserHit;

    private Sequence sequence;
    private Transform player = null; // 플레이어 참조

    private void Start()
    {
        sequence = DOTween.Sequence();

        sequence.Append(transform
            .DORotate(new Vector3(0, 0, 90), 4, RotateMode.FastBeyond360));
        
        sequence.SetLoops(-1, LoopType.Yoyo); 
    }

    private void OnEnable()
    {
        player = null;
        transform.rotation = Quaternion.Euler(0, 0, -90);
        sequence.Restart();
    }

    void Update()
    {
        Vector2 resultPos = transform.position + transform.up * 100;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 100, _whatisTarget);

        if (hit)
        {
            if (sequence.IsPlaying())
                sequence.Pause();

            if (player == null && hit.transform != null)
                player = hit.transform;

            if (hit.transform.TryGetComponent<IDamageable>(out IDamageable target))
            {
                // target.ApplyDamage();
            }
            
            resultPos = hit.point;
        }

        if (player != null)
        {
            Vector2 directionToPlayer = (player.position - transform.position).normalized;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            
            Quaternion targetRot = Quaternion.Euler(0, 0, angle - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, Time.deltaTime * 5);
        }

        float distance = Vector2.Distance(transform.position, resultPos);

        _laserBody.up = (resultPos - (Vector2)transform.position).normalized;

        _laserBody.localScale = new Vector3(_laserBody.localScale.x, distance * 2, _laserBody.localScale.z);
        
        _laserHit.position = resultPos;
    }

}
