using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowItem : MonoBehaviour
{


    [Range(1f, 10f)]
    [SerializeField] private float itemThrowForce = 2;

    [SerializeField] private GameObject itemToSpawn;
    [SerializeField] private ItemPickUp itemPickUp;
    [SerializeField] private Transform PlayerPostion;

    public void throwItem(Item item, int numberToDrop)
    {
        itemPickUp = itemToSpawn.GetComponent<ItemPickUp>();
        itemPickUp.Item = item;

        for (var i = 0; i < numberToDrop; ++i)
        {
            var itemInScene = Instantiate(itemToSpawn, new Vector3(PlayerPostion.position.x, PlayerPostion.position.y, PlayerPostion.position.z + 1), Quaternion.identity);
            var itemInSceneRb = itemInScene.GetComponent<Rigidbody>();
            var randomPostion = new Vector3(Random.Range(0f, 2f), Random.Range(0f, 3f), Random.Range(0f, 1.5f));
            var positonToThrow = (randomPostion - PlayerPostion.position).normalized;
            itemInSceneRb.AddForce(positonToThrow * itemThrowForce, ForceMode.Impulse);
        }
    }
}
