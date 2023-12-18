using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Wintrigger : MonoBehaviour
{
    [SerializeField] private Quest winQuestSO;
    
    private void OnTriggerStay2D(Collider2D other)
    {
        var questComplete = GameManager.Instance.QuestManager.IsQuestComplete(winQuestSO);
        if (questComplete)
        {
            GameManager.Instance.GameWin();
        }
    }
}
