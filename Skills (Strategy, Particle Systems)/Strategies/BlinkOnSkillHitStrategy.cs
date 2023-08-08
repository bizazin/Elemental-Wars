using System.Collections.Generic;
using DG.Tweening;
using Enums.Skill;
using Services.ExecutionStage;
using Services.Sounds;
using UI.Battle.Battleground.Views;
using UI.Battle.Battleground.Views.Items;
using UI.Battle.Units.Views;

namespace UI.FX.SkillParticles.Strategies
{
    public abstract class BlinkOnSkillHitStrategy : ISkillFxStrategy
    {
        private readonly IExecutionStepSwitchService _executionStepSwitchService;
        private readonly ISkillSoundService _skillSoundService;
        public abstract EMagicSpells Spell { get; }
        public bool IsCasting { get; private set; }

        protected BlinkOnSkillHitStrategy
        (
            IExecutionStepSwitchService executionStepSwitchService,
            ISkillSoundService skillSoundService
        )
        {
            _executionStepSwitchService = executionStepSwitchService;
            _skillSoundService = skillSoundService;
        }

        public void Cast(UnitItemView playerUnit, List<UnitPlaceItemView> enemies, float duration,
            BattlegroundView view, bool isPlayerAttack)
        {
            IsCasting = true;
            Activate(playerUnit, enemies, duration, view, isPlayerAttack);
        }

        public void StopCast()
        {
            IsCasting = false;
            ClearAfterCast();
        }

        protected abstract void Activate(UnitItemView playerUnit, List<UnitPlaceItemView> enemyUnits, float duration,
            BattlegroundView view, bool isPlayerAttack);

        protected abstract void ClearAfterCast();

        protected void ExecuteDamageHighlight(int targetUnitUid, List<float> timeDifferences)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() => _skillSoundService.PlaySkillSound(Spell, ESkillSoundType.Cast));
            for (var i = 0; i < timeDifferences.Count; i++)
            {
                var index = i;
                sequence.AppendInterval(timeDifferences[index]);
                sequence.AppendCallback(() =>
                {
                    _executionStepSwitchService.OnWaveSkill(targetUnitUid, index, timeDifferences.Count);
                    _skillSoundService.PlaySkillSound(Spell, ESkillSoundType.Damage);
                });
            }
        }

        protected List<float> CalculateTimeDifferences(float[] damageIntervals)
        {
            var timeDifferences = new List<float>();
            for (var i = 1; i < damageIntervals.Length; i++)
                timeDifferences.Add(damageIntervals[i] - damageIntervals[i - 1]);

            return timeDifferences;
        }
    }
}