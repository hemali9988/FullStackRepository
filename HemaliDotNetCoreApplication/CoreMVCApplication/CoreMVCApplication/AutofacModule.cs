using Autofac;
using CoreMVCApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVCApplication
{
    public class AutofacModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataManager>().As<IDataManager>();
       //     builder.RegisterType<DataManager>().As<IDataManager>().SingleInstance().PropertiesAutowired();

            // base.Load(builder);
        }
    }
}
