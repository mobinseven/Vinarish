using System;
using System.Data;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;

namespace VinarishMvc.Models
{
    public partial class Assistant
    {
        public Assistant()
        {
            OnCreated();
        }

        public virtual int AssistantId
        {
            get;
            set;
        }

        public virtual int PersonId
        {
            get;
            set;
        }

        public virtual int ReportId
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Assistant)]
        public virtual Reporter Person
        {
            get;
            set;
        }

        [System.ComponentModel.DisplayName(Expressions.Report)]
        public virtual Report Report
        {
            get;
            set;
        }

        #region Extensibility Method Definitions

        partial void OnCreated();

        #endregion Extensibility Method Definitions
    }
}