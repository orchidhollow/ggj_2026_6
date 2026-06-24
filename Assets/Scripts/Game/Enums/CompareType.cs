using Sirenix.OdinInspector;

/// <summary>
/// 比较类型枚举
/// 用于抽取条件的数值比较
/// </summary>
public enum CompareType
{
    /// <summary>等于</summary>
    [LabelText("等于")]
    Equal,

    /// <summary>小于</summary>
    [LabelText("小于")]
    Less,

    /// <summary>小于等于</summary>
    [LabelText("小于等于")]
    LessOrEqual,

    /// <summary>大于</summary>
    [LabelText("大于")]
    Greater,

    /// <summary>大于等于</summary>
    [LabelText("大于等于")]
    GreaterOrEqual
}
