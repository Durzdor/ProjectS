using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_QuestPrefab : MonoBehaviour, IPointerEnterHandler
{
    [Header("Button")] [Space(5)] 
    [SerializeField] private TextMeshProUGUI buttonName;

    private Quest _myQuest;
    
    public event Action<Quest> OnInteraction;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        OnInteraction?.Invoke(_myQuest);
    }

    public void SetupButton(Quest quest)
    {
        _myQuest = quest;
        buttonName.text = quest.QuestName;
    }
}
