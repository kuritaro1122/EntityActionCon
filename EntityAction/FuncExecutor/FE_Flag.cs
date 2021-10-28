//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
using System;

namespace FuncExecutor {
    public class FE_Flag : Flag {
        public FE_Flag(Func<bool> condition, int flagNum = 1) : base(condition, flagNum) {}
        public FE_IFunction A_BreakFlag() => new F_Action(() => base.BreakFlag());
        public FE_IFunction A_BreakAllFlag() => new F_Action(() => base.BreakAllFlag());
        public FE_IFunction A_RecreateFlag() => new F_Action(() => base.RecreateFlag());
    }
}