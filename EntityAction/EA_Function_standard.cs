using System.Collections;
using UnityEngine;
using FuncExecutor;
using System;

namespace EntityBehavior.Action {
    public class A_WaitForSeconds : ToEAFunction<F_WaitForSeconds> {
        public A_WaitForSeconds(float time) : base(new F_WaitForSeconds(time)) {}
    }

    public class A_waitUntil : ToEAFunction<F_WaitUntil> {
        public A_waitUntil(Func<bool> condition) : base(new F_WaitUntil(condition)) {}
    }

    public class A_Destroy : ToEAFunction<F_Destroy> { //ドロップアイテム？？
        public A_Destroy(GameObject gameObject = null) : base(new F_Destroy(gameObject)) {}
    }

    public class A_DebugLog : ToEAFunction<F_DebugLog> {
        public A_DebugLog(string message) : base(new F_DebugLog(message)) {}
    }

    public class A_Action : EA_IFunction {
        private System.Action<IEntityActionCon> action;
        private IEntityActionCon actionCon = null;
        public A_Action(System.Action action) {
            this.action = ea => action();
        }
        public A_Action(System.Action<IEntityActionCon> action) {
            this.action = action;
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {
            this.actionCon = actionCon;
        }
        public IEnumerator IGetFunction(IFunctionExecutor executor) {
            this.action(this.actionCon);
            return null;
        }
        public bool IGetIsAsyn() => false;
    }

    public class A_Coroutine : EA_IFunction {
        private System.Func<IEntityActionCon, IEnumerator> enumerator;
        private IEntityActionCon actionCon = null;
        private bool asyn;
        public A_Coroutine(bool asyn, System.Func<IEnumerator> enumerator) {
            this.enumerator = fe => enumerator();
            this.asyn = asyn;
        }
        public A_Coroutine(bool asyn, System.Func<IEntityActionCon, IEnumerator> enumerator) {
            this.enumerator = enumerator;
            this.asyn = asyn;
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {
            this.actionCon = actionCon;
        }
        public IEnumerator IGetFunction(IFunctionExecutor executor) {
            return this.enumerator(this.actionCon);
        }
        public bool IGetIsAsyn() => this.asyn;
    }

    public class ToEAFunction<I> : EA_IFunction where I : FE_IFunction {
        private readonly I function;
        public ToEAFunction(I function) {
            this.function = function;
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {}
        public IEnumerator IGetFunction(IFunctionExecutor executor) => this.function.IGetFunction(executor);
        public bool IGetIsAsyn() => this.function.IGetIsAsyn();
    }

    public struct A_ChainFunction : EA_IFunction {
        private IEntityActionCon actionCon;
        private EA_IFunction[] functions;
        private bool asyn;
        public A_ChainFunction(bool asyn, params EA_IFunction[] functions) {
            this.actionCon = null;
            this.asyn = asyn;
            this.functions = MergeArrayClass.MergeArray(functions);
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {
            this.actionCon = actionCon;
        }
        public IEnumerator IGetFunction(IFunctionExecutor executor) {
            return EntityActionCon.FunctionsExecute(this.actionCon, this.functions);
        }
        public bool IGetIsAsyn() => this.asyn;
    }

    public struct A_LoopFunction : EA_IFunction {
        enum LoopType { Condition, Count, Both }
        private LoopType type;
        private IEntityActionCon actionCon;
        private Func<bool> condition;
        private int count;
        private bool and;
        private EA_IFunction[] functions;
        private bool asyn;
        public A_LoopFunction(bool asyn, Func<bool> condition, params EA_IFunction[] functions) : this(asyn, functions) {
            this.type = LoopType.Condition;
            this.condition = condition;
        }
        public A_LoopFunction(bool asyn, int count, params EA_IFunction[] functions) : this(asyn, functions) {
            this.type = LoopType.Count;
            this.count = count;
        }
        public A_LoopFunction(bool asyn, Func<bool> condition, int count, bool and, params EA_IFunction[] functions) : this(asyn, functions) {
            this.type = LoopType.Both;
            this.condition = condition;
            this.count = count;
            this.and = and;
        }
        private A_LoopFunction(bool asyn, params EA_IFunction[] functions) {
            this.actionCon = null;
            this.functions = MergeArrayClass.MergeArray(functions);
            this.asyn = asyn;
            this.type = default;
            this.condition = default;
            this.count = default;
            this.and = default;
        }

        public void ISetEntityActionCon(IEntityActionCon actionCon) {
            this.actionCon = actionCon;
        }
        public IEnumerator IGetFunction(IFunctionExecutor executor) {
            int _count = count;
            while (LoopCondition(_count)) {
                yield return EntityActionCon.FunctionsExecute(this.actionCon, this.functions);
                _count = Mathf.Max(0, _count - 1);
            }
        }
        private bool LoopCondition(float count) {
            bool t1 = type == LoopType.Condition && condition();
            bool t2 = type == LoopType.Count && count > 0;
            bool t3 = type == LoopType.Both && (and ? condition() && count > 0 : condition() || count > 0);
            return t1 || t2 || t3;
        }
        public bool IGetIsAsyn() => this.asyn;
    }
}