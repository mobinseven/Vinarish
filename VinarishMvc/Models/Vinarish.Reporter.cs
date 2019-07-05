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
    public partial class Reporter {

        public Reporter()
        {
            this.MalfunctionReports = new List<Report>();
            this.TrainTrips = new List<TrainTrip>();
            OnCreated();
        }

        public virtual int ReporterId
        {
            get;
            set;
        }

        public virtual string VinarishUserId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Departments)]
        public virtual System.Guid DepartmentId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.UserName)]
        public virtual string UserName
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Reports)]
        public virtual IList<Report> MalfunctionReports
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Departments)]
        public virtual Department Department
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.TrainTrips)]
        public virtual IList<TrainTrip> TrainTrips
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
