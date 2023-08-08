using Enums.Skill;
using Services.ExecutionStage;
using Services.Sounds;
using UI.FX.Pools;
using UI.FX.SkillParticles.Views;
using UnityEngine;

namespace UI.FX.SkillParticles.Strategies.Impls
{
    public class FireballFxStrategy : FlyExplosionFxStrategy
    {
        private readonly IFireballFxPool _fireballFxPool;
        private Quaternion _defaultRotation;

        public override EMagicSpells Spell => EMagicSpells.Fireball;

        public FireballFxStrategy
        (
            IFireballFxPool fireballFxPool,
            IExecutionStepSwitchService executionStepSwitchService,
            ISkillSoundService skillSoundService
        ) : base(executionStepSwitchService, skillSoundService)
            => _fireballFxPool = fireballFxPool;

        protected override FlyExplosionFxView Instantiate(Transform parent) => _fireballFxPool.Spawn(parent);

        protected override void Destroy(FlyExplosionFxView clone) => _fireballFxPool.Despawn(clone);
    }
}