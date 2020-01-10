using UnityEditor;
using UnityEngine;

namespace IndiePixel.Core
{
    public class IP_Menus 
    {
        //#region Core Menus
        //[MenuItem("Indie Pixel/Designer Dashboard")]
        //public static void LaunchDesignerDashboard()
        //{
        //    IP_DesignerDash_Window.InitDesignerDashboard();
        //}
        //#endregion



        #region Project Tools Menus
        [MenuItem("Indie Pixel/Project Tools/Create Project Folders")]
        public static void CreateProjectFolders()
        {
            IP_ProjectFolders_Window.InitWindow();
        }
        #endregion




        #region Scene Helpers Menus
        [MenuItem("Indie Pixel/Scene Tools/Create Game Manager")]
        public static void CreateGameManager()
        {
            IP_Scene_Helpers.CreateGameManager();
        }

        [MenuItem("GameObject/Indie-Pixel/Create Game Manager", false, 11)]
        public static void ContextCreateGameManager()
        {
            IP_Scene_Helpers.CreateGameManager();
        }




        [MenuItem("Indie Pixel/Scene Tools/Group Selected")]
        public static void GroupSelectedGameObjects()
        {
            IP_Grouping_Window.InitWindow();
        }

        [MenuItem("GameObject/Indie-Pixel/Grouping", false, 11)]
        public static void ContextGrouping()
        {
            IP_Grouping_Window.InitWindow();
        }



        [MenuItem("Indie Pixel/Scene Tools/Object Replacement")]
        public static void ReplaceSelectedGameObjects()
        {
            IP_ObjectReplacer_Window.InitWindow();
        }

        [MenuItem("Indie Pixel/Scene Tools/Create Level Group")]
        public static void CreateLevelController()
        {
            IP_Scene_Helpers.CreateLevelGroup();
        }

        [MenuItem("GameObject/Indie-Pixel/Create Level Group", false, 11)]
        public static void ContextCreateLevelController()
        {
            IP_Scene_Helpers.CreateLevelGroup();
        }

        [MenuItem("Indie Pixel/Scene Tools/Create Inputs")]
        public static void CreateInputs()
        {
            IP_Scene_Helpers.CreateInputs();
        }

        [MenuItem("GameObject/Indie-Pixel/Create Global Input", false, 11)]
        public static void ContextCreateInputs()
        {
            IP_Scene_Helpers.CreateInputs();
        }
        #endregion



        #region Level Tools
        [MenuItem("Indie Pixel/Level Tools/Vertex Painter")]
        public static void LaunchVertexPainter()
        {
            IP_VertexPainter_Tool.LaunchVertexPainter();
        }

        [MenuItem("Indie Pixel/Level Tools/Export Selected to Single OBJ")]
        public static void ExportSelectedToOBJ()
        {
            IP_OBJ_Export.ExportWholeSelectionToSingle();
        }

        [MenuItem("Indie Pixel/Level Tools/Export Each Selected to OBJ")]
        public static void ExportAllToOBJ()
        {
            IP_OBJ_Export.ExportEachSelectionToSingle();
        }
        #endregion



       



        #region UI Helpers
        [MenuItem("Indie Pixel/UI Tools/Create UI Canvas Grp")]
        public static void CreateUICanvasGroup()
        {
            IP_UI_Helpers.CreateUIGroup();
        }
        #endregion

    }
}
