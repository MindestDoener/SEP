using UnityEngine;

public class UpgradePanelController : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.FindWithTag("CircleRenderer").GetComponent<CircleRendererController>().Rendered = true;
    }

    private void OnDisable()
    {
        GameObject.FindWithTag("CircleRenderer").GetComponent<CircleRendererController>().Rendered = false;
    }
}