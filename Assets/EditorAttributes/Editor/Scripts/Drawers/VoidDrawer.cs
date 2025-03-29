using UnityEditor;
using UnityEngine.UIElements;

namespace EditorAttributes.Editor
{
    [CustomPropertyDrawer(typeof(VoidStructure))]
    public class VoidDrawer : PropertyDrawer
    {
		public override VisualElement CreatePropertyGUI(SerializedProperty property) => new();
	}
}
