using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel
{
    [CustomEditor(typeof(IP_Game_Sequence))]
    public class IP_GameSequence_Editor : Editor
    {
        #region Variables
        IP_Game_Sequence targetSequence;

        SerializedObject sTarget;
        SerializedProperty sList;
        #endregion


        #region Main Methods
        void OnEnable()
        {
            targetSequence = (IP_Game_Sequence)target;
            sTarget = new SerializedObject(targetSequence);
            sList = sTarget.FindProperty("m_Actions");
        }

        public override void OnInspectorGUI()
        {
            sTarget.Update();
            base.OnInspectorGUI();

            GUILayout.Space(15);
            EditorGUILayout.LabelField("Actions", EditorStyles.boldLabel);
            if(targetSequence.m_Actions.Count > 0)
            {
                for(int i = 0; i < sList.arraySize; i++)
                {
                    SerializedProperty curAction = sList.GetArrayElementAtIndex(i); 

                    Color origColor = GUI.color;
                    GUI.color = Color.black;

                    GUILayout.BeginVertical("", EditorStyles.helpBox);
                    GUILayout.Space(5);
                    GUI.color = origColor;


                    EditorGUILayout.BeginHorizontal();
                    targetSequence.m_Actions[i].collapsed = EditorGUILayout.Foldout(targetSequence.m_Actions[i].collapsed, targetSequence.m_Actions[i].m_ActionName);
                    GUILayout.FlexibleSpace();
                    if(GUILayout.Button("Up", GUILayout.Width(45)))
                    {
                        MoveActionUp(targetSequence.m_Actions[i], i);
                    }

                    if(GUILayout.Button("Down", GUILayout.Width(45)))
                    {
                        MoveActionDown(targetSequence.m_Actions[i], i);
                    }

                    GUI.color = Color.red;
                    if(GUILayout.Button("X", GUILayout.ExpandWidth(true), GUILayout.Width(45)))
                    {
                        RemoveAction(targetSequence.m_Actions[i]);
                    }
                    GUI.color = origColor;
                    EditorGUILayout.EndHorizontal();



                    if(targetSequence.m_Actions[i].collapsed)
                    {
                        //Name Field
                        EditorGUILayout.BeginHorizontal();
                        SerializedProperty actionName = curAction.FindPropertyRelative("m_ActionName");
                        EditorGUILayout.PropertyField(actionName);


                        EditorGUILayout.EndHorizontal();

                        //Wait Time
                        SerializedProperty waitTime = curAction.FindPropertyRelative("m_WaitTime");
                        EditorGUILayout.PropertyField(waitTime);

                        //Events
                        SerializedProperty startedEvent = curAction.FindPropertyRelative("OnStarted");
                        EditorGUILayout.PropertyField(startedEvent);

                        SerializedProperty completedEvent = curAction.FindPropertyRelative("OnCompleted");
                        EditorGUILayout.PropertyField(completedEvent);
                    }
                    GUILayout.Space(5);
                    GUILayout.EndVertical();

                    GUILayout.Space(10);
                }
            }

            GUILayout.Space(15);
            if(GUILayout.Button("Add Action", GUILayout.ExpandWidth(true), GUILayout.Height(35)))
            {
                AddSimpleAction();
            }

            Repaint();
            sTarget.ApplyModifiedProperties();
        }
        #endregion



        #region Custom Methods
        void AddSimpleAction()
        {
            IP_Action curAction = new IP_Action();
            targetSequence.m_Actions.Add(curAction);
        }


        void RemoveAction(IP_Action action)
        {
            if(targetSequence.m_Actions.Contains(action))
            {
                targetSequence.m_Actions.Remove(action);
            }
        }

        void MoveActionUp(IP_Action action, int index)
        {
            if(index != 0 && action != null)
            {
                targetSequence.m_Actions.RemoveAt(index);
                index--;
                targetSequence.m_Actions.Insert(index, action);
            }
        }

        void MoveActionDown(IP_Action action, int index)
        {
            if(index !=  targetSequence.m_Actions.Count - 1 && action != null)
            {
                targetSequence.m_Actions.RemoveAt(index);
                index++;
                targetSequence.m_Actions.Insert(index, action);
            }
        }
        #endregion
    }
}
