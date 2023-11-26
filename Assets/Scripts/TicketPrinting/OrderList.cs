using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderList : PickUp
{
    [SerializeField] protected Transform attachPosition;
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
            childOrder.transform.localScale = Vector3.one;
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

    /// <summary>
    /// Move tickets up out of the machine
    /// </summary>
    /// <param name="speed"></param>
    public void MoveUp(float speed)
    {
        if(transform.parent != null)
        {
            if (transform.parent.GetComponent<OrderList>() != null)
                transform.parent.GetComponent<OrderList>().MoveUp(speed);
            else
                transform.localPosition += Vector3.up * speed * Gameplay.deltaTime;
        }
    }

    public void ExpandGrabArea()
    {
        BoxCollider collider = GetComponent<BoxCollider>();
        if(collider != null)
        {
            collider.center -= new Vector3(0, 0.5f, 0);
            collider.size += new Vector3(0, 1, 0);
        }
    }

    public override void PickedUp()
    {
        hasBeenPinned = false;
        pinnedPosition = Vector3.zero;
        base.PickedUp();
    }

    public override void Dropped()
    {
        base.Dropped();
        GetComponent<Rigidbody>().useGravity = true;
    }
}
