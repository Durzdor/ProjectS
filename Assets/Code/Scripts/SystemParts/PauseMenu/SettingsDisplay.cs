using UnityEngine;

public class SettingsDisplay : MonoBehaviour
{
    [SerializeField] private GameObject popupMenu;
    
    
    public void Show()
    {
        //Show stuff
        popupMenu.SetActive(true);
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }
}
