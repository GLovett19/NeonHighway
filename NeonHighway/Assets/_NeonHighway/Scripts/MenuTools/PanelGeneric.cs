using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelGeneric : MonoBehaviour
{

    public GameObject FirstSelected;

    public virtual void ShowPanel()
    {
       // do the things to show this panel
    }
    public virtual void HidePanel()
    {
        // do the things to hide this panel
        gameObject.SetActive(false);
    }

}
