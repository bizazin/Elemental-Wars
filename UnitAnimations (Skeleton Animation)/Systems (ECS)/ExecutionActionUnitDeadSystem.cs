using System.Collections.Generic;
using Ecs.ExecutionAction.Utils;
using Ecs.Utils.Systems;
using Enums.ExecutionStage;
using JCMG.EntitasRedux;
using Plugins.Extensions.InstallerGenerator.Attributes;
using Plugins.Extensions.InstallerGenerator.Enums;

namespace Ecs.ExecutionAction.Systems
{
	[Install(ExecutionType.Battle, ExecutionPriority.Normal, 660)]
	public class ExecutionActionUnitDeadSystem : ReactiveSystemPooled<ExecutionActionEntity>
	{
		private readonly UnitContext _unitContext;
		private readonly IGroup<ExecutionActionEntity> _executionActionGroup;

		public ExecutionActionUnitDeadSystem(
			ExecutionActionContext executionActionContext,
			UnitContext unitContext
		) : base(executionActionContext)
		{
			_unitContext = unitContext;
			_executionActionGroup = executionActionContext.GetUnitDeadGroup();
		}

		protected override ICollector<ExecutionActionEntity> GetTrigger(IContext<ExecutionActionEntity> context)
			=> context.CreateCollector(ExecutionActionMatcher.Execute.Added());

		protected override bool Filter(ExecutionActionEntity entity)
			=> _executionActionGroup.ContainsEntity(entity) &&
			   entity.ExecutionActionType.Value == EExecutionActionType.UnitDead;

		protected override void Execute(List<ExecutionActionEntity> executionActionEntities)
		{
			for (var i = 0; i < executionActionEntities.Count; i++)
			{
				var executionActionEntity = executionActionEntities[i];
				executionActionEntity.IsDestroyed = true;

				var unitEntity = _unitContext.GetEntityWithUnitUid(executionActionEntity.UnitUid.Value);
				unitEntity.IsDead = true;
			}
		}
	}
}