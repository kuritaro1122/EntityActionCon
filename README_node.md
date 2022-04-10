[EntityActionConはこちら](/README.md)
# FunctionExecutor_Node

Entityに命令を付与し、非同期的に逐次実行する。\
ノードごとに命令をまとめ、それらを遷移しながら実行する。\
ノードの遷移は逐次的遷移と強制遷移のに種類を選ぶことができ、ゲームのボスなどのプログラム処理に重宝する。

<!--# DEMO

-->


# Requirement

* UnityEngine
* System
* System.Collections
* [FunctionExecutor](https://github.com/kuritaro1122/FunctionExecutor)
* [EntityBehaviour.Status](https://github.com/kuritaro1122/EntityStatus)

# Usage

```
GameObject target;
bool moveToOne = false;

EntityActionCon_Node entityAction = target.ComponentEntityActionCon_Node(); //Add or GetComponent
entityAction.SetNode(0, (true, 1, () => moveToOne), (false, 0, () => true))
    .SetFunction(
        // Called when node index is 0.
        // new EA_Function1(),
        // new EA_Function2(),
        // ...
    )
    .SetNode(1, (true, 0, () => !moveToOne), (false, 1, () => true))
    .SetFunction(
        // Called when node index is 1.
        // new EA_Function1(),
        // new EA_Function2(),
        // ...
    ).BeginAction(0) // begin node index.
```

## EA_Functions
* EA_Function_Standard
```
A_WaitForSeconds(float time)
A_WaitUntil(System.Func<bool> condition)
A_Destroy(GameObject gameObject = null)
A_DebugLog(string message)
A_Action(System.Action action)
A_Action(System.Action<IEntityActionCon> action)
A_Coroutine(bool asyn, System.Func<IEnumerator> enumerator)
A_Coroutine(bool asyn, System.Func<IEntityActionCon, IEnumerator> enumerator)

A_ChainFunction(bool asyn, params EA_IFunction[] functions)
A_LoopFunction(bool asyn, Func<bool> condition, params EA_IFunction[] functions)
A_LoopFunction(bool asyn, int count, params EA_IFunction[] functions)
A_LoopFunction(bool asyn, Func<bool> condition, int count, bool and, params EA_IFunction[] functions)
```
* EA_Function_Entity3D
```
A_Movement(bool asyn, Vector3 pos, float duration)
A_Movement(bool asyn, Vector3 pos, float duration, bool addPos = false, bool speedBase = false)
A_Movement(bool asyn, Vector3? beginPos, Vector3 pos, float duration, bool addPos = false, bool speedBase = false)
A_MovementSin(bool asyn, Vector3 pos, float amplitude, float frequency, float time, Vector3 upwards)
A_MovementSin(bool asyn, Vector3? beginPos, Vector3 pos, float amplitude, float frequency, float time, Vector3 upwards)
A_Shot(bool asyn, GameObject ShotPrefab, int num, float span, params LaunchInfo[] launchInfos)
```
* [EA_Function_Curve]()
```
A_MovementCurve(bool asyn, ICurve curve, float duration, bool speedBase = false, bool localPos = false, bool localRot = false)
```

## EA_IFunction (Interface)
```
IEnumerator IGetFunction(IFunctionExecutor executor)
bool IGetAsyn()
void ISetEntityActionCon(IEntityActionCon actionCon)
```

# Contains

<!--## Inspector

-->

## Public Function
```
EntityActionCon_Node SetNode(int index, params (bool, int, Func<bool>)[] nodeTransitions)
EntityActionCon_Node SetNode(int index, NodeTransition[] nodeTransitions = null, NodeTransition[] nodeForcedTransitions = null)
FunctionExecutor_Node SetFunction(params EA_IFunction[] functions)
Coroutine BeginAction(int index = 0)
MonoBehaviour IGetMonoBehaviour()
EntityStatus IGetEntityStatus()
RigidBody IGetRigidBody()
```
### NodeTransition (struct)
```
NodeTransition(int nextNodeIndex, Func<bool> condition)
int? GetTransitionOrder()
```

# Note
* bool asynをtrueにすると、前の命令の終了を待たずに実行されます。
* 新たな命令を追加したい場合には、A_ActionやA_Coroutineを使うか、EA_IFunctionを継承したクラスを作ってください。

# License
"EntityActionCon_Node" is under [MIT license](https://en.wikipedia.org/wiki/MIT_License).
