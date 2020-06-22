using System.Collections.Generic;
using UnityEngine;

namespace InventoryTesting.UI
{
    public class Inventory : MonoBehaviour
    {
        [Header("References")]
        public GameObject inventorySlotPrefab;
        public Transform hotbarPanel;
        public Transform inventoryPanel;
        
        [HideInInspector] public List<InventorySlot> inventory = new List<InventorySlot>();
        [HideInInspector] public List<GameObject> inventorySlots;

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                inventorySlots.Add(Instantiate(inventorySlotPrefab, hotbarPanel));
            }
            for (int i = 0; i < 30; i++)
            {
                inventorySlots.Add(Instantiate(inventorySlotPrefab, inventoryPanel));
            }
        }
    }
}
