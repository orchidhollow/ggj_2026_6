using System;
using Sirenix.Utilities;

    public class ConfigHelper
    {
        private static GlobalConfigAttribute configAttribute;

        public static GlobalConfigAttribute ConfigAttribute(Type t)
        {
            if (configAttribute == null)
            {
                configAttribute = t.GetCustomAttribute<GlobalConfigAttribute>();
                if (configAttribute == null)
                    configAttribute = new GlobalConfigAttribute(TypeExtensions.GetNiceName(t));
            }

            return configAttribute;
        }
    }