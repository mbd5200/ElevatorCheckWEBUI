﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorCheckWEBUI.Helper
{
    public class AppHttpContext
    {
        static IServiceProvider _serviceProvider;
        public static IServiceProvider serviceProvider
        {

            get { return _serviceProvider; }

            set { _serviceProvider = value; }
        }

        public static HttpContext Current
        {
            get
            {
                IHttpContextAccessor httpContextAccessor = serviceProvider.GetService(typeof(IHttpContextAccessor)) as IHttpContextAccessor;

                return httpContextAccessor.HttpContext;
            }
        }
    }
}
