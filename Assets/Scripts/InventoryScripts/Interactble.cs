using System.Collections.Generic;
using UnityEngine;

public class Interactble : MonoBehaviour
{
    [Header("Colider sphere radius")]
    [SerializeField] private float _radius = 2.3f;
    [SerializeField] List<GameObject> _interactbleObjects;

    private SphereCollider _collider;
    private ItemPickUp itemPickUp;
    private GameObject FocusedItem;
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        _collider.radius = _radius;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RemoveFirstItemOnList();
        }
    }
    public void RemoveFirstItemOnList()
    {
        if (_interactbleObjects.Count != 0)
        {
            FocusedItem = _interactbleObjects[0];
            itemPickUp = FocusedItem.GetComponent<ItemPickUp>();
            Inventory.Instance.Add(itemPickUp.Item);
            Destroy(FocusedItem);
            _interactbleObjects.Remove(FocusedItem);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Item"))
        {
            _interactbleObjects.Add(col.gameObject); ;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Item"))
        {
            _interactbleObjects.Remove(col.gameObject);
        }
    }
}
