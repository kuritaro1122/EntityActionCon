# EntityActionCon
[Beta] Entityにさまざまな命令を付与するプログラム
※ EntityStatusをインポートする必要があります。


# Component add & get
* EntityActionCon ComponentEntityActionCon(this GameObject self)
* EntityActionCon ComponentEntityActionCon(this EntityStatus self)
* EntityActionCon_Node ComponentEntityActionCon<EntityActionCon_Node>(this GameObject self)
* EntityActionCon_Node ComponentEntityActionCon<EntityActionCon_Node>(this EntityStatus self)

# 【EntityActionCon】
* EntityActionCon Set(params EA_IFunction[] functions)
  - EntityActionCon Set(params EF_IFunction[] functions) //非推奨
* Coroutine BeginAction()

* MonoBehaviour IGetMonoBehaviour()
* Rigidbody IGetRigidbody()
* EntityStatus IGetEntityStatus()

# 【EntityActionCon_Node】
* EntityActionCon_Node SetNode(int index, params (bool, int, Func<bool>)[] nodeTransitions)
  - EntityActionCon_Node SetNode(int index, params NodeTransition[] nodeTransitions, NodeTransition[] nodeForcedTransition)
* EntityActionCon_Node SetFunction(params EA_IFunction[] functions)
  - EntityActionCon_Node SetFunction(params FE_IFunction[] functions) //非推奨
* Coroutine BeginAction()
  
* MonoBehaviour IGetMonoBehaviour()
* Rigidbody IGetRigidbody()
* EntityStatus IGetEntityStatus()
  
# 【EA_IFunctions】
> standard
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
> entity3d
* A_Movement(bool asyn, Vector3? beginPos, Vector3 pos, float duration, bool addPos = false, bool speedBase = false)
  - this(bool asyn, Vector3 pos, float duration)
  - this(bool asyn, Vector3 pos, float duration, bool addPos = false, bool speedBase = false)
* A_MovementSin(bool asyn, Vector3 beginPos, Vector3 pos, float amplitude, float frequency, float time, Vector3 upwards)
  - this(bool asyn, Vector3 pos, float amplitude, float frequency, float time, Vector3 upwards)
* A_Shot(bool asyn, GameObject ShotPrefab, int num, float span, params A_Shot.LaunchInfo[] launchInfos)
  - AShot.LaunchInfo(Vector3 pos, Vector3 direction, bool localPos = true, bool localDire = true, float waitTime = 0f)
  - AShot.LaunchInfo(Vector3 pos, Transform target, bool localPos = true, float waitTime = 0f)
  
# Example
```
GameObject enemy1, enemy2, bullet, player;
Instantiate(enemy1)
.ComponentEntityActionCon().Set(
  new A_WaitForSeconds(2f),
  new A_DebugLog("message")
)
.BeginAction();
  
Instantiate(enemy2, Vector3.zero, Quaternion.identity)
.ComponentEntityStatus<EntityStatus>().Set(10, 5)
.ComponentEntityActionCon().Set(
  new A_Movement(false, new Vector3(10, 0, 0), 5f, false, true),
  new A_WaitForSeconds(0.5f),
  new A_Shot(false, bullet, 3, 1f,
    new A_Shot.LaunchInfo(Vector3.zero, Vector3.forward, true, true, 0.2f),
    new A_Shot.LaunchInfo(Vector3.zero, Vector3.forward, true, true, 0.2f),
    new A_Shot.LaunchInfo(Vector3.zero, player.transform, true, 0.2f)
  ),
  new A_Movement(false, new Vector3(10, 30, 0), 5f, false, false),
  new A_Destroy()
)
.BeginAction();
```
