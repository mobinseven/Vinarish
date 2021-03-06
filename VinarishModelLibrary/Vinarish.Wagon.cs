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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinarishLib.Models
{
    public partial class Wagon
    {
        #region Constructors

        public Wagon()
        {
            Trips = new List<WagonTrip>();
            Reports = new List<Report>();
            OnCreated();
        }

        #endregion Constructors

        #region Properties

        [DisplayName(Expressions.WagonNumber)]
        public virtual string Name { get; set; }

        [NotMapped]
        [DisplayName(Expressions.WagonNumber)]
        public virtual int Number
        {
            get
            {
                return Convert.ToInt32(Name.Substring(Name.Length - 3));
            }
        }

        [DisplayName(Expressions.Reports)]
        public virtual IList<Report> Reports
        {
            get;
            set;
        }

        [DisplayName(Expressions.Trips)]
        public virtual IList<WagonTrip> Trips
        {
            get;
            set;
        }

        public virtual Guid WagonId
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        partial void OnCreated();

        #endregion Methods
    }
}