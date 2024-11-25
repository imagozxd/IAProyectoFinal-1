using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody
{
    [TaskCategory("Unity/Rigidbody")]
    [TaskDescription("Sets the mass of the Rigidbody. Returns Success.")]
    public class SetMass : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The mass of the Rigidbody")]
        public SharedFloat mass;

        // cache the rigidbody component
        private Rigidbody rigidbody;
        private GameObject prevGameObject;

        public override void OnAwake()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody = currentGameObject.GetComponent<Rigidbody>();
                prevGameObject = currentGameObject;
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (rigidbody == null) {
                Debug.LogWarning("Rigidbody is null");
                return TaskStatus.Failure;
            }

            rigidbody.mass = mass.Value;

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            mass = 0;
        }
    }
}