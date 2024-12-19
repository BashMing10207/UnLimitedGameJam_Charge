using System.Threading.Tasks;
using Unity.Behavior;
using UnityEngine;

public class GolemBossAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator BossAnimator;
    [SerializeField] private BehaviorGraphAgent BossGraph;
    
    [SerializeField] private ParticleSystem[] explosion;
    [SerializeField] private GolemBoss GolemBoss;
    
    
    public async void StartExplosion()
    {
        foreach (var item in explosion)
        {
            item.Simulate(0);
            item.Play();
        }
        
        await Task.Delay(500); 
        
        for (int i = 0; i < explosion.Length; i += 3) 
        {
            for (int j = 0; j < 3 && i + j < explosion.Length; j++) 
            {
                explosion[i + j].Simulate(0);
                explosion[i + j].Play();
            }
            await Task.Delay(400); 
        }
    }

    public void SetDead()
    {
        BossAnimator.enabled = true;
        BossGraph.enabled = false;
        GolemBoss.ActiveLaser(false);
        
        BossAnimator.SetBool("Dead",true);
    }
    
}
