using UnityEditor;
using UnityEditorInternal;

//[CustomEditor(typeof(Character))]
//[CanEditMultipleObjects]
//public class CharacterEditor : Editor {
//    private SerializedProperty moodProp;
//    private SerializedProperty emotionProp;

//    private void OnEnable() {
//        moodProp = serializedObject.FindProperty("moodRing");
//        emotionProp = serializedObject.FindProperty("emotion");

//    }

//    public override void OnInspectorGUI() {
//        this.serializedObject.Update();

//        EditorGUILayout.PropertyField(moodProp);
//        EditorGUILayout.PropertyField(emotionProp);


//        this.serializedObject.ApplyModifiedProperties();
//    }
//}