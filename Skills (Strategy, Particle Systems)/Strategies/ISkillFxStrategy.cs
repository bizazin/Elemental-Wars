using System.Collections.Generic;
using Enums.Skill;
using UI.Battle.Battleground.Views;
using UI.Battle.Battleground.Views.Items;
using UI.Battle.Units.Views;

namespace UI.FX.SkillParticles.Strategies
{
    public interface ISkillFxStrategy
    {
        EMagicSpells Spell { get; }
        bool IsCasting { get; }

        void Cast(UnitItemView playerUnit, List<UnitPlaceItemView> enemies, float duration, BattlegroundView view,
            bool isPlayerAttack);
        void StopCast();
    }
}