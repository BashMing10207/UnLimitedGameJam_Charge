using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private LayerMask _whatisTarget;

    [SerializeField]
    private Transform _laserBody, _laserHit;
    //[SerializeField]
    //private ParticleSystem _laserBodyParticle;

    void Update()
    {
        Vector2 resultPos = transform.position + transform.up*999;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, 999, _whatisTarget);

        if(hit)
        {
            if(hit.transform.TryGetComponent<IDamageable>(out IDamageable target))
            {
                //target.ApplyDamage();
            }

            resultPos = hit.point;
        }

        float distance = Vector2.Distance(transform.position, resultPos);

        _laserBody.position = ((Vector2)transform.position + resultPos)/2;
        _laserHit.position = resultPos;
        _laserBody.up = transform.up;
        _laserBody.localScale = new Vector3(_laserBody.localScale.x, distance, _laserBody.localScale.z);

        //var emissionModule = _laserBodyParticle.emission;
        //var trailModule = _laserBodyParticle.trails;
        //emissionModule.enabled = true;
        //emissionModule.rateOverTime = distance + 5;
        //trailModule.ratio = (int)distance;
    }
}
