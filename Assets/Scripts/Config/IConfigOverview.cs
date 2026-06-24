using System.Collections.Generic;

    public interface IConfigOverview<T1, T2> where T2 : ConfigBase
    {
        string FolderName { get; }
        int NextBaseConfigId { get; }
        List<T1> AllKeys { get; }
        List<T2> AllValues { get; }
    }
