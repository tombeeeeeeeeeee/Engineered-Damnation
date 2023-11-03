using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderList : MonoBehaviour
{
    [SerializeField] protected Transform attachPosition;
    protected Order childOrder;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
                transform.position += transform.up * speed * Time.deltaTime;
        }
    }
}
