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
    public partial class Wagon {

        public Wagon()
        {
            this.Trips = new List<WagonTrip>();
            this.Reports = new List<Report>();
            OnCreated();
        }

        public virtual System.Guid WagonId
        {
            get;
            set;
        }

        public virtual int Number
        {
            get;
            set;
        }

        public virtual string Name
        {
            get;
            set;
        }

        public virtual IList<WagonTrip> Trips
        {
            get;
            set;
        }

        public virtual IList<Report> Reports
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion
    }

}
