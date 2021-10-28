using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuncExecutor;
using EntityBehavior.Status;

namespace EntityBehavior.Action {
    public class EntityActionTester : MonoBehaviour {
        //GameObject entity1;
        [SerializeField] GameObject entity2;
        [SerializeField] bool sw = false;
        [SerializeField] bool sw2 = false;

        // Start is called before the first frame update
        void Start() {
            entity2
                .ComponentEntityStatus()
                .Set(10, 5)
                .ComponentEntityActionCon<EntityActionCon_Node>()
                .BeginAction();
        }

        // Update is called once per frame
        void Update() {

        }

        private void gen1() {
            /*entity1.ComponentEntityAction()
                .Set(new A_waitForSeconds(1f))
                .Set()
                .Set()
                .BeginAction();*/

            /*entity2.ComponentEntityAction_Node()
                .SetNode(0, (false, 1, () => true))
                .SetFunction(new A_waitForSeconds(1f))
                .SetFunction(new A_DebugLog("message0"))
                //.SetFunction()
                .SetNode(1, (false, 0, () => true))
                .SetFunction(new A_waitForSeconds(1f))
                .SetFunction(new A_DebugLog("message1"))
                .BeginAction(1);*/

            /*A_ChainFunction ac = new A_ChainFunction (
                false,
                new A_waitForSeconds(1f),
                new A_DebugLog("chain0"),
                new A_waitForSeconds(1f),
                new A_DebugLog("chain1"),
                new A_waitForSeconds(1f),
                new A_DebugLog("chain2")
                );

            A_ChainFunction ac1 = new A_ChainFunction (
                true,
                new A_waitForSeconds(1f),
                new A_DebugLog("chain0"),
                new A_waitForSeconds(1f),
                new A_DebugLog("chain1"),
                new A_waitForSeconds(1f),
                new A_DebugLog("chain2")
                );

            A_LoopFunction al = new A_LoopFunction(
                false,
                //() => sw2,
                3,
                new A_DebugLog("loop"),
                new A_waitForSeconds(1)
                );

            EA_Flag flag = new EA_Flag(() => sw2, 3);

            entity2.ComponentEntityAction_Node()
                .SetNode(0, (false, 1, () => flag.GetFlag()), (false, 0, () => true))
                .SetFunction(
                    //ac1,
                    //ac,
                    //ac
                    //al,
                    new A_DebugLog("node0"),
                    new A_waitForSeconds(1f)
                    )
                .SetNode(1, (false, 0, () => flag.GetFlag()), (false, 1, () => true))
                .SetFunction(
                    //new A_Action(() => flag.BreakFlag()),
                    flag.A_BreakFlag(),
                    new A_DebugLog("node1"),
                    new A_waitForSeconds(1f)
                    //new A_Action(() => Debug.Log("a"))
                    )
                .BeginAction(0);*/

            /*EntityActionCon_Node e = entity2.ComponentEntityAction_Node();
            EntityStatus es = e.IGetEntityStatus();
            es.SetDamagedAction();
            e.SetNode(0, (false, 0, () => true), (true, 1, () => e.IGetEntityStatus().HP < 5f))
                .SetFunction()
                .BeginAction(0);*/
        }
    }
}