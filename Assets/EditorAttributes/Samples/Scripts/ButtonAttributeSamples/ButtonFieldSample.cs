using UnityEngine;
using EditorAttributes;

namespace EditorAttributesSamples
{
	[HelpURL("https://editorattributesdocs.readthedocs.io/en/latest/Attributes/ButtonAttributes/buttonfield.html")]
	public class ButtonFieldSample : MonoBehaviour
	{
		[Header("ButtonField Attribute:")]

		[ButtonField(nameof(PrintNumber), buttonHeight: 30f)]
		[SerializeField] private VoidStructure buttonHolder;

		[SerializeField] private int number;

		[HorizontalGroup(true, nameof(buttonHolder01), nameof(buttonHolder02))] 
		[SerializeField] private VoidStructure groupHolder;

		[ButtonField(nameof(PrintMessage))]
		[SerializeField, HideInInspector] private VoidStructure buttonHolder01;

		[ButtonField(nameof(PrintMessage), true, 60, 300, "Hold Me")] 
		[SerializeField, HideInInspector] private VoidStructure buttonHolder02;

		private void PrintNumber() => print(number);
		private void PrintMessage() => print("Hello World!");
	}
}
