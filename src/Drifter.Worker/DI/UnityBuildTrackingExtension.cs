using System.Collections.Generic;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.ObjectBuilder;

namespace Drifter.Worker.DI
{
    public class UnityBuildTrackingExtension : UnityContainerExtension
    {

        protected override void Initialize()
        {
            this.Context.Strategies.AddNew<BuildTrackingStrategy>(UnityBuildStage.TypeMapping);
        }

        public static IBuildTrackingPolicy GetPolicy(IBuilderContext context)
        {
            return context.Policies.Get<IBuildTrackingPolicy>(context.BuildKey, true);
        }

        public static IBuildTrackingPolicy SetPolicy(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = new BuildTrackingPolicy();
            context.Policies.SetDefault<IBuildTrackingPolicy>(policy);
            return policy;
        }
    }

    public class BuildTrackingStrategy : BuilderStrategy
    {

        public override void PreBuildUp(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = UnityBuildTrackingExtension.GetPolicy(context);
            if (policy == null)
            {
                policy = UnityBuildTrackingExtension.SetPolicy(context);
            }

            policy.BuildKeys.Push(context.BuildKey);
        }

        public override void PostBuildUp(IBuilderContext context)
        {
            IBuildTrackingPolicy policy = UnityBuildTrackingExtension.GetPolicy(context);
            if ((policy != null) && (policy.BuildKeys.Count > 0))
            {
                policy.BuildKeys.Pop();
            }
        }
    }

    public interface IBuildTrackingPolicy : IBuilderPolicy
    {
        Stack<object> BuildKeys { get; }
    }

    public class BuildTrackingPolicy : IBuildTrackingPolicy
    {

        public BuildTrackingPolicy()
        {
            this.BuildKeys = new Stack<object>();
        }

        public Stack<object> BuildKeys { get; private set; }
    }
}
