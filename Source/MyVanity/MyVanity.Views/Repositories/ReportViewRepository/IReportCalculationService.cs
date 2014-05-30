using System.Collections.Generic;
using MyVanity.Common.Autofac;
using MyVanity.Model.AppointmentModels.Impl;
using MyVanity.Model.ConsentFormModels.Impl;
using MyVanity.Model.MessageModels;

namespace MyVanity.Views.Repositories.ReportViewRepository
{
    public interface IReportCalculationService : IPerRequestDependency
    {
        List<ConsentReportViewModel> CalculateUnsignedConsents(int daysToProcedure = 5);

        List<AppointmentReportViewModel> CalculateUnconfirmedAppointments(int daysToAppointment = 5);

        List<MessageReportViewModel> CalculateUnansweredEmails(int afterDaysSent);
    }
}
