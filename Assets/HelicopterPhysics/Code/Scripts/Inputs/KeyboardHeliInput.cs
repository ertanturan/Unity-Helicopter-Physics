using UnityEngine;
namespace HelicopterPhysics.Inputs
{
    public class KeyboardHeliInput : BaseHeliInput
    {

        protected override void HandleThrottle()
        {
            base.HandleThrottle();
            ThrottleInput = Input.GetAxis("Throttle");
        }

        protected override void HandlePedal()
        {
            base.HandlePedal();
            CollectiveInput = Input.GetAxis("Collective");
        }

        protected override void HandleCollective()
        {
            base.HandleCollective();

            PedalInput = Input.GetAxis("Pedal");
        }


        protected override void HandleCyclic()
        {
            base.HandleCyclic();
            Vector2 temp = new Vector2(Horizontal, Vertical);

            CyclicInput = temp;
        }

        //protected override void HandleVertHor()
        //{
        // //use mouse to control helicopter
        //    Vertical = Input.GetAxis("Mouse Y");
        //    Horizontal = Input.GetAxis("Mouse X");
        //}
    }

}
