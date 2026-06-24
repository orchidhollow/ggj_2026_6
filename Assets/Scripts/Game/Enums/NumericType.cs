using Sirenix.OdinInspector;

/// <summary>
/// 数值类型枚举
/// 表示玩家可拥有的属性类型
/// </summary>
public enum NumericType
{
    /// <summary>体力</summary>
    [LabelText("体力")]
    Health,

    /// <summary>精神</summary>
    [LabelText("精神")]
    Spirit,

    /// <summary>金钱</summary>
    [LabelText("金钱")]
    Money,

    /// <summary>名声</summary>
    [LabelText("名声")]
    Reputation
}
