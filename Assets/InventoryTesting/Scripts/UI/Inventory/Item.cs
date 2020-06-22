using UnityEngine;

namespace InventoryTesting.UI
{
    [CreateAssetMenu(fileName = "Item", menuName = "Scriptables/Item")]
    public class Item : ScriptableObject
    {
        public Sprite itemIcon;
        public int itemID;
        public int maxStack;
        public bool holdable;
    }
}
