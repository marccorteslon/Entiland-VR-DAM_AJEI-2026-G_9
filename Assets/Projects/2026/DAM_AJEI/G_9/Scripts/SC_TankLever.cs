using Autohand;
using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_TankLever : PhysicsGadgetHingeAngleReader
    {
        public SC_Tank tank;
        public bool right;

        private HingeJoint hinge;
        private Quaternion startLocalRotation;

        void Awake()
        {
            hinge = GetComponent<HingeJoint>();

            startLocalRotation = transform.localRotation;
        }

        void Update()
        {
            float angle = GetLocalHingeAngle();

            JointLimits limits = hinge.limits;
            float min = limits.min;
            float max = limits.max;

            float normalized = Mathf.InverseLerp(min, max, angle) * 2f - 1f;

            if (right)
                tank.right_input = normalized;
            else
                tank.left_input = normalized;
        }

        float GetLocalHingeAngle()
        {
            // Current rotation relative to starting pose
            Quaternion delta = Quaternion.Inverse(startLocalRotation) * transform.localRotation;

            // Convert hinge axis to local space
            Vector3 axis = hinge.axis;

            delta.ToAngleAxis(out float angle, out Vector3 deltaAxis);

            // Ensure proper sign
            float sign = Mathf.Sign(Vector3.Dot(deltaAxis, axis));

            return angle * sign;
        }
    }
}