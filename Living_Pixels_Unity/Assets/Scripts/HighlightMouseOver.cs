using UnityEngine;
using System.Collections;

public class HighlightMouseOver : MonoBehaviour 
{
    private Color startcolor;
    private Color mouseOverColour = Color.red;

    void OnMouseEnter()
    {
        startcolor = renderer.material.color;
        this.renderer.material.color = mouseOverColour;
    }


    void OnMouseExit()
    {
        renderer.material.color = startcolor;
    }
}
