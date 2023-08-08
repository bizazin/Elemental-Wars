using System;
using Databases.Animations;
using DG.Tweening;
using Enums.Hero;
using Spine;
using UI.Battle.Units.Views;

namespace Services.Battle.Impls
{
    public class UnitSpineAnimationService : IUnitSpineAnimationService
    {
        private readonly IUnitAnimationDatabase _unitAnimationDatabase;

        public UnitSpineAnimationService(IUnitAnimationDatabase unitAnimationDatabase) => 
            _unitAnimationDatabase = unitAnimationDatabase;

        public void PlayAnimation(UnitItemView unit, EUnitAnimationType animationType, bool isLooped = true)
        {
            var timeScale = _unitAnimationDatabase.GetAnimationTimeScale(animationType);
            PlayAnimationInternal(unit, animationType, isLooped, animation => animation.AnimationEnd / timeScale);
        }

        public void PlayAnimation(UnitItemView unit, EUnitAnimationType animationType, float duration, bool isLooped = true) => 
            PlayAnimationInternal(unit, animationType, isLooped, animation => animation.AnimationEnd / duration);

        private void PlayAnimationInternal(UnitItemView unit, EUnitAnimationType animationType, bool isLooped, Func<TrackEntry, float> computeInterval)
        {
            var name = _unitAnimationDatabase.GetAnimationName(animationType);
    
            foreach (var unitSkin in unit.Skins)
            {
                var animationState = unitSkin.AnimationState;
                var animation = animationState.SetAnimation(0, name, isLooped);
                animationState.TimeScale = computeInterval(animation);
        
                QueueIdleAnimation(animationState, computeInterval(animation));
            }
        }

        private void QueueIdleAnimation(AnimationState animationState, float interval)
        {
            DOTween.Sequence()
                .AppendInterval(interval)
                .AppendCallback(() =>
                {
                    animationState.TimeScale = _unitAnimationDatabase.GetAnimationTimeScale(EUnitAnimationType.Idle);
                    animationState.AddAnimation(0, _unitAnimationDatabase.GetAnimationName(EUnitAnimationType.Idle), true, 0);
                });
        }
    }
}