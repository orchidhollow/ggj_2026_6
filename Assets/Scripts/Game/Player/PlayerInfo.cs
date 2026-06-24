using System.Collections.Generic;
using System.Diagnostics;

public class PlayerInfo
{
    public Dictionary<NumericType, int> numericContainer;

    public PlayerInfo(int health, int spirit, int money, int reputation)
    {
        numericContainer = new Dictionary<NumericType, int>
        {
            { NumericType.Health, health },
            { NumericType.Spirit, spirit },
            { NumericType.Money, money },
            { NumericType.Reputation, reputation }
        };
    }

    public int GetValue(NumericType type)
    {
        return numericContainer.TryGetValue(type, out var value) ? value : 0;
    }

    public void ModifyValue(NumericType type, int value)
    {
        UnityEngine.Debug.Log($"{type.GetEnumLabel()}{value}");
        if (numericContainer.ContainsKey(type))
            numericContainer[type] += value;
        else
            numericContainer[type] = value;
    }
}
