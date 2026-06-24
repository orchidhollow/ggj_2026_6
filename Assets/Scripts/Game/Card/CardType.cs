/// <summary>
/// 卡牌类型枚举
/// </summary>
public enum CardType
{
    /// <summary>行动卡</summary>
    ActionCard,
    /// <summary>事件卡</summary>
    EventCard
}

/// <summary>
/// 行动卡选项枚举
/// </summary>
public enum ActionCardOption
{
    /// <summary>工作选项</summary>
    Work,
    /// <summary>休息选项</summary>
    Rest,
    /// <summary>准备选项</summary>
    Prepare
}

/// <summary>
/// 事件卡选项枚举
/// </summary>
public enum EventCardOption
{
    /// <summary>接受选项</summary>
    Accept,
    /// <summary>拒绝选项</summary>
    Refuse,
    /// <summary>延迟选项</summary>
    Delay
}

/// <summary>
/// 事件卡效果类型枚举
/// </summary>
public enum EventCardEffectType
{
    /// <summary>接受效果</summary>
    Accept,
    /// <summary>拒绝效果</summary>
    Refuse,
    /// <summary>每次延迟效果</summary>
    PerDelay,
    /// <summary>延迟上限效果</summary>
    DelayLimit
}
