using UnityEditor;


namespace HelicopterPhysics.Inputs.Editorial
{
    [CustomEditor(typeof(KeyboardHeliInput))]
    public class KeyboardHeli_InputEditor : Editor
    {
        private KeyboardHeliInput _target;

        private void OnEnable()
        {
            _target = (KeyboardHeliInput)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawDebugLayout();
            Repaint();
        }

        private void DrawDebugLayout()
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Inputs", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Throttle : " + _target.ThrottleInput.ToString("0.00"));
            EditorGUILayout.LabelField("Collective : " + _target.CollectiveInput.ToString("0.00"));
            EditorGUILayout.LabelField("Cyclic : " + _target.CyclicInput.ToString("0.00"));
            EditorGUILayout.LabelField("Pedal : " + _target.PedalInput.ToString("0.00"));

            EditorGUILayout.EndVertical();
        }
    }
}
