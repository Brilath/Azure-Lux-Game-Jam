using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private Item _item;
    public Item CurrentItem { get { return _item; } private set { _item = value; } }

    private Camera _gameCamera;

    private void Awake()
    {
        _gameCamera = Camera.main;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.localPosition = Vector3.zero;
        Vector3 worldPosition = _gameCamera.ScreenToWorldPoint(Input.mousePosition);
        var collider = Physics2D.OverlapPoint(worldPosition);
        if(collider != null)
        {
            Sheep sheep = collider.GetComponent<Sheep>();
            if (sheep == null) return;

            ItemHolder itemHolder = sheep.GetComponent<ItemHolder>();
            if (itemHolder == null) return;
            itemHolder.GiveItem(CurrentItem);
        }  
    }
}
