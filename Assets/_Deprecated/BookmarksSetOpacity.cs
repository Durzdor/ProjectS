using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookmarksSetOpacity : MonoBehaviour
{
    [SerializeField] private List<Image> images =new ();

    [ContextMenu("dark")]
    public void Darken()
    {
        var _dark = new Color32(150, 150, 150,255);
        foreach (var im in images)
        {
            im.color = _dark;
        }
    }

    [ContextMenu("light")]
    public void Lighten()
    {
        var _light = new Color32(255, 255, 255,255);
        foreach (var im in images)
        {
            im.color = _light;
        }
    }
}
