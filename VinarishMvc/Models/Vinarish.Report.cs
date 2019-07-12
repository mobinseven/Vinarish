﻿//------------------------------------------------------------------------------
// This is auto-generated code.
//------------------------------------------------------------------------------
// This code was generated by Entity Developer tool using EF Core template.
// Code is generated on: 6/29/2019 9:55:51 PM
//
// Changes to this file may cause incorrect behavior and will be lost if
// the code is regenerated.
//------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace VinarishMvc.Models
{
    public partial class Report
    {
        public Report()
        {
            AppendixReports = new List<Report>();
            Assistants = new List<Assistant>();
            OnCreated();
        }

        public virtual int ReportId
        { get; set; }

        [DisplayName(Expressions.DateTime)]
        public virtual System.DateTime DateTimeCreated
        {
            get;
            set;
        }

        [DisplayName(Expressions.DateTimeModified)]
        public virtual System.DateTime DateTimeModified
        {
            get;
            set;
        }

        [DisplayName(Expressions.Reporter)]
        public virtual int ReporterId
        {
            get;
            set;
        }

        [NotMapped]
        [DisplayName(Expressions.Status)]
        public virtual ReportStatus Status
        {
            get
            {
                if (AppendixReports.Any(r => r.DeviceStatus.DeviceStatusType == DeviceStatusType.Repair))
                {
                    return ReportStatus.Processed;
                }
                else if (AppendixReports.Any(r => r.DeviceStatus.DeviceStatusType == DeviceStatusType.Unrepairable))
                {
                    return ReportStatus.Postponed;
                }
                else
                {
                    return ReportStatus.Waiting;
                }
            }
        }

        [DisplayName(Expressions.DeviceStatus)]
        public virtual System.Guid DeviceStatusId
        {
            get;
            set;
        }

        [DisplayName(Expressions.ParentReport)]
        public virtual int? ParentReportId
        {
            get;
            set;
        }

        [DisplayName(Expressions.DevicePlaces)]
        public virtual System.Guid DevicePlaceId
        {
            get;
            set;
        }

        [DisplayName(Expressions.Wagon)]
        public virtual System.Guid WagonId
        {
            get;
            set;
        }

        public virtual System.Guid? WagonTripId
        {
            get;
            set;
        }

        [DisplayName(Expressions.Code + Expressions.Report)]
        public virtual string Code
        {
            get;
            set;
        }

        [DisplayName(Expressions.Reporter)]
        public virtual Reporter Reporter
        {
            get;
            set;
        }

        [DisplayName(Expressions.DeviceStatus)]
        public virtual DeviceStatus DeviceStatus
        {
            get;
            set;
        }

        [DisplayName(Expressions.ChildReports)]
        public virtual IList<Report> AppendixReports
        {
            get;
            set;
        }

        [DisplayName(Expressions.ParentReport)]
        public virtual Report ParentReport
        {
            get;
            set;
        }

        [DisplayName(Expressions.DevicePlaces)]
        public virtual DevicePlace DevicePlace
        {
            get;
            set;
        }

        [DisplayName(Expressions.Wagon)]
        public virtual Wagon Wagon
        {
            get;
            set;
        }

        public virtual WagonTrip WagonTrip
        {
            get;
            set;
        }

        [DisplayName(Expressions.Site)]
        public virtual int? SiteId { get; set; }

        [DisplayName(Expressions.Site)]
        public virtual Site Site { get; set; }

        [DisplayName(Expressions.Assistants)]
        public virtual IList<Assistant> Assistants
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion Extensibility Method Definitions
    }
}