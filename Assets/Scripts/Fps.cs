//Simple FPS from https://gist.github.com/mstevenson/5103365

using UnityEngine;
using System.Collections;

public class Fps : MonoBehaviour
{
    private float count;

    private IEnumerator Start()
    {
        GUI.depth = 2;
        while (true)
        {
            count = 1f / Time.unscaledDeltaTime;
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnGUI()
    {
        Rect location = new Rect(5, 5, 85, 25);
        string text = $"FPS: {Mathf.Round(count)}";
        Texture black = Texture2D.linearGrayTexture;
        GUI.DrawTexture(location, black, ScaleMode.StretchToFill);
        GUI.color = Color.black;
        GUI.skin.label.fontSize = 18;
        GUI.Label(location, text);
    }
}
