using UnityEngine;

public class TargetBaseModelSelect : MonoBehaviour
{
    public GameObject selectedModel;
    public GameObject[] modelOptions;

    public GameObject SelectModel(eSide _side)
    {
        switch (_side)
        {
            case eSide.left:
                selectedModel = modelOptions[0];
                break;
            case eSide.right:
                selectedModel = modelOptions[1];
                break;
            case eSide.any:
                break;
            case eSide.both:
                selectedModel = modelOptions[2];
                break;
            default:
                break;
        }
        selectedModel.SetActive(true);
        return selectedModel;
    }
}
