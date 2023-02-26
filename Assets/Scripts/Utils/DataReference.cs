using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace RTLOL
{
    [Serializable]
    public struct DataReference<TData>
    {
        public string Guid;
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(DataReference<>))] public class DataReferenceDrawerImpl : DataReferenceDrawer {}

    public abstract class DataReferenceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var dataType = fieldInfo.FieldType.GetGenericArguments()[0];

            EditorGUI.BeginProperty(position, label, property);
            {
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                var guidProperty = property.FindPropertyRelative("Guid");
                var guid = new GUID(guidProperty.stringValue);
                string assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var oldObj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(assetPath);

                var newObj = EditorGUI.ObjectField(position, oldObj, dataType, false);
                if (newObj != oldObj)
                {
                    assetPath = AssetDatabase.GetAssetPath(newObj);
                    guid = AssetDatabase.GUIDFromAssetPath(assetPath);
                    guidProperty.stringValue = guid.ToString();
                }
                guidProperty.serializedObject.ApplyModifiedProperties();
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}
