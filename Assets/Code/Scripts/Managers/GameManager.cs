using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton

    public static GameManager Instance;

    private void MakeSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion Singleton

    public ItemDatabase ItemDatabase { get; private set; }
    public CraftDatabase CraftDatabase { get; private set; }
    public EquipmentDatabase EquipDatabase { get; private set; }
    public PlayerInventory PlayerInventory { get; private set; }
    public PopupMessageManager PopupManager { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public PlayerModel Player { get; private set; }
    public PlayerHealthBar PlayerHealthBar { get; private set; }
    public QuestManager QuestManager { get; private set; }
    public KillCountManager KillCountManager { get; private set; }
    public Transform PlayerTransform { get; set; }

    

    private void Awake()
    {
        MakeSingleton();
        LoadComponents();
        QuestManager.ResetAllQuests();
    }

    private void Start()
    {
        AudioManager.Instance.PlayAmbienceMusic();
    }

    private void LoadComponents()
    {
        ItemDatabase = GetComponent<ItemDatabase>();
        CraftDatabase = GetComponent<CraftDatabase>();
        EquipDatabase = GetComponent<EquipmentDatabase>();
        PlayerInventory = GetComponent<PlayerInventory>();
        PopupManager = GetComponent<PopupMessageManager>();
        GameStateManager = GetComponent<GameStateManager>();
        //Player = FindObjectOfType<PlayerModel>();
        PlayerHealthBar = GetComponent<PlayerHealthBar>();
        QuestManager = GetComponent<QuestManager>();
        KillCountManager = GetComponent<KillCountManager>();
    }

    public void GameOver()
    {
        //_GOPanel.SetActive(true)
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }

    public void GameWin()
    {
        //_winPanel.SetActive(true);
        SceneManager.LoadScene("WinScene");
        Destroy(gameObject);
    }

    public void PlayerSetup(PlayerModel pModel)
    {
        Player = pModel;
    }
}