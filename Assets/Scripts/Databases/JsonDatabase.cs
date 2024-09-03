using Databases.Serializables;
using Inventories.Items.Enums;
using System.IO;
using UnityEngine;

namespace Databases
{
    public class JsonDatabase : Database
    {
        public static JsonDatabase Instance { 
            get
            {
                _instance ??= new JsonDatabase();

                return _instance;
            }
        }

        private static JsonDatabase _instance;

        public JsonDatabase()
        {
            DatabasePath = Path.Combine(Application.persistentDataPath, "database");
            InventoryPath = Path.Combine(DatabasePath, "inventory.json");
            ExperiencePath = Path.Combine(DatabasePath, "experience.json");


            Directory.CreateDirectory(DatabasePath);

            Experience = GetExperience();
            Inventory = GetInventory();
        }

        public string DatabasePath { get; private set; }

        public string InventoryPath { get; private set; }

        public string ExperiencePath { get; private set; }

        public ExperienceSerializable Experience { get; private set; }

        public InventorySerializable Inventory { get; private set; }

        public override InventorySerializable GetInventory()
        {
            if (!File.Exists(InventoryPath))
            {
                var emptyInventory = new InventorySerializable()
                {
                    items = new ItemSerializable[]
                    {
                        new()
                        {
                            itemType = ItemType.Sword,
                            isSword = true
                        },
                        new()
                        {
                            itemType = ItemType.Bow,
                            isBow = true
                        }
                    }
                };

                SaveInventory(emptyInventory);

                return emptyInventory;
            }

            return JsonUtility.FromJson<InventorySerializable>(File.ReadAllText(InventoryPath));
        }

        public override void SaveInventory(InventorySerializable inventorySerializable)
        {
            File.WriteAllText(InventoryPath, JsonUtility.ToJson(inventorySerializable));
        }

        public override ExperienceSerializable GetExperience()
        {
            if (!File.Exists(ExperiencePath))
            {
                var emptyExperience = new ExperienceSerializable()
                {
                    experience = 0,
                    level = 1
                };

                SaveExperience(emptyExperience);

                return emptyExperience;
            }

            return JsonUtility.FromJson<ExperienceSerializable>(File.ReadAllText(ExperiencePath));
        }

        public override void SaveExperience(ExperienceSerializable experienceSerializable)
        {
            File.WriteAllText(ExperiencePath, JsonUtility.ToJson(experienceSerializable));
        }
    }
}
