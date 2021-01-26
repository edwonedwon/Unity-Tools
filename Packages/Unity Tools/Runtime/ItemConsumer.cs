using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Edwon.Tools
{
    // currently, items being held will not be consumed by this
    public class ItemConsumer : MonoBehaviour, ITriggerReceiver
    {
        public string itemNameFilter;
        [HideInInspector]
        public Item itemReadyToConsume;
        public UnityEventItem onReadyToConsume;

        void OnReadyToConsume(Item itemToConsume)
        {
            onReadyToConsume.Invoke(itemToConsume);
        }

        Item GetItemFromCollider(Collider collider)
        {
            if (collider.tag == Item.itemTag 
            || collider.transform.root.tag == Item.itemTag)
            {
                Item item = collider.GetComponentInParent<Item>();
                if (item != null)
                {
                    if (!item.consumable) // if not consumable dont consume it
                        return null;

                    IHoldable holdable = item.GetComponent<IHoldable>();
                    if (holdable == null) // if no holdable
                    {
                        return item;
                    }
                    else if (holdable.holder == null) // else if item not being held
                    {
                        return item;
                    }
                    // else item is currently being held so don't return it
                }
            }
            return null;
        }

        public void OnTriggerEnter(Collider collider)
        {
            itemReadyToConsume = GetItemFromCollider(collider);
            if (itemReadyToConsume != null)
                OnReadyToConsume(itemReadyToConsume);
        }

        public void OnTriggerStay(Collider collider) {}

        public void OnTriggerExit(Collider collider) {}

    }
}