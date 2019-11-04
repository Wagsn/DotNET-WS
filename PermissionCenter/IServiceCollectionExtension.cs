namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// 添加本项目存在的依赖注入
        /// </summary>
        /// <param name="services">依赖注入服务的容器</param>
        /// <returns></returns>
        public static IServiceCollection AddUserDefined(this IServiceCollection services)
        {
            services.AddTransient<PermissionCenter.Stores.ISubjectStore, PermissionCenter.Stores.SubjectStore>();

            return services;
        }
    }
}
