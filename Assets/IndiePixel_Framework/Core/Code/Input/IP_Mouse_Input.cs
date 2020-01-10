using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace IndiePixel.Core
{
    public class IP_Mouse_Input : MonoBehaviour
    {
        #region Variables
        public MouseButtonInfo m_LeftMouseButton = new MouseButtonInfo();
        public MouseButtonInfo m_MiddleMouseButton = new MouseButtonInfo();
        public MouseButtonInfo m_RightmouseButton = new MouseButtonInfo();

        private Vector2 mousePosition;
        public Vector2 MousePostion
        {
            get{return mousePosition;}
        }

        private Vector2 mouseDelta;
        public Vector2 MouseDelta
        {
            get{return mouseDelta;}
        }

        private float zoomDelta;
        public float ZoomDelta
        {
            get{return zoomDelta;}
        }

        [Header("Mouse Events")]
        public UnityEvent onLeftButtonDown = new UnityEvent();
        public UnityEvent onLeftButtonUp = new UnityEvent();

        public UnityEvent onMiddleButtonDown = new UnityEvent();
        public UnityEvent onMiddleButtonUp = new UnityEvent();

        public UnityEvent onRightButtonDown = new UnityEvent();
        public UnityEvent onRightButtonUp = new UnityEvent();
        #endregion





        #region Main Methods
        // Use this for initialization
        void Start () 
        {
            InitializeInputs();    
        }

        // Update is called once per frame
        void Update () 
        {
            HandleButtonEvents();
            HandleMousePositionData();
        }
        #endregion






        #region Custom Methods
        protected virtual void InitializeInputs(){}
        protected virtual void HandleButtonEvents()
        {
            //Left Mouse Button Events
            m_LeftMouseButton.buttonDown = Input.GetMouseButtonDown(0);
            m_LeftMouseButton.buttonHeld = Input.GetMouseButton(0);
            m_LeftMouseButton.buttonUp = Input.GetMouseButtonUp(0);

            if(onLeftButtonDown != null && m_LeftMouseButton.buttonDown)
            {
                onLeftButtonDown.Invoke();
            }

            if(onLeftButtonDown != null && m_LeftMouseButton.buttonUp)
            {
                onLeftButtonUp.Invoke();
            }



            //Middle Mouse Button Events
            m_MiddleMouseButton.buttonDown = Input.GetMouseButtonDown(2);
            m_MiddleMouseButton.buttonHeld = Input.GetMouseButton(2);
            m_MiddleMouseButton.buttonUp = Input.GetMouseButtonUp(2);
            zoomDelta = Input.GetAxis("Mouse ScrollWheel");

            if(onMiddleButtonDown != null && m_MiddleMouseButton.buttonDown)
            {
                onMiddleButtonDown.Invoke();
            }

            if(onMiddleButtonUp != null && m_MiddleMouseButton.buttonUp)
            {
                onMiddleButtonUp.Invoke();
            }



            //Right Mouse Button Events
            m_RightmouseButton.buttonDown = Input.GetMouseButtonDown(1);
            m_RightmouseButton.buttonHeld = Input.GetMouseButton(1);
            m_RightmouseButton.buttonUp = Input.GetMouseButtonUp(1);

            if(onRightButtonDown != null && m_RightmouseButton.buttonDown)
            {
                onRightButtonDown.Invoke();
            }

            if(onRightButtonUp != null && m_RightmouseButton.buttonUp)
            {
                onRightButtonUp.Invoke();
            }
        }

        protected virtual void HandleMousePositionData()
        {
            mousePosition = Input.mousePosition;
            mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
        #endregion
    }


    #region Button Info Struct
    public struct MouseButtonInfo
    {
        public bool buttonDown;
        public bool buttonHeld;
        public bool buttonUp;
    }
    #endregion
}
