using System.Reflection;

namespace NowaitechShared.Models
{
    public static class GetAssemblies
    {
        public static Type[] Assembilies()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type[] types = assembly.GetTypes();

            Type[] interfaceImplementingTypes = types
                .Where(t => typeof(IEntityBase).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface)
                .ToArray();

            return interfaceImplementingTypes;
        }
    }
}
