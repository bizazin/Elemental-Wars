using System;
using DG.Tweening;
using Ecs.Utils.View.Impls;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Battle.Units.Views
{
    public class UnitItemView : LinkableView<UnitEntity>
    {
        [SerializeField] private Image healthBarImage;
        [SerializeField] private SkeletonAnimation headSkeletonAnimation;
        [SerializeField] private SkeletonAnimation bodySkeletonAnimation;
        [SerializeField] private SkeletonAnimation tailSkeletonAnimation;
        [SerializeField] private SkeletonAnimation[] skeletonAnimations;
        
        public Image HealthBarImage => healthBarImage;
        public SkeletonAnimation[] Skins => skeletonAnimations;
        
        public Tween Blink(Color blinkColor, float blinkDuration, Color startHealthBarColor)
        {
            return DOTween.Sequence()
                .AppendCallback(() =>
                {
                    SetHeroDamageColor(blinkColor);
                    SetHealthBarColor(blinkColor);
                })
                .AppendInterval(blinkDuration)
                .AppendCallback(() =>
                {
                    SetHeroDamageColor(Color.white);
                    SetHealthBarColor(startHealthBarColor);
                });
        }
        
        public void ChangeSkin(string head, string body, string tail)
        {
            ApplyNewSkin(headSkeletonAnimation, head);
            ApplyNewSkin(bodySkeletonAnimation, body);
            ApplyNewSkin(tailSkeletonAnimation, tail);
        }
        
        private void ApplyNewSkin(SkeletonAnimation skeletonAnimation, string skin)
        {
            var newSkin = skeletonAnimation.Skeleton.Data.FindSkin(skin);
            if (newSkin == null)
                throw new Exception($"[{nameof(UnitItemView)}] There is no skin {skin} in SkeletonAnimation");

            skeletonAnimation.Skeleton.SetSkin(newSkin);
            skeletonAnimation.Skeleton.SetSlotsToSetupPose();
            skeletonAnimation.AnimationState.Apply(skeletonAnimation.Skeleton);
        }

        private void SetHeroDamageColor(Color color)
        {
            foreach (var skeletonAnimation in skeletonAnimations)
                foreach (var slot in skeletonAnimation.Skeleton.Slots)
                    slot.SetColor(color);
        }
        
        private void SetHealthBarColor(Color color) =>
            healthBarImage.color = color;
    }
}