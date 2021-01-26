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
        public UnityEventItem onReadyToConsume;
        Item itemScratch = null;
        IHoldable holdableScratch = null;

        void OnReadyToConsume(Item itemToConsume)
        {
            onReadyToConsume.Invoke(itemToConsume);
        }

        Item GetItemFromCollider(Collider collider)
        {
            if (collider.tag == Item.itemTag 
            || collider.transform.root.tag == Item.itemTag)
            {
                itemScratch = collider.GetComponentInParent<Item>();
                if (itemScratch != null)
                {
                    if (!itemScratch.consumable) // if not consumable dont consume it
                        return null;

                    if (!itemNameFilter.IsWhiteSpaceOnly()) // if name filter is not null
                        if (itemScratch.itemName != itemNameFilter) // if name filter doesn't match, don't consume it
                            return null;

                    IHoldable holdableScratch = itemScratch.GetComponent<IHoldable>();
                    if (holdableScratch == null) // if no holdable
                    {
                        return itemScratch;
                    }
                    else if (holdableScratch.holder == null) // else if item not being held
                    {
                        return itemScratch;
                    }
                    // else item is currently being held so don't return it
                }
            }
            return null;
        }

        public void OnTriggerEnter(Collider collider)
        {
            itemScratch = GetItemFromCollider(collider);
            if (itemScratch != null)
                OnReadyToConsume(itemScratch);
        }

        public void OnTriggerStay(Collider collider) {}

        public void OnTriggerExit(Collider collider) {}

    }
}