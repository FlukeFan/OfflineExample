using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Util;
using Domain.Core;
using System;

namespace OfflineExample
{
    public partial class Global : HttpApplication
    {
        protected void Application_Start()
        {
            RegisterRoutes(RouteTable.Routes);
            BuildTestData();
        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        private static void BuildTestData()
        {
            var clientId = 1;

            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(00),  Notes = "test note 01" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(01),  Notes = "test note 02" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(05),  Notes = "test note 03" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(09),  Notes = "test note 04" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(14),  Notes = "test note 05" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(23),  Notes = "test note 06" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(45),  Notes = "test note 07" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(62),  Notes = "test note 08" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(78),  Notes = "test note 09" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(85),  Notes = "test note 10" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(99),  Notes = "test note 11" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(103), Notes = "test note 12" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(127), Notes = "test note 13" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(190), Notes = "test note 14" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(191), Notes = "test note 15" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(192), Notes = "test note 16" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(240), Notes = "test note 17" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(250), Notes = "test note 18" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(260), Notes = "test note 19" }.Execute();
        }
    }
}