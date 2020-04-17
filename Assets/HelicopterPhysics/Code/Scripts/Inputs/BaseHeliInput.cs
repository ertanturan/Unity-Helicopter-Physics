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
            get { return _vertical; }
        }
        public float Horizontal
        {
            get { return _horizontal; }
        }
        public float StickyThrottle
        {
            get
            {
                return _stickyThrottle;
            }
        }
        public float StickyCollective
        {
            get { return _stickyCollective; }
        }

        private Vector2 _cyclicInput = Vector2.zero;
        private float _pedalInput = 0f;
        private float _throttleInput = 0f;
        private float _collectiveInput = 0f;
        private float _stickyThrottle = 0f;
        private float _stickyCollective = 0f;

        private float _vertical = 0f;
        private float _horizontal = 0f;

        private void Update()
        {
            HandleInput();
        }

        protected virtual void HandleInput()
        {

            HandleVertHor();
            HandleThrottle();
            HandleCollective();
            HandleCyclic();
            HandlePedal();
            ClampInputs();
            HandleStickyThrottle();
            HandleStickyCollective();
        }



        protected virtual void HandleThrottle() { }
        protected virtual void HandlePedal() { }
        protected virtual void HandleCollective() { }
        protected virtual void HandleCyclic() { }

        protected virtual void HandleVertHor()
        {
            _vertical = Input.GetAxis("Vertical");
            _horizontal = Input.GetAxis("Horizontal");
        }
        private void ClampInputs()
        {
            ThrottleInput = Mathf.Clamp(ThrottleInput, -1f, 1f);
            CollectiveInput = Mathf.Clamp(CollectiveInput, -1f, 1f);
            _cyclicInput = Vector2.ClampMagnitude(_cyclicInput, 1);
            PedalInput = Mathf.Clamp(PedalInput, -1f, 1f);
        }
        private void HandleStickyThrottle()
        {

            _stickyThrottle += ThrottleInput * (Time.deltaTime / 10);
            _stickyThrottle = Mathf.Clamp01(_stickyThrottle);
        }

        private void HandleStickyCollective()
        {
            _stickyCollective += _collectiveInput * Time.deltaTime;
            _stickyCollective = Mathf.Clamp01(_stickyCollective);
        }
    }
}

