using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    public static ItemDatabase Instance;
    private void Awake()
    {
        Instance = this;
    }

    public List<Item> itemDB = new List<Item>();

    public GameObject itemPrefab;
    public Vector3[] pos;

    // Test
    private void Start()
    {
        for (int i = 0; i < itemDB.Count; i++)
        {
            GameObject go = Instantiate(itemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<FieldItems>().SetItem(itemDB[Random.Range(0, itemDB.Count)]);
        }
    }

}
