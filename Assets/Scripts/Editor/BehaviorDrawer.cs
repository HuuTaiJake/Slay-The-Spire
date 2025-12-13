#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;

[CustomPropertyDrawer(typeof(Behavior), true)]
public class BehaviorDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        // Header height
        float height = EditorGUIUtility.singleLineHeight;

        if (property.managedReferenceValue != null && property.isExpanded)
        {
            height += EditorGUIUtility.standardVerticalSpacing;

            var end = property.GetEndProperty();
            var child = property.Copy();

            // Enter children
            if (child.NextVisible(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(child, end))
                        break;

                    height += EditorGUI.GetPropertyHeight(child, true)
                              + EditorGUIUtility.standardVerticalSpacing;
                }
                while (child.NextVisible(false));
            }
        }

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // ===== Header =====
        var headerRect = new Rect(
            position.x,
            position.y,
            position.width,
            EditorGUIUtility.singleLineHeight
        );

        string typeName = property.managedReferenceValue != null
            ? property.managedReferenceValue.GetType().Name
            : "None";

        string labelText = label.text;

        // Optional cleanup for "Element X"
        if (label.text.StartsWith("Element"))
        {
            // labelText = ...
        }

        // Foldout
        var labelRect = new Rect(
            headerRect.x,
            headerRect.y,
            headerRect.width - 80,
            headerRect.height
        );

        property.isExpanded = EditorGUI.Foldout(
            labelRect,
            property.isExpanded,
            $"{labelText} ({typeName})",
            true
        );

        // Button
        var buttonRect = new Rect(
            headerRect.x + headerRect.width - 75,
            headerRect.y,
            75,
            headerRect.height
        );

        if (GUI.Button(
                buttonRect,
                property.managedReferenceValue == null ? "Select" : "Change"))
        {
            ShowBehaviorMenu(property);
        }

        // ===== Draw Children =====
        if (property.managedReferenceValue != null && property.isExpanded)
        {
            EditorGUI.indentLevel++;

            var drawRect = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight
                           + EditorGUIUtility.standardVerticalSpacing,
                position.width,
                EditorGUIUtility.singleLineHeight
            );

            var end = property.GetEndProperty();
            var child = property.Copy();

            // Enter children
            if (child.NextVisible(true))
            {
                do
                {
                    if (SerializedProperty.EqualContents(child, end))
                        break;

                    float h = EditorGUI.GetPropertyHeight(child, true);
                    drawRect.height = h;

                    EditorGUI.PropertyField(drawRect, child, true);
                    drawRect.y += h + EditorGUIUtility.standardVerticalSpacing;
                }
                while (child.NextVisible(false));
            }

            EditorGUI.indentLevel--;
        }

        EditorGUI.EndProperty();
    }

    private void ShowBehaviorMenu(SerializedProperty property)
    {
        var menu = new GenericMenu();

        // None option
        menu.AddItem(new GUIContent("None"), false, () =>
        {
            property.serializedObject.Update();
            property.managedReferenceValue = null;
            property.serializedObject.ApplyModifiedProperties();
        });

        // Find all Behavior types
        var types = AppDomain.CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t =>
                typeof(Behavior).IsAssignableFrom(t) &&
                !t.IsAbstract &&
                t != typeof(Behavior)
            );

        foreach (var type in types)
        {
            string menuPath = type.Name;

            menu.AddItem(new GUIContent(menuPath), false, () =>
            {
                property.serializedObject.Update();
                property.managedReferenceValue = Activator.CreateInstance(type);
                property.serializedObject.ApplyModifiedProperties();
            });
        }

        menu.ShowAsContext();
    }
}
#endif
