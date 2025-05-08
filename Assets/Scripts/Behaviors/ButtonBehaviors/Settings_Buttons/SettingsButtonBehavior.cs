using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SettingsButtonBehavior : MonoBehaviour
{
    [SerializeField] Slider[] connectors;
    public float connectorValueTarget;
    public float fillSpeed = 100f;

    private int connectorNum;
    private bool isActive;

    private void Awake()
    {
        isActive = false;
    }

    private void Update()
    {
        for (connectorNum = 0; connectorNum < connectors.Length; connectorNum++)
        {
            if (isActive)
            {
                connectors[connectorNum].value = Mathf.Lerp(connectors[connectorNum].value, connectorValueTarget, Time.deltaTime * fillSpeed);
            }
        }
    }

    public void IncreaseFill()
    {
        StartCoroutine(FillConnectors());
    }

    public void DecreaseFill()
    {
        StartCoroutine(UnfillConnectors());
    }

    IEnumerator FillConnectors()
    {
        isActive = true;
        Debug.Log("FILL");
        connectorValueTarget = 1;
        while (connectorNum <= 6)
        {
            connectorNum++;
            yield return new WaitForSeconds(.2f);
        }
        isActive = false;
    }

    IEnumerator UnfillConnectors()
    {
        isActive = true;
        connectorValueTarget = 0;
        while (connectorNum >= 0)
        {
            connectorNum--;
            yield return new WaitForSeconds(.2f);
        }
        isActive = false;
    }
}
