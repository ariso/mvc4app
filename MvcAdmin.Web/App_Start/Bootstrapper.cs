using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using MvcAdmin.Model;
namespace MvcAdmin
{
    public class Bootstrapper
    {
        public static void Run()
        {
            SetAutofacWebAPI();
        }
        private static void SetAutofacWebAPI()
        {
            var builder = new ContainerBuilder();
            
            #region 配置注册方法
            string dataType = ConfigurationManager.AppSettings["dataBaseType"].ToString().ToLower();
            //var data = Assembly.Load("Model");//加载特定程序集
            var data2 = Assembly.Load("MvcAdmin.Service");//加载特定程序集
            switch (dataType)
            {
                case "mssql":
                    builder.RegisterAssemblyTypes(data2)
                        .Where(a => a.FullName.Contains("MvcAdmin.Service.MSSQLServer")).AsImplementedInterfaces();
                    break;
                case "mysql":
                    builder.RegisterAssemblyTypes(data2)
                        .Where(a => a.FullName.Contains("MvcAdmin.Service.MySQLServer")).AsImplementedInterfaces();
                    break;
                default:
                    //builder.RegisterAssemblyTypes(data)
                    //    .Where(a => a.FullName.Contains("Model.MYSQL")).AsImplementedInterfaces();
                    builder.RegisterAssemblyTypes(data2)
                        .Where(a => a.FullName.Contains("MvcAdmin.Service.MySQLServer")).AsImplementedInterfaces();
                    break;
            }
            #endregion

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
           
        }
    }
}