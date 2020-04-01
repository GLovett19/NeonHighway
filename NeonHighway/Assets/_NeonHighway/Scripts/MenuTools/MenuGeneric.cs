using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MenuGeneric : MonoBehaviour
{
    //Inpsector Assigned Components
    [Header("Inspector Assigned Components")]
    public PanelGeneric FirstPanel;
    public List<PanelGeneric> subPanels; // this is a list of all accessible sub panels in this scene from this menu. this is filled in the inspector and does not populate automatically

    //Auto Assigned components
    private EventSystem MyEventSystem;

    //Private Fields
    //None Just yet

    //Public Fields 
    [Header("Public Fields")]
    public string currScene;


    private void Awake()
    {
        MyEventSystem = FindObjectOfType<EventSystem>();
    }

    void Update()
    {
        if (MyEventSystem.currentSelectedGameObject == null)
        {
            MyEventSystem.SetSelectedGameObject(null);
            foreach (PanelGeneric panel in subPanels)
            {
                if (panel.isActiveAndEnabled)
                {
                    MyEventSystem.SetSelectedGameObject(panel.FirstSelected);
                    break;
                }
            }
        }
    }

    //Method to be initiate a scene change 
    public void SelectScene(string val)
    {
        ActiveSceneManager.UnloadScene(currScene);
        ActiveSceneManager.LoadScene(val, false);
    }

    // Method to show a menu panel
    public void ShowPanel(string val)
    {
        // check the panel name against all sub panels of this menu
        foreach (PanelGeneric panel in subPanels)
        {
            if (panel.gameObject.name == val)
            {
                panel.gameObject.SetActive(true);
                panel.ShowPanel();
                MyEventSystem.SetSelectedGameObject(panel.FirstSelected);
            }
            else
            {
                panel.HidePanel();
                panel.gameObject.SetActive(false);
            }
        }
    }
    public void HidePanel(string val)
    {
        foreach (PanelGeneric panel in subPanels)
        {
            if (panel.gameObject.name == val)
            {
                panel.HidePanel();
            }
        }
    }

    //Method to Exit the game 
    public void ExitGame()
    {
        // save data before quitting? 
        Application.Quit();
    }

}
