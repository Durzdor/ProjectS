using UnityEngine;

public class MissionIconBehaviour : MonoBehaviour
{
    [SerializeField] private Transform Mission;
    private Vector2 _dir;
    private SpriteRenderer _sr;

   private void Start()
    {
         
        _sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        _dir = (Mission.position - GameManager.Instance.PlayerTransform.position);
        _sr.enabled = _dir.magnitude < 10 ? false : true;
        transform.position = GameManager.Instance.PlayerTransform.position + (Vector3)_dir.normalized * 9;
    }
}
