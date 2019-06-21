﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/21/2019 12:55:20 PM
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
    public partial class Device {

        public Device()
        {
            this.MalfunctionReports = new List<Report>();
            OnCreated();
        }

        public virtual int DeviceId
        {
            get;
            set;
        }

        public virtual string Code
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }

        public virtual int DeviceTypeId
        {
            get;
            set;
        }

        public virtual int WagonId
        {
            get;
            set;
        }

        public virtual string Serial
        {
            get;
            set;
        }

        public virtual System.DateTime? GuaranteeDate
        {
            get;
            set;
        }

        public virtual DeviceType DeviceType
        {
            get;
            set;
        }

        public virtual Wagon Wagon
        {
            get;
            set;
        }

        public virtual IList<Report> MalfunctionReports
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}