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

        [Space]
        [Header("Auto Level Properties")]
        public float AutoLevelForce = 2f;

        private Vector3 _flatForward;
        private float _forwardDot;
        private Vector3 _flatRight;
        private float _rightDot;

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
            AutoLevel(rb);
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
            _flatForward = transform.forward;
            _flatForward.y = 0f;
            _flatForward = _flatForward.normalized;

            Debug.DrawRay(transform.position, _flatForward, Color.blue);

            //calculate flat right
            _flatRight = transform.right;
            _flatRight.y = 0f;
            _flatRight = _flatRight.normalized;

            Debug.DrawRay(transform.position, _flatRight, Color.red);

            // calculate angles (dot products)
            _forwardDot = Vector3.Dot(transform.up, _flatForward);
            _rightDot = Vector3.Dot(transform.up, _flatRight);

            //Debug.Log(string.Format("Fwd : {0} - Right : {1} ", _forwardDot.ToString("0.0")
            //    , _rightDot.ToString("0.0")));


        }

        private void AutoLevel(Rigidbody rb)
        {
            //auto correct the helicopter

            float rightForce = -_forwardDot * AutoLevelForce;
            float forwardForce = _rightDot * AutoLevelForce;


            rb.AddRelativeTorque(Vector3.right * rightForce, ForceMode.Acceleration);
            rb.AddRelativeTorque(Vector3.forward * forwardForce, ForceMode.Acceleration);
        }


    }
}
