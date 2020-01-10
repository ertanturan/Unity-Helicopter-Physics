using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IndiePixel.Core
{
    public class IP_VertexPainter_Tool : EditorWindow 
    {

        #region Variables
        public IP_VTXPainterState m_CurrentState;

        public GUISkin curSkin;
        public RaycastHit m_CurHit;
        public GameObject m_CurrentGO;
        public Mesh m_CurrentMesh;

        public IP_MouseData m_MouseData = new IP_MouseData();
        public IP_BrushData m_BrushData = new IP_BrushData();

        public float m_MinBrushSize = 0.1f;

        private bool m_WasPainting = false;
        #endregion


        #region Main Methods
        public static void LaunchVertexPainter()
        {
            var win = GetWindow<IP_VertexPainter_Tool>(false, "Mesh Painter", true);
            win.m_CurrentState = IP_VTXPainterState.None;
            win.GenerateGUIStyle();

            win.m_BrushData = new IP_BrushData(1f, 0.5f, 0.5f, Color.white, IP_SculptState.NormalDir, 1f, 1, 0f);
        }

        void OnFocus()
        {
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
            SceneView.onSceneGUIDelegate += this.OnSceneGUI;
        }

        void OnDestroy()
        {
            SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
        }
        #endregion



        #region UI Methods
        void OnGUI()
        {
            //Header
            GUILayout.Box("Mesh Painter", curSkin.box, GUILayout.Height(60), GUILayout.ExpandWidth(true));

            //Body
            EditorGUILayout.BeginVertical(curSkin.GetStyle("Body"), GUILayout.ExpandHeight(false));
            EditorGUILayout.LabelField("Info", curSkin.label);

            string curObjectName = "None";
            if(m_CurrentGO && m_CurrentMesh)
            {
                curObjectName = m_CurrentGO.name;
                curObjectName = m_CurrentMesh.name;
            }
            EditorGUILayout.LabelField("GameObject: " + curObjectName, curSkin.GetStyle("h3"));
            EditorGUILayout.LabelField("Mesh: " + curObjectName, curSkin.GetStyle("h3"));
            EditorGUILayout.LabelField("State: " + m_CurrentState.ToString(), curSkin.GetStyle("h3"));
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();


            if(m_CurrentState != IP_VTXPainterState.None)
            {
                EditorGUILayout.BeginVertical(curSkin.GetStyle("Body"), GUILayout.ExpandHeight(true));
                EditorGUILayout.LabelField("Actions", curSkin.label);

                DrawActionUI();

                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }


            EditorGUILayout.BeginVertical(curSkin.GetStyle("Body"), GUILayout.ExpandHeight(true));
            EditorGUILayout.LabelField("Tools", curSkin.label);
            EditorGUILayout.Space();

            DrawToolUI();

            EditorGUILayout.EndVertical();

            //Footer
            GUILayout.Box("", curSkin.box, GUILayout.Height(30f), GUILayout.ExpandWidth(true));

            //Update the GUI
            Repaint();
        }

        void OnSceneGUI(SceneView sceneView)
        {
            ProcessInputs();

            if(m_CurrentState == IP_VTXPainterState.Painting || m_CurrentState == IP_VTXPainterState.Sculpting)
            {
                //Raycast into the scene and see if we are hitting anything
                Ray worldRay = HandleUtility.GUIPointToWorldRay(m_MouseData.mousePos);
                if(!Physics.Raycast(worldRay, out m_CurHit, float.MaxValue))
                {
                    m_CurrentGO = null;
                    m_CurrentMesh = null;
                    return;
                }
                m_CurrentGO = m_CurHit.transform.gameObject;

                //Show the Brush
                if(m_CurHit.transform != null)
                {
                    if(m_CurrentState == IP_VTXPainterState.Painting)
                    {
                        Handles.color = m_BrushData.brushColor;   
                    }

                    if(m_CurrentState == IP_VTXPainterState.Sculpting)
                    {
                        Handles.color = Color.red;
                    }
                    Handles.DrawWireDisc(m_CurHit.point, m_CurHit.normal, m_BrushData.falloffSize);
                    Handles.DrawWireDisc(m_CurHit.point, m_CurHit.normal, m_BrushData.brushSize);

                    Handles.color = new Color(Handles.color.r, Handles.color.g, Handles.color.b, m_BrushData.opacity);
                    Handles.DrawSolidDisc(m_CurHit.point, m_CurHit.normal, m_BrushData.brushSize);
                }

                //Get the Mesh only if its different than what is in the current it object  
                if(m_CurHit.transform.GetInstanceID() != m_CurrentGO.GetInstanceID())
                {
                    m_CurrentMesh = IP_VertexPainter_Utils.GetMesh(m_CurrentGO);
                }

                //Set Scene View to passive so we can paint without selecting anything
                Tools.current = Tool.None;
                HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            }

            //Handle the Functions of the brush
            if(m_MouseData.leftClickHold)
            {
                IP_VertexPainter_Utils.HandleBrush(m_CurrentGO, m_CurrentMesh, m_CurHit.point, m_BrushData, m_CurrentState);
                if(m_CurrentMesh)
                {
                    EditorUtility.SetDirty(m_CurrentMesh);
                }
                m_WasPainting = true;
            }
            else
            {
                if(m_WasPainting && m_CurrentGO)
                {
                    MeshCollider curCollider = m_CurrentGO.GetComponent<MeshCollider>();
                    if(curCollider)
                    {
                        curCollider.convex = true;
                        curCollider.convex = false;
                    }

                    m_WasPainting = false;
                }
            }


            //Update the Scene View
            sceneView.Repaint();
        }
        #endregion



        #region Body Methods
        protected virtual void DrawToolUI()
        {
            if(GUILayout.Button("Color Fill", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
            {
                GameObject curSelected = IP_VertexPainter_Utils.GetSelection();
                Mesh curMesh = IP_VertexPainter_Utils.GetMesh(curSelected);

                IP_VertexPainter_Utils.FillMeshWithColor(curMesh, m_BrushData.brushColor);
            }

            if(GUILayout.Button("Add Mesh Collider", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
            {
                //Get the Selected GO
                GameObject curSelected = IP_VertexPainter_Utils.GetSelection();

                //Add the Mesh Collider
                IP_VertexPainter_Utils.AddMeshCollider(curSelected);
            }

            if(GUILayout.Button("Save Mesh", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
            {
                //Get the current selection
                GameObject curSelected = IP_VertexPainter_Utils.GetSelection();

                //Save the mesh
                IP_VertexPainter_Utils.SaveMesh(curSelected);
            }
        }

        protected virtual void DrawActionUI()
        {
            if(m_CurrentState == IP_VTXPainterState.Painting)
            {
                EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("Red", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.brushColor = Color.red;
                }
                if(GUILayout.Button("Green", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.brushColor = Color.green;
                }
                if(GUILayout.Button("Blue", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.brushColor = Color.blue;
                }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                if(GUILayout.Button("White", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.brushColor = Color.white;
                }
                if(GUILayout.Button("Black", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.brushColor = Color.black;
                }
                EditorGUILayout.EndHorizontal();

            }

            if(m_CurrentState == IP_VTXPainterState.Sculpting)
            {
                m_BrushData.sculptIntensity = EditorGUILayout.Slider("Intensity: ", m_BrushData.sculptIntensity, 0f, 50f);

                if(GUILayout.Button("Normal Dir", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.sculptState = IP_SculptState.NormalDir;
                }

                if(GUILayout.Button("World Up Dir", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.sculptState = IP_SculptState.WorldUpDir;
                }

                if(GUILayout.Button("Flatten", curSkin.button, GUILayout.Height(44), GUILayout.ExpandWidth(true)))
                {
                    m_BrushData.sculptState = IP_SculptState.Flatten;
                }
            }
        }
        #endregion



        #region Input Methods
        void ProcessInputs()
        {
            Event e = Event.current;

            HandleKeyboardShortcuts(e);

            if(e.isMouse)
            {
                //TODO:  Set up mouse clicks and stuff
                HandleMouse(e);
            }

            m_MouseData.mousePos = e.mousePosition;
        }

        void HandleKeyboardShortcuts(Event curEvent)
        {
            if(!curEvent.alt)
            {
                if(curEvent.type == EventType.KeyDown)
                {
                    //If the Paint key is Pressed
                    if(curEvent.keyCode == KeyCode.P)
                    {
                        if(m_CurrentState == IP_VTXPainterState.None)
                        {
                            //Debug.Log("Setting Painting");
                            m_CurrentState = IP_VTXPainterState.Painting;
                        }
                        else if(m_CurrentState == IP_VTXPainterState.Painting)
                        {
                            //Debug.Log("Setting None");
                            m_CurrentState = IP_VTXPainterState.None;
                        }
                    }

                    //if the sculpt key is pressed
                    if(curEvent.keyCode == KeyCode.S)
                    {
                        if(m_CurrentState == IP_VTXPainterState.None)
                        {
                            m_CurrentState = IP_VTXPainterState.Sculpting;
                        }
                        else if(m_CurrentState == IP_VTXPainterState.Sculpting)
                        {
                            m_CurrentState = IP_VTXPainterState.None;
                        }
                    }

                    //if the escape key is pressed
                    if(curEvent.keyCode == KeyCode.Escape)
                    {
                        m_CurrentState = IP_VTXPainterState.None;
                    }

                    //Brush size keyboard control
                    if(curEvent.keyCode == KeyCode.LeftBracket)
                    {
                        m_BrushData.brushSize -= 0.2f;
                        if(m_BrushData.brushSize <= m_BrushData.falloffSize)
                        {
                            m_BrushData.falloffSize = m_BrushData.brushSize;
                        }
                        m_BrushData.brushSize = Mathf.Clamp(m_BrushData.brushSize, m_MinBrushSize, float.MaxValue);
                    }

                    if(curEvent.keyCode == KeyCode.RightBracket)
                    {
                        m_BrushData.brushSize += 0.2f;
                        m_BrushData.brushSize = Mathf.Clamp(m_BrushData.brushSize, m_MinBrushSize, float.MaxValue);
                    }

                    if(curEvent.keyCode == KeyCode.Semicolon)
                    {
                        m_BrushData.falloffSize -= 0.2f;
                        m_BrushData.falloffSize = Mathf.Clamp(m_BrushData.falloffSize, m_MinBrushSize, float.MaxValue);
                    }

                    if(curEvent.keyCode == KeyCode.Quote)
                    {
                        m_BrushData.falloffSize += 0.2f;
                        if(m_BrushData.falloffSize >= m_BrushData.brushSize)
                        {
                            m_BrushData.brushSize = m_BrushData.falloffSize;
                        }
                        m_BrushData.falloffSize = Mathf.Clamp(m_BrushData.falloffSize, m_MinBrushSize, float.MaxValue);
                    }

                    if(curEvent.keyCode == KeyCode.Equals)
                    {
                        m_BrushData.opacity += 0.02f;
                    }

                    if(curEvent.keyCode == KeyCode.Minus)
                    {
                        m_BrushData.opacity -= 0.02f;
                    }
                    m_BrushData.opacity = Mathf.Clamp01(m_BrushData.opacity);


                    if(curEvent.control)
                    {
                        m_BrushData.sculptDir = -1;
                    }


                }

                //Reset any keyboard data stuff here
                if(curEvent.type == EventType.KeyUp)
                {
                    m_BrushData.sculptDir = 1;
                }
            }

        }

        void HandleMouse(Event curEvent)
        {
            m_MouseData.mouseDelta = curEvent.delta;
            if(!curEvent.alt)
            {
                //Left mouse button
                m_MouseData.leftClickHold = false;
                m_MouseData.leftClick = false;
                if(curEvent.button == 0)
                {
                    if(curEvent.type == EventType.MouseDown)
                    {
                        m_MouseData.leftClick = true;

                        if(m_CurHit.transform && m_CurrentGO)
                        {
                            m_BrushData.startHeight = m_CurrentGO.transform.InverseTransformPoint(m_CurHit.point).y;
                        }
                    }

                    if(curEvent.type == EventType.MouseDrag)
                    {
                        m_MouseData.leftClickHold = true;
                    }
                }

                //Right Click
                m_MouseData.rightClick = false;
                m_MouseData.rightClickHold = false;
                if(curEvent.button == 1)
                {
                    if(curEvent.type == EventType.MouseDown)
                    {
                        m_MouseData.rightClick = true;
                    }

                    if(curEvent.type == EventType.MouseDrag)
                    {
                        m_MouseData.rightClickHold = true;
                    }
                }
            }
        }
        #endregion



        #region Utility Methods
        void GenerateGUIStyle()
        {
            curSkin = (GUISkin)Resources.Load("EF_VertexPainter_Skin");
            if(!curSkin)
            {
                curSkin = EditorGUIUtility.GetBuiltinSkin(EditorSkin.Inspector);
            }
        }
        #endregion
        
    }
}
