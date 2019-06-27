﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/22/2019 4:35:10 PM
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

        public virtual string UserName
        {
            get;
            set;
        }

        public virtual System.Guid DepartmentId
        {
            get;
            set;
        }

        public virtual IList<Report> MalfunctionReports
        {
            get;
            set;
        }

        public virtual Department Department
        {
            get;
            set;
        }

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
