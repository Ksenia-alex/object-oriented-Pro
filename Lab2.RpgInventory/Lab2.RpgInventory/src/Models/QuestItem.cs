using Lab2.RpgInventory.Strategies;

namespace Lab2.RpgInventory.Models;

public class QuestItem(string name, string description, int weight)
    : Item(name, description, ItemType.QuestItem, weight, 0, new ViewingStrategies());