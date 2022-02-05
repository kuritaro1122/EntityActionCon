using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuncExecutor;
using Curve;

namespace EntityBehavior.Action {
    public struct A_MovementCurve : EA_IFunction {
        private Rigidbody rb;
        private ICurve curve;
        private bool asyn;
        private float duration;
        private bool speedBase;
        private bool localPos;
        private bool localRot;
        public A_MovementCurve(bool asyn, ICurve curve, float duration, bool speedBase = false, bool localPos = false, bool localRot = false) {
            this.asyn = asyn;
            this.curve = curve;
            this.rb = null;
            this.duration = duration;
            this.speedBase = speedBase;
            this.localPos = localPos;
            this.localRot = localRot;
        }
        private IEnumerator Movement(Transform transform) {
            Vector3 beginPos = this.localPos ? transform.position : Vector3.zero;
            Quaternion beginRot = this.localRot ? transform.rotation : Quaternion.identity;
            float time;
            time = this.speedBase ? this.curve.GetCurveLength() / this.duration : this.duration;
            float t = time;
            while (t > 0f) {
                Vector3 localPos = this.curve.GetPosition(1 - (t / time), true);
                Vector3 pos = beginPos + beginRot * localPos;
                if (this.rb != null && this.rb.isKinematic == false) rb.MovePosition(transform.parent.transform.TransformPoint(pos));
                else transform.localPosition = pos;
                t -= Time.deltaTime;
                yield return null;
            }
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {
            this.rb = actionCon.IGetRigidbody();
        }
        public IEnumerator IGetFunction(IFunctionExecutor executor) => Movement(executor.IGetMonoBehaviour().transform);
        public bool IGetIsAsyn() => this.asyn;
    }

    /*public struct A_MovementCurve_straight : EA_IFunction {
        private A_MovementCurve function;
        public A_MovementCurve_straight(bool asyn) {
            this.function = new A_MovementCurve();
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) => this.function.ISetEntityActionCon(actionCon);
        public IEnumerator IGetFunction(IFunctionExecutor executor) => this.function.IGetFunction(executor);
        public bool IGetIsAsyn() => this.function.IGetIsAsyn();
    }

    public struct A_MovementCurve_catmullRom : EA_IFunction {
        private A_MovementCurve function;
        public void ISetEntityActionCon(IEntityActionCon actionCon) => this.function.ISetEntityActionCon(actionCon);
        public IEnumerator IGetFunction(IFunctionExecutor executor) => this.function.IGetFunction(executor);
        public bool IGetIsAsyn() => this.function.IGetIsAsyn();
    }*/
}
