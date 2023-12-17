using UnityEngine;

public class MissionIconBehaviour : MonoBehaviour
{
    private Transform Character;
    [SerializeField] private Transform Mission;
    private Vector2 _dir;
    private SpriteRenderer _sr;

   private void Start()
    {
        Character = GameManager.Instance.Player.transform;
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _dir = (Mission.position - Character.position);
        _sr.enabled = _dir.magnitude < 10 ? false : true;
        transform.position = Character.position + (Vector3)_dir.normalized * 10;
    }
}
