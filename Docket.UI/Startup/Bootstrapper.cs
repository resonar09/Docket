﻿using Autofac;
using Docket.DataAccess;
using Docket.UI.Data;
using Docket.UI.Data.Lookups;
using Docket.UI.Data.Repository;
using Docket.UI.View.Services;
using Docket.UI.ViewModel;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docket.UI.Startup
{
    class Bootstrapper
    {
        public IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance(); ;
            builder.RegisterType<DocketDbContext>().AsSelf();
            builder.RegisterType<MessageDialogService>().As<IMessageDialogService>();
            builder.RegisterType<MainWindow>().AsSelf();
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<NavigationViewModel>().As<INavigationViewModel>();
            builder.RegisterType<ClientDetailViewModel>().As<IClientDetailViewModel>();
            builder.RegisterType<LookupDataService>().AsImplementedInterfaces();
            builder.RegisterType<ClientRepository>().AsImplementedInterfaces();
            builder.RegisterType<DocketSQLDataService>().As<IDocketDataService>();
            return builder.Build();
        }
    }
}
