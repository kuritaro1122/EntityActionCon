# EntityActionCon
[Beta] Entityにさまざまな命令を付与するプログラム
※ EntityStatusをインポートする必要があります。

# 【簡易リファレンス】
**コンポーネントに追加 & 取得**
* EntityActionCon ComponentEntityActionCon(this GameObject self)
* EntityActionCon ComponentEntityActionCon(this EntityStatus self)
* EntityActionCon_Node ComponentEntityActionCon<EntityActionCon_Node>(this GameObject self)
* EntityActionCon_Node ComponentEntityActionCon<EntityActionCon_Node>(this EntityStatus self)

**EntityActionCon**
~ public関数 ~
* EntityActionCon Set(params EA_IFunction[] functions)
* EntityActionCon Set(params EF_IFunction[] functions) //非推奨
* Coroutine BeginAction()

* MonoBehaviour IGetMonoBehaviour()
* Rigidbody IGetRigidbody()
* EntityStatus IGetEntityStatus()

**EntityActionCon_Node**
~ public関数 ~
* EntityActionCon_Node SetNode(int index, params (bool, int, Func<bool>)[] nodeTransitions)
* EntityActionCon_Node SetNode(int index, params NodeTransition[] nodeTransitions, NodeTransition[] nodeForcedTransition)
* EntityActionCon_Node SetFunction(params EA_IFunction[] functions)
* EntityActionCon_Node SetFunction(params FE_IFunction[] functions) //非推奨
* Coroutine BeginAction()
  
* MonoBehaviour IGetMonoBehaviour()
* Rigidbody IGetRigidbody()
* EntityStatus IGetEntityStatus()
  
**EA_IFunctions**
* A_WaitForSeconds(float time)
* A_WaitUntil(Func<bool> condition)
* A_Destroy(GameObject gameObject = null, System.Action action = null)
* A_DebugLog(string message)
* A_ChainFunction(bool asyn, params EA_IFunction[] functions)
* A_LoopFunction(bool asyn, Func<bool> condition, int count, bool and, params EA_IFunction[] functions)
  - this(bool asyn, Func<bool> condition, params EA_IFunction[] functions)
  - this(bool asyn, int count, params EA_IFunction[] functions)
* A_Action(System.Action action)
* A_Coroutine(bool asyn, IEnumerator coroutine)
