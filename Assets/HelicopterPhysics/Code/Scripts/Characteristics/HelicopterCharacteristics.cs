using HelicopterPhysics.Gameplay;
using HelicopterPhysics.Inputs;
using UnityEngine;
namespace HelicopterPhysics.Characteristics
{
    public class HelicopterCharacteristics : MonoBehaviour
    {
        [Header("Lift Properties")]
        public float MaxLiftForce = 100f;
        private HeliController _heliControl;

        [Space]

        [Header("Tail Rotor Properties")]
        public float TailForce = 2f;

        [Space]
        [Header("Cyclic Properties")]
        public float CyclicForce = 2f;


        private Vector3 FlatForward;
        private Vector3 FlatRight;

        protected virtual void Awake()
        {
            _heliControl = GetComponent<HeliController>();
        }

        public virtual void HandleCharacteristics(Rigidbody rb, InputController input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);

            CalculateAngles();
            AutoLevel();
        }

        protected virtual void HandleLift(Rigidbody rb, InputController input)
        {
            Vector3 liftForce = transform.up *
                (UnityEngine.Physics.gravity.magnitude + MaxLiftForce) * rb.mass;
            float normalizedRPMs = _heliControl.Engines[0].NormalizedRPM;
            liftForce *= Mathf.Pow(input.CurrentInput.StickyCollective, 2) * Mathf.Pow(normalizedRPMs, 2f);

            rb.AddForce(liftForce, ForceMode.Force);
        }

        protected virtual void HandleCyclic(Rigidbody rb, InputController input)
        {
            //handle cyclic
            float cyclicXForce = -input.CurrentInput.CyclicInput.x * CyclicForce;
            rb.AddRelativeTorque(Vector3.forward * cyclicXForce, ForceMode.Acceleration);


            float cyclicYForce = input.CurrentInput.CyclicInput.y * CyclicForce;
            rb.AddRelativeTorque(Vector3.right * cyclicYForce, ForceMode.Acceleration);
        }

        protected virtual void HandlePedals(Rigidbody rb, InputController input)
        {
            //handle tail rotors

            rb.AddTorque(Vector3.up * input.CurrentInput.PedalInput * TailForce, ForceMode.Acceleration);
        }

        private void CalculateAngles()
        {
            //calculate flat forward
            FlatForward = transform.forward;
            FlatForward.y = 0f;

            FlatForward = FlatForward.normalized;

            Debug.DrawRay(transform.position, FlatForward, Color.blue);

            //calculate flat right
            FlatRight = transform.right;
            FlatRight.y = 0f;

            FlatRight = FlatRight.normalized;
            Debug.DrawRay(transform.position, FlatRight, Color.red);

            // calculate angles (dot products)


        }

        private void AutoLevel()
        {

        }


    }
}
