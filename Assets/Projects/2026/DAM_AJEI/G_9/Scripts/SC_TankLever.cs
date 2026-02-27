using Autohand;
using UnityEngine;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_TankLever : PhysicsGadgetHingeAngleReader
    {
        public SC_Tank tank;

        public bool right;

        void Update()
        {
            if (Mathf.Abs(GetValue()) > 0.1f)
            {
                if (right)
                    tank.right_input = GetValue();
                else
                    tank.left_input = GetValue();
            }
        }
    }
}