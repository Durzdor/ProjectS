using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionIconBehaviour : MonoBehaviour
{
    public Transform Character;
    public Transform Mission;
    private Vector2 _dir;
    private SpriteRenderer _sr;

    void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
    }


    // Update is called once per frame
    void Update()
    {
        _dir = (Mission.position - Character.position);
        _sr.enabled = _dir.magnitude < 10 ? false : true;
        transform.position = Character.position + (Vector3)_dir.normalized * 10;
    }
}
