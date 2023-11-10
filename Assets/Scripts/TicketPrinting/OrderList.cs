using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderList : PickUp
{
    [SerializeField] protected Transform attachPosition;
    protected Order childOrder;

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

    public virtual bool CheckOffDemon(uint demonKey)
    {
        if(childOrder != null)
            return childOrder.CheckOffDemon(demonKey);
        else
            return false;
    }

    public void MoveUp(float speed)
    {
        if(transform.parent != null)
        {
            if (transform.parent.GetComponent<OrderList>() != null)
                transform.parent.GetComponent<OrderList>().MoveUp(speed);
            else
                transform.localPosition += Vector3.up * speed * Time.deltaTime;
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
        base.PickedUp();
        transform.rotation = Quaternion.identity;
    }
}
