using UnityEngine;
namespace HelicopterPhysics.Inputs
{
    public class KeyboardHeliInput : BaseHeliInput
    {

        public float ThrottleInput { get { return _throttleInput; } }
        public float CollectiveInput { get { return _collectiveInput; } }
        public Vector2 CyclicInput { get { return _cyclicInput; } }
        public float PedalInput { get { return _pedalInput; } }


        private float _throttleInput = 0f;
        private float _collectiveInput = 0f;
        private Vector2 _cyclicInput = Vector2.zero;
        private float _pedalInput = 0f;

        protected override void HandleInput()
        {
            base.HandleInput();

            HandleThrottle();
            HandleCollective();
            HandleCyclic();
            HandlePedal();

        }


        private void HandleThrottle()
        {
            _throttleInput = Vertical;
        }

        private void HandlePedal()
        {
            _pedalInput = Horizontal;
        }

        private void HandleCollective()
        {

        }


        private void HandleCyclic()
        {
            _cyclicInput.y = Vertical;
            _cyclicInput.x = Horizontal;
        }

    }

}
