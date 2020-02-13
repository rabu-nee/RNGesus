using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(Character))]
[CanEditMultipleObjects]
public class CharacterEditor : Editor {
    private SerializedProperty moodProp;
    private SerializedProperty emotionProp;
    private ReorderableList relationships;

    private void OnEnable() {
        var property = this.serializedObject.FindProperty("relationships");
        moodProp = serializedObject.FindProperty("moodRing");
        emotionProp = serializedObject.FindProperty("emotion");

        this.relationships = ReorderableListUtility.CreateAutoLayout(property);
    }

    public override void OnInspectorGUI() {
        this.serializedObject.Update();

        EditorGUILayout.PropertyField(moodProp);
        EditorGUILayout.PropertyField(emotionProp);

        ReorderableListUtility.DoLayoutListWithFoldout(this.relationships);

        this.serializedObject.ApplyModifiedProperties();
    }
}