using UnityEngine;

namespace UI.FX.SkillParticles.Views
{
    public class FlyExplosionFxView : ExplosionDefaultFxView
    {
        [SerializeField] protected ParticleSystem attackParticle;
        [SerializeField] private float flyDuration;
        public ParticleSystem AttackParticle => attackParticle;
        public float FlyDuration => flyDuration;
    }
}