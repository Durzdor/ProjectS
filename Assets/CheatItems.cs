using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatItems : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _items;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
            foreach (var item in _items)
                item.SetActive(true);
    }
}
