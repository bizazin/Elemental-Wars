using UI.FX.Core;
using UnityEngine;

namespace UI.FX.SkillParticles.Views
{
    public class ExplosionDefaultFxView : FxView
    {
        [SerializeField] protected ParticleSystem explosionParticle;
        [SerializeField] protected ParticleSystem[] allParticles;
        [SerializeField] protected float explosionDuration;
        [SerializeField] protected float[] damageTimeIntervals;

        public ParticleSystem ExplosionParticle => explosionParticle;

        public ParticleSystem[] AllParticles => allParticles;

        public float ExplosionDuration => explosionDuration;

        public float[] DamageTimeIntervals => damageTimeIntervals;

        public override void Play() => explosionParticle.Play();

        public override void Stop()
        {
            foreach (var particle in allParticles) 
                particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }
}