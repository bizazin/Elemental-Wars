using System;
using System.Collections.Generic;
using System.Linq;
using Enums.Skill;
using UI.Battle.Battleground.Views;
using UI.Battle.Battleground.Views.Items;
using UI.Battle.Units.Views;
using UI.FX.SkillParticles.Strategies;

namespace Services.Battle.Impls
{
    public class BattleAnimationService : IBattleAnimationService
    {
        private readonly BattlegroundView _battlegroundView;
        private readonly Dictionary<EMagicSpells, ISkillFxStrategy> _skillFxStrategies;

        public BattleAnimationService
        (
            BattlegroundView battlegroundView,
            List<ISkillFxStrategy> skillFxStrategies
        )
        {
            _battlegroundView = battlegroundView;
            _skillFxStrategies = skillFxStrategies.ToDictionary(x => x.Spell);
        }

        public void PlayAttackParticle(UnitItemView playerUnit, List<UnitPlaceItemView> enemies, EMagicSpells skillType,
            float duration, bool isPlayerAttack)
        {
            if (skillType == EMagicSpells.None)
                throw new Exception($"[{nameof(BattleAnimationService)}] Invalid magic skill type: {skillType}");
            
            _skillFxStrategies[skillType].Cast(playerUnit, enemies, duration/1000, _battlegroundView, isPlayerAttack);
        }
    }
}