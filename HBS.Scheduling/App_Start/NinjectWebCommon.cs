using System.Web.Http;
using HBS.Data.Abstract;
using HBS.Data.Concrete;
using WebApiContrib.IoC.Ninject;

[assembly: WebActivator.PreApplicationStartMethod(typeof(HBS.Scheduling.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(HBS.Scheduling.App_Start.NinjectWebCommon), "Stop")]

namespace HBS.Scheduling.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();


            //Support WEB API
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IProfessionalRepository>().To<ProfessionalRepository>();
            kernel.Bind<ISecurityRepository>().To<SecurityRepository>();
            kernel.Bind<ICommonRepository>().To<CommonRepository>();
        }        
    }
}
