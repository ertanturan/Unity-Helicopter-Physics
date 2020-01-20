using UnityEngine;

namespace HelicopterPhysics.Inputs
{
    public class BaseHeliInput : MonoBehaviour
    {

        public float ThrottleInput
        {
            get { return _throttleInput; }
            set { _throttleInput = value; }
        }
        public float CollectiveInput
        {
            get { return _collectiveInput; }
            set { _collectiveInput = value; }
        }
        public Vector2 CyclicInput
        {
            get { return _cyclicInput; }
            set { _cyclicInput = value; }
        }
        public float PedalInput
        {
            get { return _pedalInput; }
            set { _pedalInput = value; }
        }
        public float Vertical
        {
            get { return _Vertical; }
        }
        public float Horizontal
        {
            get { return _Horizontal; }
        }


        private Vector2 _cyclicInput = Vector2.zero;
        private float _pedalInput = 0f;
        private float _throttleInput = 0f;
        private float _collectiveInput = 0f;


        private float _Vertical = 0f;
        private float _Horizontal = 0f;

        private void Update()
        {
            HandleInput();
        }

        protected virtual void HandleInput()
        {
            _Vertical = Input.GetAxis("Vertical");
            _Horizontal = Input.GetAxis("Horizontal");

            HandleThrottle();
            HandleCollective();
            HandleCyclic();
            HandlePedal();
            ClampInputs();
        }



        protected virtual void HandleThrottle() { }
        protected virtual void HandlePedal() { }
        protected virtual void HandleCollective() { }
        protected virtual void HandleCyclic() { }

        private void ClampInputs()
        {
            ThrottleInput = Mathf.Clamp(ThrottleInput, -1f, 1f);
            CollectiveInput = Mathf.Clamp(CollectiveInput, -1f, 1f);
            CyclicInput = Vector2.ClampMagnitude(CyclicInput, 1);
            PedalInput = Mathf.Clamp(PedalInput, -1f, 1f);
        }

    }
}

