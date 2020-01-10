using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel.Core
{
    [CustomEditor(typeof(IP_Mouse_Input))]
    public class IP_MouseInput_Editor : Editor 
    {
        #region Variables
        IP_Mouse_Input targetInput;
        #endregion


        #region Main Methods
        void OnEnable()
        {
            targetInput = (IP_Mouse_Input)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUILayout.Space(20f);

            //Gather the Debug information for the Mouse
            string mouseInfo = "";
            mouseInfo += "Mouse Position: " + targetInput.MousePostion.ToString() + "\n";
            mouseInfo += "Mouse Delta: " + targetInput.MouseDelta.ToString() + "\n";
            mouseInfo += "Zoom Delta: " + targetInput.ZoomDelta.ToString() + "\n";
            mouseInfo += "\n";
            mouseInfo += "Left Button Down: " + targetInput.m_LeftMouseButton.buttonDown.ToString() + "\n";
            mouseInfo += "Left Button Held: " + targetInput.m_LeftMouseButton.buttonHeld.ToString() + "\n";
            mouseInfo += "Left Button Up: " + targetInput.m_LeftMouseButton.buttonUp.ToString() + "\n";
            mouseInfo += "\n";
            mouseInfo += "Middle Button Down: " + targetInput.m_MiddleMouseButton.buttonDown.ToString() + "\n";
            mouseInfo += "Middle Button Held: " + targetInput.m_MiddleMouseButton.buttonHeld.ToString() + "\n";
            mouseInfo += "Middle Button Up: " + targetInput.m_MiddleMouseButton.buttonUp.ToString() + "\n";
            mouseInfo += "\n";
            mouseInfo += "Right Button Down: " + targetInput.m_RightmouseButton.buttonDown.ToString() + "\n";
            mouseInfo += "Right Button Held: " + targetInput.m_RightmouseButton.buttonHeld.ToString() + "\n";
            mouseInfo += "Right Button Up: " + targetInput.m_RightmouseButton.buttonUp.ToString() + "\n";

            //Display the Debug Information
            EditorGUILayout.TextArea(mouseInfo, GUILayout.ExpandWidth(true), GUILayout.Height(210));

            GUILayout.Space(20f);

            Repaint();
        }
        #endregion
    }
}
