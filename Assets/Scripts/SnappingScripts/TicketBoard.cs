using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicketBoard : SnappingGameObject
{
    private void OnTriggerStay(Collider other)
    {
        if (pickupScript.heldObj == other.gameObject) return;
        if(SnapType(other.gameObject))
        {
            OrderList order = other.gameObject.GetComponent<OrderList>();
            order.hasBeenPinned = true;
            if(order.pinnedPosition == Vector3.zero)
                other.gameObject.GetComponent<OrderList>().pinnedPosition = new Vector3( gameObject.GetComponent<BoxCollider>().center.x + transform.position.x, other.transform.position.y, other.transform.position.z);
            other.transform.rotation = Quaternion.LookRotation(-transform.right, transform.up);
        }
    }
    public override bool SnapType(GameObject obj)
    {
        return obj.GetComponent<OrderList>();
    }
}
