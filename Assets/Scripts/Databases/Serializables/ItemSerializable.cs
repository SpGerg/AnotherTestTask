using Inventories.Items.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases.Serializables
{
    [Serializable]
    public class ItemSerializable
    {
        public ItemType itemType;

        public bool isBow;

        public bool isSword;
    }
}
