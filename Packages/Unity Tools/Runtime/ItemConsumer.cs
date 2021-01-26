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
        public bool doNotConsumeIfHeld = false;
        public bool doNotConsumeIfDragged = false;
        public bool onlyConsumeIfDragged = false;
        public UnityEventItem onReadyToConsume;

        // SCRATCH
        Item itemScratch = null;
        IHoldable holdableScratch = null;
        IDraggable draggableScratch = null;

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
                    // is consumable filter
                    if (!itemScratch.consumable) // if not consumable dont consume it
                        return null;

                    // name filter
                    if (!itemNameFilter.IsWhiteSpaceOnly()) // if name filter is not null
                        if (itemScratch.itemName != itemNameFilter) // if name filter doesn't match, don't consume it
                            return null;

                    bool canConsume = false;

                    // isHeld filter
                    bool isHeld = false;
                    holdableScratch = itemScratch.GetComponent<IHoldable>();
                    if (holdableScratch != null)
                    {
                        isHeld = holdableScratch.IsHeld;
                        if (doNotConsumeIfHeld && !isHeld)
                            canConsume = true;
                    }

                    // isDragged filter
                    bool isDragged = false;
                    draggableScratch = itemScratch.GetComponent<IDraggable>();
                    if (draggableScratch != null)
                    {
                        isDragged = draggableScratch.IsDragged;
                        if (doNotConsumeIfDragged && !isDragged)
                            canConsume = true;
                        if (onlyConsumeIfDragged && isDragged)
                            canConsume = true;
                    }

                    if (canConsume)
                        return itemScratch;
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