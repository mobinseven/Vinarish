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
    public partial class Train
    {
        public Train()
        {
            this.TrainTrips = new List<TrainTrip>();
            OnCreated();
        }

        public virtual int TrainId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Train)]
        public virtual string Name
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Trips)]
        public virtual IList<TrainTrip> TrainTrips
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion Extensibility Method Definitions
    }
}