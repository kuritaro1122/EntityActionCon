using FuncExecutor;

namespace EntityBehavior.Action {
    public interface EA_IFunction : FE_IFunction {
        void ISetEntityActionCon(IEntityActionCon actionCon);
    }
}