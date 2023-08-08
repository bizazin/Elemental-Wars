using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Services.ExecutionStage;
using Services.Sounds;
using Services.Sounds.Core;
using UI.Battle.Battleground.Views;
using UI.Battle.Battleground.Views.Items;
using UI.Battle.Units.Views;
using UI.FX.SkillParticles.Views;
using UnityEngine;
using Utils;

namespace UI.FX.SkillParticles.Strategies
{
    public abstract class ExplosionDefaultFxStrategy : BlinkOnSkillHitStrategy
    {
       	private readonly ISoundService _skillSoundService;
        private ExplosionDefaultFxView _clone;
        private float _durationMultiplier;

        protected ExplosionDefaultFxStrategy
        (
            IExecutionStepSwitchService executionStepSwitchService,
            ISkillSoundService skillSoundService
        ) : base(executionStepSwitchService, skillSoundService)
        {
        }

        protected override void Activate(UnitItemView playerUnit, List<UnitPlaceItemView> enemyUnits, float duration,
            BattlegroundView view, bool isPlayerAttack)
        {
            _clone = Instantiate(view.SkillParent);
            if (enemyUnits.Count != 1)
                throw new Exception($"[{nameof(FlyExplosionFxStrategy)}] Must be only one enemy unit");
            var enemyUnit = enemyUnits[0].UnitItemView;

            var explosionParticleTransform = _clone.ExplosionParticle.transform;
            explosionParticleTransform.position = enemyUnit.ExplosionPosition.position;

            _durationMultiplier = duration / _clone.ExplosionDuration;
            _clone.AllParticles.AdjustSpeed(1 / _durationMultiplier);
            var damageIntervals = _clone.DamageTimeIntervals
                .Select(interval => interval * _durationMultiplier)
                .ToArray();
            var timeDifferences = CalculateTimeDifferences(damageIntervals);

            DOTween.Sequence()
                .AppendCallback(() =>
                {
                    ExecuteDamageHighlight(enemyUnit.UnitUid, timeDifferences);
                    _clone.Play();
                })
                .AppendInterval(duration)
                .AppendCallback(() =>
                {
                    if (IsCasting)
                        StopCast();
                });
        }

        protected override void ClearAfterCast()
        {
            _clone.Stop();
            _clone.AllParticles.AdjustSpeed(_durationMultiplier);
            Destroy(_clone);
        }

        protected abstract ExplosionDefaultFxView Instantiate(Transform parent);
        protected abstract void Destroy(ExplosionDefaultFxView clone);
    }
}