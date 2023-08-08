using System.Collections.Generic;
using System.Linq;
using Databases.Skills;
using Ecs.ExecutionAction.Utils;
using Ecs.Utils.Systems;
using Enums.ExecutionStage;
using Enums.Player;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;
using UI.Battle.Battleground.Controllers;

namespace Ecs.ExecutionAction.Systems
{
    [Install(ExecutionType.Battle, ExecutionPriority.Normal, 200)]
    public class ExecutionActionExecuteSkillSystem : ReactiveSystemPooled<ExecutionActionEntity>
    {
        private readonly ExecutionStepContext _executionStepContext;
        private readonly UnitContext _unitContext;
        private readonly SkillContext _skillContext;
        private readonly IBattlegroundController _battlegroundController;
        private readonly ISkillCastInfoDatabase _skillCastInfoDatabase;
        private readonly IGroup<ExecutionActionEntity> _executionActionGroup;

        public ExecutionActionExecuteSkillSystem(
            ExecutionStepContext executionStepContext,
            ExecutionActionContext executionActionContext,
            UnitContext unitContext,
            SkillContext skillContext,
            IBattlegroundController battlegroundController,
            ISkillCastInfoDatabase skillCastInfoDatabase
        ) : base(executionActionContext)
        {
            _executionStepContext = executionStepContext;
            _unitContext = unitContext;
            _skillContext = skillContext;
            _battlegroundController = battlegroundController;
            _skillCastInfoDatabase = skillCastInfoDatabase;
            _executionActionGroup = executionActionContext.GetExecuteSkillGroup();
        }

        protected override ICollector<ExecutionActionEntity> GetTrigger(IContext<ExecutionActionEntity> context)
            => context.CreateCollector(ExecutionActionMatcher.Execute.Added());

        protected override bool Filter(ExecutionActionEntity entity)
            => _executionActionGroup.ContainsEntity(entity) &&
               entity.ExecutionActionType.Value == EExecutionActionType.ExecuteSkill;

        protected override void Execute(List<ExecutionActionEntity> executionActionEntities)
        {
            for (var i = 0; i < executionActionEntities.Count; i++)
            {
                var executionActionEntity = executionActionEntities[i];
                executionActionEntity.AddExecutionActionType();
                executionActionEntity.IsDestroyed = true;

                var applicatorEntity = _unitContext.GetEntityWithUnitUid(executionActionEntity.ApplicatorUnitUid.Value);
                var skillEntity = _skillContext.GetEntityWithSkillUid(executionActionEntity.SkillUid.Value);
                var targetUnitIds = executionActionEntity.TargetUnitUids.Value;
                var skillCastInfo = _skillCastInfoDatabase.GetSkillCastInfo(skillEntity.SkillId.Value);
                var delayBeforeSkillMs =
                    (int)(skillCastInfo.DelayBeforeSkillMs * _executionStepContext.DurationTimeCoefficient.Value);

                var skillDurationMs = executionActionEntity.DurationTimeMs.Value - delayBeforeSkillMs;
                var magicSpellType = skillEntity.MagicSpellType.Value;
                var skillId = skillEntity.SkillId.Value;
                var isPlayer = applicatorEntity.TeamType.Value == EPlayerType.Player;

                var targetEntities = targetUnitIds.Select(_unitContext.GetEntityWithUnitUid).ToArray();
                _battlegroundController.CastAttack(magicSpellType, skillId, applicatorEntity, isPlayer,
                    skillDurationMs, delayBeforeSkillMs, skillCastInfo.AnimationType, targetEntities);
            }
        }
    }
}