using System;

namespace InventoryTesting.UI
{
    [Serializable]
    public class InventorySlot
    {
        public Item itemData;
        public int itemCount;

        public InventorySlot(Item _itemData, int _itemCount)
        {
            itemData = _itemData;
            itemCount = _itemCount;
        }
    }
}
