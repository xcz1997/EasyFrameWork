﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using MvcApplication.Models;
using MvcApplication.Repository;

namespace MvcApplication.Service
{
    public class ExampleItemService : ServiceBase<ExampleItem>, IExampleItemService
    {

    }
}