/// <summary>
/// 行动卡实例类
/// 用于存储每回合生成的行为卡数据
/// </summary>
public class ActionCardInstance
{
    /// <summary>工作效果对应的配置ID</summary>
    public int workConfigId;

    /// <summary>休息效果对应的配置ID</summary>
    public int restConfigId;

    /// <summary>准备次数，选择prepare时+1</summary>
    public int prepareCount;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="workId">工作效果配置ID</param>
    /// <param name="restId">休息效果配置ID</param>
    public ActionCardInstance(int workId, int restId)
    {
        workConfigId = workId;
        restConfigId = restId;
        prepareCount = 0;
    }
}
