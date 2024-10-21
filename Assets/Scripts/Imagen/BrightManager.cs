using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightManager : MonoBehaviour
{
    public Image panelBrillo;
    public Scrollbar brilloScrollbar;
    public void BrightChange()
    {
        panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, brilloScrollbar.value);
    }
}
