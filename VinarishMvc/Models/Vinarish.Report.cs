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
    public partial class Report {

        public Report()
        {
            this.AppendixReports = new List<Report>();
            OnCreated();
        }

        public virtual int ReportId
        {
            get;
            set;
        }

        public virtual System.DateTime DateTimeCreated
        {
            get;
            set;
        }

        public virtual System.DateTime DateTimeModified
        {
            get;
            set;
        }

        public virtual int DeviceId
        {
            get;
            set;
        }

        public virtual int ReporterId
        {
            get;
            set;
        }

        public virtual ReportStatus Status
        {
            get;
            set;
        }

        public virtual int DeviceStatusId
        {
            get;
            set;
        }

        public virtual int? AppendixReportId
        {
            get;
            set;
        }

        public virtual Device Device
        {
            get;
            set;
        }

        public virtual Reporter Reporter
        {
            get;
            set;
        }

        public virtual DeviceStatus DeviceStatus
        {
            get;
            set;
        }

        public virtual IList<Report> AppendixReports
        {
            get;
            set;
        }

        public virtual Report Report1
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}