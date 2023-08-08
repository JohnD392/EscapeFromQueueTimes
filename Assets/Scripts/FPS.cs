using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour
{
    private float fps;
    private float ms;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            ms = Time.unscaledDeltaTime * 1000f;
            fps = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(5, Screen.height - 25, 200, 25), ms.ToString("F2") + " ms " + Mathf.Round(fps) + " fps");
    }
}