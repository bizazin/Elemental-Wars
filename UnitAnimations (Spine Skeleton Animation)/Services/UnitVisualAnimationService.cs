using Databases.Animations;
using Enums.Hero;
using UI.Battle.Units.Views;
using UnityEngine;

namespace Services.Battle.Impls
{
    public class UnitVisualAnimationService : IUnitVisualAnimationService
    {
        private readonly IUnitSpineAnimationService _unitSpineAnimationService;

        public UnitVisualAnimationService(IUnitSpineAnimationService unitSpineAnimationService) =>
            _unitSpineAnimationService = unitSpineAnimationService;

        public void TakeDamage(UnitItemView unit, Color blinkColor, float blinkDuration)
        {
            unit.Blink(blinkColor, blinkDuration, unit.HealthBarImage.color);

            _unitSpineAnimationService.PlayAnimation(unit, EUnitAnimationType.Hurt, false);
        }

        public void TakeEffect(UnitItemView unit, bool isBuffEffect , float duration)
        {
            var animationType = isBuffEffect ? EUnitAnimationType.Buff : EUnitAnimationType.Debuff;

            _unitSpineAnimationService.PlayAnimation(unit, animationType, duration, false);
        }
    }
}