/// <summary>
/// 事件卡实例类
/// 用于存储每回合生成的事件卡数据
/// </summary>
public class EventCardInstance
{
    /// <summary>事件卡配置ID</summary>
    public int configId;

    /// <summary>延迟次数，选择delay时+1</summary>
    public int delayCount;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="id">事件卡配置ID</param>
    public EventCardInstance(int id)
    {
        configId = id;
        delayCount = 0;
    }
}
