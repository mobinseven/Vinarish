﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/28/2019 10:58:16 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class DeviceStatus {

        public DeviceStatus()
        {
            this.Reports = new List<Report>();
            OnCreated();
        }

        public virtual System.Guid StatusId
        {
            get;
            set;
        }

        public virtual string Code
        {
            get;
            set;
        }

        public virtual string Text
        {
            get;
            set;
        }

        public virtual DeviceStatusType? DeviceStatusType
        {
            get;
            set;
        }

        public virtual System.Guid? DeviceTypeId
        {
            get;
            set;
        }

        public virtual IList<Report> Reports
        {
            get;
            set;
        }

        public virtual DeviceType DeviceType
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
