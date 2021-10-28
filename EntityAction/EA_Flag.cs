using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace EntityBehavior.Action {
    public class EA_Flag : Flag {
        public EA_Flag(Func<bool> conditon, int flagNum = 1) : base(conditon, flagNum) {}
        public EA_IFunction A_BreakFlag() => new A_Action(() => base.BreakFlag());
        public EA_IFunction A_BreakAllFlag() => new A_Action(() => base.BreakAllFlag());
        public EA_IFunction A_RecreateFlag() => new A_Action(() => base.RecreateFlag());
    }
}