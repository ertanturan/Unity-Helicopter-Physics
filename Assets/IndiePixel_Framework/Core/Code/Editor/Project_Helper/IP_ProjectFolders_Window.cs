using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections;
using System.Collections.Generic;

namespace IndiePixel.Core
{
    public class IP_ProjectFolders_Window : IP_Base_Window 
    {
        #region Variables
        static IP_ProjectFolders_Window m_Win;

        string m_wantedRootName = "Game";
        string m_dialogName = "Project Setup";
        #endregion



        #region Main Methods
        public static void InitWindow()
        {
            m_Win = GetWindow<IP_ProjectFolders_Window>(true, "Project Folders", true);
            m_Win.Show();
        }

        void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Game Name: ", EditorStyles.boldLabel);
            m_wantedRootName = EditorGUILayout.TextField(m_wantedRootName);
            EditorGUILayout.EndHorizontal();

            if(GUILayout.Button("Create Folder Structure", GUILayout.ExpandWidth(true), GUILayout.Height(32)))
            {
                //Debug.Log(Application.dataPath);
                CreateRootFolder();
            }


            //Make sure we have the instance and repaint it
            if(m_Win)
            {
                m_Win.Repaint();
            }
        }
        #endregion


        #region Custom Methods
        void CreateRootFolder()
        {
            if(m_wantedRootName == "" || m_wantedRootName == null)
            {
                DialogDisplay("Please Provide a Proper Game Name");
                return;
            }

            if(m_wantedRootName == "Game")
            {
                DialogDisplay("Do you really want to name this game..Game?");
                return;
            }

            Debug.Log("Creating Root Folder...");
            string assetFolder = Application.dataPath;
            string rootName = assetFolder + "/" + m_wantedRootName;

            DirectoryInfo rootInfo = Directory.CreateDirectory(rootName);

            if(!rootInfo.Exists)
            {
                return;
            }
            CreatSubDirectories(rootName);

            AssetDatabase.Refresh();

            if(m_Win)
            {
                m_Win.Close();
            }
        }

        void CreatSubDirectories(string aRootFolder)
        {
            DirectoryInfo rootInfo = null;
            List<string> afolderList = new List<string>();

            rootInfo = Directory.CreateDirectory(aRootFolder + "/" + "Art");
            if(rootInfo.Exists)
            {
                afolderList.Clear();
                afolderList.Add("Animation");
                afolderList.Add("Audio");
                afolderList.Add("Fonts");
                afolderList.Add("Materials");
                afolderList.Add("Objects");
                afolderList.Add("Textures");

                CreateSubFolders(aRootFolder + "/" + "Art", afolderList); 
            }

            rootInfo = Directory.CreateDirectory(aRootFolder + "/" + "Code");
            if(rootInfo.Exists)
            {
                afolderList.Clear();
                afolderList.Add("Editor");
                afolderList.Add("Scripts");
                afolderList.Add("Shaders");
                CreateSubFolders(aRootFolder + "/" + "Code", afolderList);
            }

            rootInfo = Directory.CreateDirectory(aRootFolder + "/" + "Resources");
            if(rootInfo.Exists)
            {
                afolderList.Clear();
                afolderList.Add("Characters");
                afolderList.Add("Managers");
                afolderList.Add("Props");
                afolderList.Add("UI");
                CreateSubFolders(aRootFolder + "/" + "Resources", afolderList);
            }

            rootInfo = Directory.CreateDirectory(aRootFolder + "/" + "Prefabs");
            if(rootInfo.Exists)
            {
                afolderList.Clear();
                afolderList.Add("Characters");
                afolderList.Add("Props");
                afolderList.Add("UI");
            }

            DirectoryInfo sceneDir = Directory.CreateDirectory(aRootFolder + "/" + "Scenes");
            //Debug.Log(sceneDir.FullName);

            //Create the Base Level Scenes needed for a simple game
            Scene curFrontendScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(curFrontendScene, "Assets/" + m_wantedRootName + "/Scenes/" + m_wantedRootName + "_Frontend.unity", true);

            Scene curMainScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(curMainScene, "Assets/" + m_wantedRootName + "/Scenes/" + m_wantedRootName + "_Main.unity", true);

            Scene curStartupScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);
            EditorSceneManager.SaveScene(curStartupScene, "Assets/" + m_wantedRootName + "/Scenes/" + m_wantedRootName + "_Startup.unity", true);

        }

        void CreateSubFolders(string aRootFolder, List<string> subFolders)
        {
            foreach(string aFolder in subFolders)
            {
                Directory.CreateDirectory(aRootFolder + "/" + aFolder);
            }
        }

        void DialogDisplay(string aMessage)
        {
            EditorUtility.DisplayDialog(m_dialogName + "Warning", aMessage, "OK");
        }
        #endregion
    }
}
