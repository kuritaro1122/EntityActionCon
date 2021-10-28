using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FuncExecutor;

namespace EntityBehavior.Action {
    public struct A_Movement : EA_IFunction {
        private Rigidbody rb;
        private bool asyn;
        private Vector3? beginPos;
        private Vector3 pos;
        private float duration;
        private bool addPos;
        private bool speedBase;
        public A_Movement(bool asyn, Vector3 pos, float duration) :
            this(asyn, null, pos, duration, false, false) {}
        public A_Movement(bool asyn, Vector3 pos, float duration, bool addPos = false, bool speedBase = false) :
            this(asyn, null, pos, duration, addPos, speedBase) {}
        public A_Movement(bool asyn, Vector3? beginPos, Vector3 pos, float duration, bool addPos = false, bool speedBase = false) {
            this.asyn = asyn;
            this.beginPos = beginPos;
            this.pos = pos;
            this.duration = duration;
            this.addPos = addPos;
            this.speedBase = speedBase;
            this.rb = null;
        }
        private IEnumerator Movement(Transform transform) {
            Vector3 _beginPos = beginPos ?? transform.localPosition;
            Vector3 _endPos;
            if (addPos) _endPos = _beginPos + pos;
            else _endPos = pos;
            float _time = (speedBase) ? Vector3.Distance(_beginPos, _endPos) / duration : duration;
            float elapsedTime = _time;
            while (elapsedTime > 0f) { //条件変える？？
                if (rb != null && rb.isKinematic == false) rb.velocity = (_endPos - _beginPos) / _time;
                else transform.localPosition = Vector3.Lerp(_beginPos, _endPos, 1 - elapsedTime / _time);
                elapsedTime -= Time.deltaTime;
                yield return null;
            }
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {
            this.rb = actionCon.IGetRigidbody();
        }
        public IEnumerator IGetFunction(IFunctionExecutor executor) => Movement(executor.IGetMonoBehaviour().transform);
        public bool IGetIsAsyn() => this.asyn;
    }

    public struct A_MovementSin : EA_IFunction {
        private bool asyn;
        private Vector3? beginPos;
        private Vector3 pos;
        private float frequency;
        private float amplitude;
        private float time;
        private Vector3 upwards;
        public A_MovementSin(bool asyn, Vector3 pos, float amplitude, float frequency, float time, Vector3 upwards) :
            this(asyn, null, pos, amplitude, frequency, time, upwards) {}
        public A_MovementSin(bool asyn, Vector3? beginPos, Vector3 pos, float amplitude, float frequency, float time, Vector3 upwards) {
            this.asyn = asyn;
            this.beginPos = beginPos;
            this.pos = pos;
            this.amplitude = amplitude;
            this.frequency = frequency;
            this.time = time;
            this.upwards = upwards.normalized;
        }
        public IEnumerator SinMovement(Transform transform) {
            Vector3 _beginPos = this.beginPos ?? transform.position;
            float _time = this.time;
            while (_time > 0f) {
                float t = 1 - _time / time;
                Vector3 _pos = Vector3.Lerp(_beginPos, this.pos, t);
                _pos += amplitude * Mathf.Sin(t * frequency * Mathf.PI) * upwards;
                if (transform == null) break; //必要？
                transform.position = _pos;
                _time -= Time.deltaTime;
                yield return null;
            }
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {}
        public IEnumerator IGetFunction(IFunctionExecutor executor) => SinMovement(executor.IGetMonoBehaviour().transform);
        public bool IGetIsAsyn() => this.asyn;
    }

    public struct A_Shot : EA_IFunction {
        private bool asyn;
        private GameObject ShotPrefab;
        private int num;
        private float span;
        private LaunchInfo[] launchInfos;
        public A_Shot(bool asyn, GameObject ShotPrefab, int num, float span, params LaunchInfo[] launchInfos) { //タプル版も作るか
            this.asyn = asyn;
            this.ShotPrefab = ShotPrefab;
            this.num = num;
            this.span = span;
            this.launchInfos = new LaunchInfo[launchInfos.Length];
            for (int i = 0; i < launchInfos.Length; i++) {
                this.launchInfos[i] = launchInfos[i];
            }
        }
        private IEnumerator Shot(Transform Parent) {
            for (int i = 0; i < num; i++) {
                foreach (var launch in launchInfos)
                    yield return launch.GetCoroutine(Parent, ShotPrefab);
                if (span > 0f) yield return new WaitForSeconds(span);
            }
        }
        public void ISetEntityActionCon(IEntityActionCon actionCon) {}
        public IEnumerator IGetFunction(IFunctionExecutor executor) => Shot(executor.IGetMonoBehaviour().transform);
        public bool IGetIsAsyn() => this.asyn;

        [System.Serializable]
        public struct LaunchInfo {
            private enum DirectionType { direction, lookAtTarget }
            [SerializeField] DirectionType directionType;
            [SerializeField] float waitTime;
            [Header("Position")]
            [SerializeField] Vector3 pos;
            [SerializeField] bool localPos;
            [Header("direction")]
            [SerializeField] Vector3 direction;
            [SerializeField] bool localDirection;
            [SerializeField] Transform target;
            public LaunchInfo(Vector3 pos, Vector3 direction, bool localPos = true, bool localDire = true, float waitTime = 0f) :
                this(DirectionType.direction, pos, direction, null, localPos, localDire, waitTime) {}
            public LaunchInfo(Vector3 pos, Transform target, bool localPos = true, float waitTime = 0f) :
                this(DirectionType.lookAtTarget, pos, Vector3.zero, target, localPos, false, waitTime) {}
            private LaunchInfo(DirectionType directionType, Vector3 pos, Vector3 direction, Transform target, bool localPos = true, bool localDire = true, float waitTime = 0f) {
                this.directionType = directionType;
                this.pos = pos;
                this.direction = direction;
                this.target = target;
                this.localPos = localPos;
                this.localDirection = localDire;
                this.waitTime = waitTime;
            }
            public IEnumerator GetCoroutine(Transform Parent, GameObject Shot) {
                yield return new WaitForSeconds(waitTime);
                Vector3 _position;
                Quaternion _rotation;
                if (Parent != null) {
                    if (localPos) _position = Parent.TransformPoint(pos);
                    else _position = pos;
                    switch (directionType) {
                        case DirectionType.direction:
                            Quaternion localRot = Quaternion.LookRotation(direction, Vector3.up);
                            if (localDirection) _rotation = Parent.rotation * localRot;
                            else _rotation = localRot;
                            break;
                        case DirectionType.lookAtTarget:
                            _rotation = Quaternion.LookRotation(target.position - _position);
                            break;
                        default:
                            _rotation = Quaternion.identity;
                            break;
                    }
                    MonoBehaviour.Instantiate(Shot, _position, _rotation);
                }
            }
        }
    }
}