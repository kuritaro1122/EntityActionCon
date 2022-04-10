using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using FuncExecutor;
using EntityBehavior.Status;

namespace EntityBehavior.Action {
    [AddComponentMenu("EntityBehaviour/EntityActionCon")]
    public class EntityActionCon : FunctionExecutor, IEntityActionCon {
        [SerializeField] EntityStatus entityStatus;
        [SerializeField] new Rigidbody rigidbody;
        public EntityActionCon Set(FE_IFunction function, params FE_IFunction[] functions) {
            base.Set(function);
            base.Set(functions);
            return this;
        }
        public EntityActionCon Set(params EA_IFunction[] functions) {
            if (functions == null) {
                Debug.LogWarning("EntityActionCon/functions is null.", this);
                return this;
            }
            foreach (var function in functions) function.ISetEntityActionCon(this);
            base.Set(functions);
            return this;
        }
        public EntityStatus IGetEntityStatus() => this.entityStatus;
        public Rigidbody IGetRigidbody() => this.rigidbody;

        public static IEnumerator FunctionsExecute(IEntityActionCon actionCon, params EA_IFunction[] functions) {
            foreach (var function in functions) {
                function.ISetEntityActionCon(actionCon);
                IEnumerator enumerator = function.IGetFunction(actionCon);
                if (function.IGetIsAsyn())
                    actionCon.IGetMonoBehaviour().StartCoroutine(enumerator);
                else yield return enumerator;
            }
        }
    }

    public static class AddComponenter_EA {
        public static EntityActionCon ComponentEntityActionCon(this GameObject self) {
            return self.ComponentEntityActionCon<EntityActionCon>();
        }
        public static EntityActionCon ComponentEntityActionCon(this EntityStatus self) {
            return self.ComponentEntityActionCon<EntityActionCon>();
        }
        public static I ComponentEntityActionCon<I>(this GameObject self) where I : MonoBehaviour, IEntityActionCon {
            return self.ComponentCheck<I>();
        }
        public static I ComponentEntityActionCon<I>(this EntityStatus self) where I : MonoBehaviour, IEntityActionCon {
            return ComponentEntityActionCon<I>(self.gameObject);
        }
    }
}