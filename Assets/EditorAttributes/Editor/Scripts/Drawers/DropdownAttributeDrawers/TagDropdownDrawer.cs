using UnityEditor;
using UnityEditorInternal;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace EditorAttributes.Editor
{
	[CustomPropertyDrawer(typeof(TagDropdownAttribute))]
    public class TagDropdownDrawer : PropertyDrawerBase
    {
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
            var root = new VisualElement();

            if (property.propertyType == SerializedPropertyType.String)
            {
                var tagField = new TagField(property.displayName, DoesStringValueContainTag(property.stringValue) ? property.stringValue : "Untagged") 
                {
                    showMixedValue = property.hasMultipleDifferentValues, 
                    tooltip = property.tooltip
                };

				tagField.AddToClassList(BaseField<VoidStructure>.alignedFieldUssClassName);

				tagField.RegisterValueChangedCallback(callback =>
                {
                    property.stringValue = tagField.value;
                    property.serializedObject.ApplyModifiedProperties();
                });

                root.Add(tagField);

                ExecuteLater(tagField, () => tagField.Q(className: TagField.inputUssClassName).style.backgroundColor = EditorExtension.GLOBAL_COLOR / 2f);
            }
            else
            {
                root.Add(new HelpBox("The TagDropdown attribute can only be attached to string fields", HelpBoxMessageType.Error));
            }

            return root;
		}

        private bool DoesStringValueContainTag(string stringValue)
        {
            foreach (var tag in InternalEditorUtility.tags)
            {
                if (stringValue == tag) 
                    return true;
            }

            return false;
        }
    }
}
