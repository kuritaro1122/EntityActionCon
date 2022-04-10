using UnityEngine;
using FuncExecutor;
using System;
using EntityBehavior.Status;

namespace EntityBehavior.Action {
    [AddComponentMenu("EntityBehaviour/EntityActionCon_Node")]
    public class EntityActionCon_Node : FunctionExecutor_Node, IEntityActionCon {
        [SerializeField] EntityStatus entityStatus;
        [SerializeField] new Rigidbody rigidbody;

        public new EntityActionCon_Node SetNode(int index, params (bool, int, Func<bool>)[] nodeTransitions) {
            base.SetNode(index, nodeTransitions);
            return this;
        }
        public new EntityActionCon_Node SetNode(int index, NodeTransition[] nodeTransitions = null, NodeTransition[] nodeForcedTransition = null) {
            base.SetNode(index, nodeTransitions, nodeForcedTransition);
            return this;
        }
        public EntityActionCon_Node SetFunction(FE_IFunction function, params FE_IFunction[] functions) {
            base.SetFunction(function);
            base.SetFunction(functions);
            return this;
        }
        public EntityActionCon_Node SetFunction(params EA_IFunction[] functions) {
            foreach (var function in functions) function.ISetEntityActionCon(this);
            base.SetFunction(functions);
            return this;
        }

        public EntityStatus IGetEntityStatus() => this.entityStatus;
        public Rigidbody IGetRigidbody() => this.rigidbody;
    }
}