using UnityEngine;

namespace HelicopterPhysics.Physics
{
    public class HelicopterBaseRigidbodyController : MonoBehaviour
    {

        public Transform COG;
        protected Rigidbody Rb;

        public float WeightInLbs = 10f;

        public float Weight;


        const float lbsToKg = 0.454f;
        const float kgToLbs = 2.205f;

        protected virtual void Awake()
        {
            Rb = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            float finalKG = WeightInLbs * lbsToKg;
            Weight = finalKG;

            Rb.mass = Weight;
        }

        private void FixedUpdate()
        {
            if (Rb)
            {
                HandlePhysics();
            }
        }

        protected virtual void HandlePhysics()
        {

        }
    }

}
