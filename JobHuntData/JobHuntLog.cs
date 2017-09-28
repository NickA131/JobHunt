//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace JobHuntData
{
    using System;
    using System.Collections.Generic;
    
    public partial class JobHuntLog
    {
        public int JobHuntId { get; set; }
        public Nullable<int> WhereFoundId { get; set; }
        public Nullable<int> WhoFoundId { get; set; }
        public Nullable<int> JobTypeId { get; set; }
        public string JobTitle { get; set; }
        public Nullable<decimal> SalaryRate { get; set; }
        public Nullable<double> Duration { get; set; }
        public string AgencyName { get; set; }
        public Nullable<System.DateTime> DateContact { get; set; }
        public Nullable<bool> SentCVToAgency { get; set; }
        public Nullable<bool> Responded { get; set; }
        public Nullable<int> WhoRespondedId { get; set; }
        public Nullable<System.DateTime> RespondedDate { get; set; }
        public Nullable<bool> SentCVToClient { get; set; }
        public string AgentName { get; set; }
        public string CompanyName { get; set; }
        public string CompanyContactInterviewer { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> InterviewDate { get; set; }
        public string InterviewFeedback { get; set; }
        public Nullable<System.DateTime> SecondInterviewDate { get; set; }
        public string SecondInterviewFeedback { get; set; }
        public string Notes { get; set; }
        public System.DateTime DateEntered { get; set; }
    
        public virtual JobType JobType { get; set; }
        public virtual WhereFound WhereFound { get; set; }
        public virtual WhoFound WhoFound { get; set; }
    }
}
