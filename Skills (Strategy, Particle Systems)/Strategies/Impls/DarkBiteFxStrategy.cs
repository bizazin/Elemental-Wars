using Enums.Skill;
using Services.ExecutionStage;
using Services.Sounds;
using UI.FX.Pools;
using UI.FX.SkillParticles.Views;
using UnityEngine;

namespace UI.FX.SkillParticles.Strategies.Impls
{
    public class DarkBiteFxStrategy : ExplosionDefaultFxStrategy
    {
        private readonly IDarkBiteFxPool _darkBiteFxPool;

        public override EMagicSpells Spell => EMagicSpells.DarkBite;
        
        public DarkBiteFxStrategy
		(
            IDarkBiteFxPool darkBiteFxPool,
            IExecutionStepSwitchService executionStepSwitchService,
            ISkillSoundService skillSoundService
        ) : base(executionStepSwitchService, skillSoundService) =>
            _darkBiteFxPool = darkBiteFxPool;

        protected override ExplosionDefaultFxView Instantiate(Transform parent) => _darkBiteFxPool.Spawn(parent);

        protected override void Destroy(ExplosionDefaultFxView clone) => _darkBiteFxPool.Despawn(clone);
    }
}