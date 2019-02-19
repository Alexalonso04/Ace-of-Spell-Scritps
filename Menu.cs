using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string lastTooltip = " ";

    void OnGUI()
    {
              // This box is larger than many elements following it, and it has a tooltip.
        GUI.Box(new Rect(5, 300 , 500, 75), new GUIContent("Box", "this box has a tooltip"));

        // This button is inside the box, but has no tooltip so it does not
        // override the box's tooltip.
        GUI.Button(new Rect(10, 55, 100, 20), "No tooltip here");

        // This button is inside the box, and HAS a tooltip so it overrides
        // the tooltip from the box.
        GUI.Button(new Rect(10, 80, 100, 20), new GUIContent("I have a tooltip", "The button overrides the box"));

        // finally, display the tooltip from the element that has
        // mouseover or keyboard focus
        GUI.Box(new Rect(10, 200, 300, 40), GUI.tooltip);
    }

    void Button1OnMouseOver()
    {
        Debug.Log("Play game got focus");
    }

    void Button2OnMouseOut()
    {
        Debug.Log("Quit lost focus");
    }
}
