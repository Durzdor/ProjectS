using UnityEngine;

public class SettingsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private GameObject controlsInfo;
    
    
    public void Show()
    {
        //Show stuff
        popupMenu.SetActive(true);
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }

    public void DisplayControls()
    {
        var ena = controlsInfo.activeInHierarchy;
        controlsInfo.SetActive(!ena);
    }
}
