using UnityEngine;

namespace HelicopterPhysics.Characteristics
{
    public class HelicopterCharacteristics : MonoBehaviour
    {

        public virtual void HandleCharacteristics()
        {
            HandleLift();
            HandleCyclic();
            HandlePedals();
        }

        protected virtual void HandleLift()
        {

        }

        protected virtual void HandleCyclic()
        {

        }

        protected virtual void HandlePedals()
        {

        }


    }
}
