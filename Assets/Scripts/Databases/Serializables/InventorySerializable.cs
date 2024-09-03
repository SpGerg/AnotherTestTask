using Inventories.Items.Enums;
using System;

namespace Databases.Serializables
{
    [Serializable]
    public class InventorySerializable
    {
        public ItemSerializable[] items;
    }
}
