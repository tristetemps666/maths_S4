using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class BlinkText : MonoBehaviour
{
    public TextMeshPro textComponent;
    public float blinkInterval = 0.5f;

    private void Start()
    {
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled;
            
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}