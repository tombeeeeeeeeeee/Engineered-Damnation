using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderList : PickUp
{
    public Transform attachPosition;
    protected Order childOrder;
    public bool hasBeenPinned = false;
    public Vector3 pinnedPosition = Vector3.zero;

    private void Update()
    {
        if (!hasBeenAlt && idealParent != null)
            transform.rotation = Quaternion.LookRotation(idealParent.forward, idealParent.up);
        else if(hasBeenPinned)
            transform.position = pinnedPosition;
    }

    /// <summary>
    /// Adds Demon to a todo list
    /// </summary>
    /// <param name="demonKey"></param>
    /// <param name="demonDescription"></param>
    /// <param name="childOrderPrefab"></param>
    /// <returns></returns>
    public OrderList AddToList(uint demonKey, string demonDescription, Order childOrderPrefab)
    {
        if(childOrder == null)
        {
            childOrder = Instantiate(childOrderPrefab, attachPosition, false);
            childOrder.Initialise(demonKey, demonDescription);
            return childOrder;
        }
        else
            return childOrder.AddToList(demonKey, demonDescription,childOrderPrefab);
    }

    /// <summary>
    /// Check off a completed demon from the the todo list
    /// </summary>
    /// <param name="demonKey"></param>
    /// <returns>True if a demon is found, and false if not.</returns>
    public virtual bool CheckOffDemon(uint demonKey)
    {
        if(childOrder != null)
            return childOrder.CheckOffDemon(demonKey);
        else
            return false;
    }


    public void ExpandGrabArea()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if(collider != null)
        {
            collider.center -= new Vector3(0, 0.033f, 0);
            collider.size += new Vector3(0, 0.066f, 0);
        }
    }

    public override void PickedUp()
    {
        hasBeenPinned = false;
        pinnedPosition = Vector3.zero;
        if(transform.parent && transform.parent.parent && transform.parent.parent.gameObject.GetComponent<DemonListSpawner>())
        {
            transform.parent.parent.gameObject.GetComponent<DemonListSpawner>().curList = null;
            transform.parent = null;
        }

        base.PickedUp();
    }

    public override void Dropped()
    {
        base.Dropped();
        GetComponent<Rigidbody>().useGravity = true;
    }
}
