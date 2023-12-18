using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour
{
    // Aca hacemos otra pantalla dentro del menu de "pausa"
    // No se si quiero mostrarlo en el HUD tamb
    // No se si vamos a tener integracion con mapa/minimapa
    private QuestManager _questManager;
    [SerializeField] private GameObject popupMenu;
    [SerializeField] private UI_QuestPrefab questUIprefab;
    [SerializeField] private ScrollRect contentContainer;
    [SerializeField] private GameObject infoContainer;
    [SerializeField] private TextMeshProUGUI questIdentifier;
    [SerializeField] private TextMeshProUGUI questObjectives;
    [SerializeField] private TextMeshProUGUI questRewards;
    
    private List<UI_QuestPrefab> _itemsGenerated = new();

    
    
    private void Awake()
    {
        _questManager = GetComponent<QuestManager>();
        _questManager.OnQuestReceived += DisplayActiveQuests;
        _questManager.OnQuestReceived += RecieveQuestPopup;
        _questManager.OnQuestCompleted += DisplayCompletedQuests;
        _questManager.OnQuestCompleted += CompleteQuestPopup;
    }

    public void Show()
    {
        //Show stuff
        popupMenu.SetActive(true);
        FilterQuestLog();
    }

    public void Hide()
    {
        popupMenu.SetActive(false);
    }

    // Modo console debug
    private void DisplayActiveQuests(Quest q)
    {
        var msg = "";
        foreach (var quest in _questManager.ActiveQuests)
        {
            msg += $"Quest: {quest.QuestName} is Active \n";
        }

        Debug.Log(msg);
    }

    private void GenerateQuestPrefab(Quest quest)
    {
        
        var go = Instantiate(questUIprefab, contentContainer.content.transform, true);
        if (!_itemsGenerated.Contains(go))
        {
            go.OnInteraction += ShowDetails;
            _itemsGenerated.Add(go);
        }

        go.SetupButton(quest);

        //reset the item's scale -- this can get munged with UI prefabs
        go.transform.localScale = Vector2.one;
    }

    private void FilterQuestLog()
    {
        var beforeClear = _itemsGenerated.Count;
        foreach (var item in _itemsGenerated)
        {
            item.OnInteraction -= ShowDetails;
            Destroy(item.gameObject);
        }

        _itemsGenerated.Clear();
        var activeQuests = GameManager.Instance.QuestManager.ActiveQuests;
        for (int i = activeQuests.Count - 1; i >= 0; i--)
        {
            var quest = activeQuests.ElementAt(i);
            GenerateQuestPrefab(quest);
        }
        if (_itemsGenerated.Count != beforeClear)
        {
            ClearDetails();
        }
    }

    private void ShowDetails(Quest quest)
    {
        infoContainer.SetActive(true);
        questIdentifier.text = quest.QuestName;
        string objectivesMsg = "";
        foreach (var qObjective in quest.QuestObjectives)
        {
            objectivesMsg += $"\n {qObjective.QuestRequirementString()}";
        }

        questObjectives.text = objectivesMsg;
        var rewardsMsg = "";
        for (int i = 0; i < quest.QuestReward.ItemReward.Length; i++)
        {
            rewardsMsg += $"\n You get {quest.QuestReward.ItemReward[i].Identifier} x{quest.QuestReward.RewardQuantity[i]}";
        }
        
        questRewards.text = rewardsMsg;
    }

    private void ClearDetails()
    {
        infoContainer.SetActive(false);
    }

    // En quest manager cambie el evento a que siempre tire una quest, se puede sacar para el libro de quests
    private void RecieveQuestPopup(Quest q)
    {
        var msg = "";
        msg += $"Quest: {q.QuestName} received! \n";
        GameManager.Instance.PopupManager.ShowMessage(msg);
    }

    // Modo console debug
    private void DisplayCompletedQuests(Quest q)
    {
        var msg = "";
        foreach (var quest in _questManager.CompletedQuests)
        {
            msg += $"Quest: {quest.QuestName} completed! \n";
        }

        Debug.Log(msg);
    }

    private void CompleteQuestPopup(Quest q)
    {
        var msg = "";
        msg += $"Quest: {q.QuestName} completed! \n";
        GameManager.Instance.PopupManager.ShowMessage(msg);
    }
}