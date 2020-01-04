using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorStatusUtil
{
   public static Color GetColor(ColorStatus cs)
    {
        Color c = new Color();


        switch (cs)
        {
            case (ColorStatus.cyan):
                c = Color.cyan;
                break;
            case (ColorStatus.magenta):
                c = Color.magenta;
                break;
            case (ColorStatus.yellow):
                c = Color.yellow;
                break;
            case (ColorStatus.k):
                c = Color.black;
                break;
        }

        return c;

    }
}
