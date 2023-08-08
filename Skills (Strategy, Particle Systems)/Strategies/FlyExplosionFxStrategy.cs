using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Services.ExecutionStage;
using Services.Sounds;
using UI.Battle.Battleground.Views;
using UI.Battle.Battleground.Views.Items;
using UI.Battle.Units.Views;
using UI.FX.SkillParticles.Views;
using UnityEngine;
using Utils;

namespace UI.FX.SkillParticles.Strategies
{
    public abstract class FlyExplosionFxStrategy : BlinkOnSkillHitStrategy
    {
        private FlyExplosionFxView _clone;
        private float _durationMultiplier;

        protected FlyExplosionFxStrategy
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
            var startPos = playerUnit.CastSkillPosition.position;
            var endPos = enemyUnit.ExplosionPosition.position;
            _clone.ExplosionParticle.transform.position = endPos;
            Transform attackParticleTransform;
            (attackParticleTransform = _clone.AttackParticle.transform).position = startPos;
            _durationMultiplier = duration / _clone.ExplosionDuration;
            _clone.AllParticles.AdjustSpeed(1 / _durationMultiplier);

            var direction = (endPos - startPos).normalized;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            attackParticleTransform.rotation = Quaternion.Euler(0, 0, 180 + angle);

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
                .AppendInterval(_clone.ExplosionDuration * _durationMultiplier)
                .Join(attackParticleTransform.DOMove(endPos, _clone.FlyDuration * _durationMultiplier))
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

        protected abstract FlyExplosionFxView Instantiate(Transform parent);
        protected abstract void Destroy(FlyExplosionFxView clone);
    }
}