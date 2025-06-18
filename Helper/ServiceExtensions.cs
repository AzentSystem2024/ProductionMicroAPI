using System.Reflection;

namespace MicroApi.Helper
{
    public static class ServiceExtensions
    {
        public static void AddDataLayerServices(this IServiceCollection services)
        {
            var dalAssembly = Assembly.GetExecutingAssembly(); // Scans the current assembly

            var dalTypes = dalAssembly.GetTypes()
                .Where(t => t.Name.EndsWith("Service") && t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var type in dalTypes)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name == $"I{type.Name}");
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
            }
        }
    }
}
