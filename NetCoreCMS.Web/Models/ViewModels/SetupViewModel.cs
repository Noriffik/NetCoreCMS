﻿/*
 * Author: TecRT
 * Website: http://tecrt.com
 * Copyright (c) tecrt.com
 * License: BSD (3 Clause)
*/
using NetCoreCMS.Framework.Core.Data;
using NetCoreCMS.Framework.Setup;
using System.ComponentModel.DataAnnotations;

namespace NetCoreCMS.Core.Modules.Setup.Models.ViewModels
{
    public class SetupViewModel : DatabaseInfo
    { 
        public DatabaseEngine Database { get; set; }
        
    }
}
