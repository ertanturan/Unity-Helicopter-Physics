using System.Collections;
using UnityEditor;
using UnityEngine;

namespace IndiePixel.Core
{
    public class IP_ObjectReplacer_Window : IP_Base_Window 
    {
        #region Variables
        static IP_ObjectReplacer_Window m_Win;
        GameObject m_TargetObject;
        #endregion

        #region Main Methods
        public static void InitWindow()
        {
            m_Win = GetWindow<IP_ObjectReplacer_Window>(true, "Object Replacer", true);
            m_Win.Show();
        }

        void OnEnable()
        {
            Selection.selectionChanged += GetSelected;
        }

        void OnDisable()
        {
            Selection.selectionChanged -= GetSelected;
        }
        #endregion


        #region UI Methods
        void OnGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Selected Object Count: " + m_SelectedObjects.Length);
            m_TargetObject = (GameObject)EditorGUILayout.ObjectField("Target Object: ", m_TargetObject, typeof(GameObject), false);


            if(GUILayout.Button("Replace Selected", GUILayout.Height(44), GUILayout.ExpandWidth(true)))
            {
                ReplaceSelected();
            }

            Repaint();    
        }

        void ReplaceSelected()
        {
            if(m_TargetObject && m_SelectedObjects.Length > 0)
            {
                for(int i = 0; i < m_SelectedObjects.Length; i++)
                {
                    GameObject oldGO = m_SelectedObjects[i];
                    GameObject newGO = Instantiate(m_TargetObject, oldGO.transform.position, oldGO.transform.rotation);
                    newGO.transform.localScale = oldGO.transform.localScale;

                    DestroyImmediate(oldGO);
                }
            }
            else
            {
                IP_Editor_Utils.DisplayDialogBox("Please Select Objects to replace or assign a GameObject to the target object!");
            }
        }
        #endregion
    }
}
