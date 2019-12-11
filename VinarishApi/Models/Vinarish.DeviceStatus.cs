﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/29/2019 9:55:51 PM
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

namespace VinarishApi.Models
{
    public partial class DeviceStatus
    {
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

        [System.ComponentModel.DisplayName(Expressions.DeviceStatus)]
        public virtual string Code
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Status)]
        public virtual string Text
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Type + Expressions.DeviceStatus)]
        public virtual DeviceStatusType? DeviceStatusType
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.DeviceTypes)]
        public virtual System.Guid? DeviceTypeId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Reports)]
        public virtual IList<Report> Reports
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.DeviceTypes)]
        public virtual DeviceType DeviceType
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion Extensibility Method Definitions
    }
}