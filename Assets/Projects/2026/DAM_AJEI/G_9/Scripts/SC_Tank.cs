using UnityEngine;
using UnityEngine.InputSystem;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_Tank : MonoBehaviour
    {

        [Header("Stats")]
        public float speed;
        public float turn_speed;
        public Vector3 right_wheel_offset, left_wheel_offset;
        [Range(0, 1)]public float damping = 0.99f;

        public float right_input = 0, left_input = 0;
        private Vector2 input;
        private Rigidbody rb;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            right_wheel_offset += transform.position;
            left_wheel_offset += transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            TankVelocity();


        }

        private void TankVelocity()
        {
            Vector3 right_vel = transform.forward * speed * right_input;
            Vector3 left_vel = transform.forward * speed * left_input;

            float turn = right_input - left_input;


            rb.AddTorque(Vector3.up * turn * turn_speed, ForceMode.Acceleration);

            rb.AddForce((right_vel + left_vel) * speed, ForceMode.Acceleration);


            // Clamp the velocity
            rb.linearVelocity = ClampSpeed(rb.linearVelocity);
            rb.angularVelocity = ClampSpeed(rb.angularVelocity);

            rb.linearVelocity *= damping;
            rb.angularVelocity *= damping;
        }

        private Vector3 ClampSpeed(Vector3 vel)
        {
            vel.x = Mathf.Clamp(vel.x, -10, 10);
            vel.y = Mathf.Clamp(vel.y, -5, 5);
            vel.z = Mathf.Clamp(vel.z, -10, 10);

            return vel;
        }
        
        public void ProcessInput(InputAction.CallbackContext con)
        {
            if (con.performed)
            {
                input = con.ReadValue<Vector2>();

                if (input.x == 0)
                {
                    right_input = input.y;
                    left_input = input.y;
                }
                else if (input.x > 0)
                {
                    right_input = 1;
                    left_input = 0;
                }
                else if (input.x < 0)
                {
                    right_input = 0;
                    left_input = 1;
                }
                
            }
            else if (con.canceled)
            {
                right_input = 0;
                left_input = 0;
            }
        }
    
        public void ProcessRightInput(InputAction.CallbackContext con)
        {
            if (con.performed)
            {
                input.x = con.ReadValue<float>();
            }
        }
        public void ProcessLeftInput(InputAction.CallbackContext con)
        {
            if (con.performed)
            {
                input.y = con.ReadValue<float>();
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(right_wheel_offset + transform.position, 0.5f);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(left_wheel_offset + transform.position, 0.5f);
        }
    }
}