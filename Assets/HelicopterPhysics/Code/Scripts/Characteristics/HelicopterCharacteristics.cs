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

        protected virtual void Awake()
        {
            _heliControl = GetComponent<HeliController>();
        }

        public virtual void HandleCharacteristics(Rigidbody rb, InputController input)
        {
            HandleLift(rb, input);
            HandleCyclic(rb, input);
            HandlePedals(rb, input);
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

        }

        protected virtual void HandlePedals(Rigidbody rb, InputController input)
        {

        }


    }
}
