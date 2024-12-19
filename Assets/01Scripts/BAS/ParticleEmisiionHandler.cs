using UnityEngine;

public class ParticleEmisiionHandler : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    public void ChangeEmission(float aa)
    {
        ParticleSystem.EmissionModule emission = _particleSystem.emission;

        emission.rateOverTime = aa;
    }
}
