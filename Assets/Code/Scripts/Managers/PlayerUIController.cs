using System.Collections.Generic;
using UnityEngine;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private BookAnimations popupMenu;
    [SerializeField] private GameObject menuContent;
    [SerializeField] private GameObject notBookUI;
    [SerializeField] private List<BookmarksSetOpacity> bookmarksList;
    
    
    private InventoryDisplay _playerInventory;
    private CraftDisplay _playerCrafting;
    private EquipmentDisplay _playerEquipment;
    private QuestDisplay _playerQuests;
    private SettingsDisplay _playerSettings;

    private int _currentFilter;
    private int _lengthFilter = 2; // 0=Inventory 1=Crafting 2=Equipment 3=Quests 4=Menu

    private void Awake()
    {
        _playerInventory = GetComponent<InventoryDisplay>();
        _playerCrafting = GetComponent<CraftDisplay>();
        _playerEquipment = GetComponent<EquipmentDisplay>();
        _playerQuests = GetComponent<QuestDisplay>();
        _playerSettings = GetComponent<SettingsDisplay>();
        popupMenu.OnFullOpenComplete += FullOpenHandler;
        popupMenu.OnFullCloseComplete += FullCloseHandler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            AudioManager.Instance.PlayInventorySound();
            ScreenBg();
            ShowInventory();
            _currentFilter = 0;
            SetBookmarkOpacity(0);
        }
    }

    private void ScreenBg()
    {
        if (popupMenu.IsAnimating) return;
        var ena = popupMenu.gameObject.activeInHierarchy;
        if (!ena)
        {
            popupMenu.gameObject.SetActive(true);
            notBookUI.gameObject.SetActive(false);
            popupMenu.FullBookOpen();
        }
        else
        {
            menuContent.SetActive(false);
            popupMenu.FullBookClose();
        }
    }

    private void FullOpenHandler()
    {
        menuContent.SetActive(true);
    }
    private void FullCloseHandler()
    {
        popupMenu.gameObject.SetActive(false);
        notBookUI.gameObject.SetActive(true);
    }

    private void ShowInventory()
    {
        _playerInventory.Show();
        _playerCrafting.Hide();
        _playerEquipment.Hide();
        _playerQuests.Hide();
        _playerSettings.Hide();
    }

    private void ShowCrafting()
    {
        _playerInventory.Hide();
        _playerCrafting.Show();
        _playerEquipment.Hide();
        _playerQuests.Hide();
        _playerSettings.Hide();
    }

    private void ShowEquipment()
    {
        _playerInventory.Hide();
        _playerCrafting.Hide();
        _playerEquipment.Show();
        _playerQuests.Hide();
        _playerSettings.Hide();
    }
    
    private void ShowQuestLog()
    {
        _playerInventory.Hide();
        _playerCrafting.Hide();
        _playerEquipment.Hide();
        _playerQuests.Show();
        _playerSettings.Hide();
    }
    
    private void ShowSettings()
    {
        _playerInventory.Hide();
        _playerCrafting.Hide();
        _playerEquipment.Hide();
        _playerQuests.Hide();
        _playerSettings.Show();
    }

    [ContextMenu("NextFilter")]
    public void NextFilter()
    {
        _currentFilter++;
        if (_currentFilter > _lengthFilter)
        {
            _currentFilter = 0;
        }

        SwitchUi();
    }

    [ContextMenu("PrevFilter")]
    public void PreviousFilter()
    {
        _currentFilter--;
        if (_currentFilter < 0)
        {
            _currentFilter = _lengthFilter;
        }

        SwitchUi();
    }

    public void SetFilter(int filter)
    {
        _currentFilter = filter;
        SwitchUi();
        SetBookmarkOpacity(filter);
    }

    private void SwitchUi()
    {
        switch (_currentFilter)
        {
            case 0:
                ShowInventory();
                break;

            case 1:
                ShowCrafting();
                break;

            case 2:
                ShowEquipment();
                break;
            
            case 3:
                ShowQuestLog();
                break;
            
            case 4:
                ShowSettings();
                break;
            
            default:
                ShowInventory();
                break;
        }
    }

    private void SetBookmarkOpacity(int index)
    {
        for (int i = 0; i < bookmarksList.Count; i++)
        {
            if (i == index)
            {
                bookmarksList[i].Lighten();
            }
            else
            {
                bookmarksList[i].Darken();
            }
        }
    }
}