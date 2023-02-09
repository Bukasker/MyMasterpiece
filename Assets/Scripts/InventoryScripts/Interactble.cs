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
    private StorageController storage;
	public TraderScript trader;

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
            if(trader != null)
            {
                trader.StartTrade();
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
        if (col.CompareTag("Trader"))
        {
            trader = col.gameObject.GetComponent<TraderScript>();
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
		if (col.CompareTag("Trader"))
		{
            trader = null;
		}
	}
}
