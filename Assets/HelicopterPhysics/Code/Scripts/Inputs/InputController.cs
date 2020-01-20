using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace HelicopterPhysics.Inputs
{
    [RequireComponent(typeof(KeyboardHeliInput))]
    [RequireComponent(typeof(XboxHeliInput))]
    public class InputController : MonoBehaviour
    {
        public InputType Input = InputType.PC;
        private XboxHeliInput _xboxController;
        private KeyboardHeliInput _pcController;

        private List<BaseHeliInput> _inputs = new List<BaseHeliInput>();

        private void Awake()
        {
            _inputs = GetComponents<BaseHeliInput>().ToList();
            _xboxController = GetComponent<XboxHeliInput>();
            _pcController = GetComponent<KeyboardHeliInput>();

            SetInputType();
        }

        private void SetInputType()
        {
            DisableAllInputs();
            switch (Input)
            {
                case InputType.PC:
                    _pcController.enabled = true;
                    break;

                case InputType.XBOX:
                    _xboxController.enabled = true;
                    break;

                default:
                    _pcController.enabled = true;
                    break;
            }
        }

        private void DisableAllInputs()
        {
            foreach (BaseHeliInput input in _inputs)
            {
                input.enabled = false;
            }
        }
    }

}
