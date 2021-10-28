//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using FuncExecutor;
using EntityBehavior.Status;

namespace EntityBehavior.Action {
    public interface IEntityActionCon : IFunctionExecutor {
        EntityStatus IGetEntityStatus();
        Rigidbody IGetRigidbody();
    }
}