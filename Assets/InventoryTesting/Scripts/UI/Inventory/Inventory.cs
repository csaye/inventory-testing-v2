using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryTesting.UI
{
    public class Inventory : MonoBehaviour
    {
        public static Inventory instance;

        [Header("References")]
        public GameObject inventorySlotPrefab;
        public Transform hotbarPanel;
        public Transform inventoryPanel;
        public Sprite placeholder;
        
        [HideInInspector] public List<InventorySlot> inventory = new List<InventorySlot>();
        [HideInInspector] public List<GameObject> inventorySlots;

        private bool inventoryPanelActive;

        private CanvasGroup inventoryPanelCanvasGroup;

        private int maxSlots = 40;

        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }

            if (inventoryPanelCanvasGroup == null)
            {
                inventoryPanelCanvasGroup = inventoryPanel.GetComponent<CanvasGroup>();
            }

            UpdateInventoryPanel();

            // Initialize with ten slots in hotbar
            for (int i = 0; i < 10; i++)
            {
                inventorySlots.Add(Instantiate(inventorySlotPrefab, hotbarPanel));
                inventory.Add(new InventorySlot(null, 0));
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown("escape"))
            {
                inventoryPanelActive = !inventoryPanelActive;
                UpdateInventoryPanel();
            }
        }

        private void UpdateInventoryPanel()
        {
            inventoryPanelCanvasGroup.alpha = inventoryPanelActive ? 1 : 0;
            inventoryPanelCanvasGroup.interactable = inventoryPanelActive;
            inventoryPanelCanvasGroup.blocksRaycasts = inventoryPanelActive;
        }

        // Sets the current number of inventory slots to slots
        public void SetInventorySlots(int slots)
        {
            if (slots < inventory.Count)
            {
                Debug.LogError("Trying to set slots lower than current slots.");
                return;
            }
            if (slots > maxSlots)
            {
                Debug.LogError("Trying to set slots higher than max slots.");
            }

            for (int i = 0; i < slots - inventory.Count; i++)
            {
                inventorySlots.Add(Instantiate(inventorySlotPrefab, inventoryPanel));
                inventory.Add(new InventorySlot(null, 0));
            }
        }

        // Returns whether the item was added successfully
        public bool AddItem(Item item)
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                // If slot is empty
                if (inventory[i].itemCount <= 0)
                {
                    inventory[i] = new InventorySlot(item, 1);
                    UpdateInventory();
                    return true;
                }

                // If stackable
                if (inventory[i].itemData.itemID == item.itemID && inventory[i].itemCount < item.maxStack)
                {
                    inventory[i].itemCount++;
                    UpdateInventory();
                    return true;
                }
            }

            return false;
        }

        private void UpdateInventory()
        {
            for (int i = 0; i < inventory.Count; i++)
            {
                // Set slot icon
                if (inventory[i].itemData != null)
                {
                    inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = inventory[i].itemData.itemIcon;
                }
                else
                {
                    inventorySlots[i].transform.GetChild(0).GetComponent<Image>().sprite = placeholder;
                }

                // Set slot count
                if (inventory[i].itemCount > 1)
                {
                    inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = inventory[i].itemCount.ToString();
                }
                else
                {
                    inventorySlots[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "";
                }
            }
        }
    }
}
