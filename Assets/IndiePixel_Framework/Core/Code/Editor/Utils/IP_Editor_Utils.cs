using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;

namespace IndiePixel.Core
{
    public static class IP_Editor_Utils 
    {
        //Display Dialog Box
        public static void DisplayDialogBox(string aMessage)
        {
            EditorUtility.DisplayDialog("Indie Pixel Warning", aMessage, "OK");
        }



        //Get the selected Game object
        public static GameObject GetSelectedObject(string aWarningMessage)
        {
            GameObject selectedGO = Selection.activeGameObject;
            if(!selectedGO)
            {
                EditorUtility.DisplayDialog("Indie Pixel Warning", aWarningMessage, "OK");
                return null;
            }
            else
            {
                return selectedGO;
            }
        }

        public static GameObject GetSelectedObject()
        {
            GameObject selectedGO = Selection.activeGameObject;
            if(!selectedGO)
            {
                return null;
            }
            else
            {
                return selectedGO;
            }
        }



        //Check to see if an Input GO is in the scene
        public static void CheckForInputs()
        {
//            GameObject inputs = GameObject.FindObjectOfType<EF_PlayerInput>().gameObject;
//            if(!inputs)
//            {
//                GameObject inputGO = new GameObject("_Input_Manager");
//                DrawIcon(inputGO, 3);
//            }
        }


        //Get the Emortal Editor Skin
        public static GUISkin GetEditorUI()
        {
            GUISkin editorSkin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/IndiePixel_Framework/Core/Code/Editor/Editor_UI/Emortal_Editor_GUISkin.guiskin");
            return editorSkin;
        }

        public static GUISkin GetInspectorEditorUI()
        {
            GUISkin editorSkin = AssetDatabase.LoadAssetAtPath<GUISkin>("Assets/IndiePixel_Framework/Core/Code/Editor/Editor_UI/Emortal_Inspector_GUISkin.guiskin");
            return editorSkin;
        }

        public static Texture GetEmortalLogo()
        {
            Texture logo = AssetDatabase.LoadAssetAtPath<Texture>("Assets/IndiePixel_Framework/Core/Code/Editor/Editor_UI/Textures/emortal_logo_editor.png");
            return logo;
        }



        //Draw the Builtin Icons for a given Object
        public static void DrawIcon(GameObject go, int idx)
        {
            var largeIcons = GetTextures("sv_label_", string.Empty, 0, 8);
            var icon = largeIcons[idx];
            var egu = typeof(EditorGUIUtility);
            var flags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
            var args = new object[]{go, icon.image};
            var setIcon = egu.GetMethod("SetIconForObject", flags, null, new Type[]{typeof(UnityEngine.Object), typeof(Texture2D)}, null);
            setIcon.Invoke(null, args);
        } 



        //Get all the built in Gizmos from Unity
        public static GUIContent[] GetTextures(string basename, string postfix, int startIndex, int count)
        {
            GUIContent[] contentArray = new GUIContent[count];
            for(int i = 0; i < count; i++)
            {
                contentArray[i] = EditorGUIUtility.IconContent(basename + (startIndex + i) + postfix);
            }
            return contentArray;
        }
    }
}
