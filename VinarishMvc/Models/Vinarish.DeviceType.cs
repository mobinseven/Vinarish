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

namespace VinarishMvc.Models
{
    public partial class DeviceType {

        public DeviceType()
        {
            this.DevicePlaces = new List<DevicePlace>();
            this.DeviceStatus = new List<DeviceStatus>();
            OnCreated();
        }

        public virtual System.Guid DeviceTypeId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("رده دستگاه")]
        public virtual string Name
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("بخش")]
        public virtual System.Guid DepartmentId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("بخش")]
        public virtual Department Department
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("دستگاه")]
        public virtual IList<DevicePlace> DevicePlaces
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName("وضعیت")]
        public virtual IList<DeviceStatus> DeviceStatus
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
