using InventoryTesting.UI;
using UnityEngine;

namespace InventoryTesting
{
    public class ItemController : MonoBehaviour
    {
        [Header("References")]
        public Item item;
        
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                if (Inventory.instance.AddItem(item))
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
