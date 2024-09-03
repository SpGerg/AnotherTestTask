using Databases.Serializables;
using System.IO;
using UnityEngine;

namespace Databases
{
    public abstract class Database
    {
        public abstract InventorySerializable GetInventory();

        public abstract void SaveInventory(InventorySerializable inventorySerializable);

        public abstract ExperienceSerializable GetExperience();

        public abstract void SaveExperience(ExperienceSerializable experienceSerializable);
    }
}

