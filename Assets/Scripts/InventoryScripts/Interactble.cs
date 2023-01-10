using System.Collections.Generic;
using UnityEngine;

public class Interactble : MonoBehaviour
{
    [Header("Colider sphere radius")]
    [SerializeField] private float radius = 2.3f;
    [SerializeField] List<GameObject> interactbleObjects;

    private SphereCollider collider;
    private ItemPickUp itemPickUp;
    private GameObject focusedItem;
    public StorageController storage;

    void Start()
    {
        collider = GetComponent<SphereCollider>();
        collider.radius = radius;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
	        if (interactbleObjects != null)
	        {
		        RemoveFirstItemOnList();
			}
	        if (storage != null)
	        {
                storage.Move();
	        }
        }
    }
    public void RemoveFirstItemOnList()
    {
        if (interactbleObjects.Count != 0)
        {
            focusedItem = interactbleObjects[0];
            itemPickUp = focusedItem.GetComponent<ItemPickUp>();
            Inventory.Instance.Add(itemPickUp.Item);
            Destroy(focusedItem);
            interactbleObjects.Remove(focusedItem);
        }
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Item"))
        {
            interactbleObjects.Add(col.gameObject);
        }
        if (col.CompareTag("Storage"))
        {
	        storage = col.gameObject.GetComponent<StorageController>();
        }
	}
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Item"))
        {
            interactbleObjects.Remove(col.gameObject);
        }
        if (col.CompareTag("Storage"))
        {
	        storage = null;
        }
	}
}
