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

            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(00), Notes = "test note 1" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(01), Notes = "test note 2" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(05), Notes = "test note 3" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(09), Notes = "test note 4" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(14), Notes = "test note 5" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(23), Notes = "test note 6" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(45), Notes = "test note 7" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(62), Notes = "test note 8" }.Execute();
            new Appointment.Create() { ClientId = clientId++, VisitDate = DateTime.UtcNow.Date.AddDays(78), Notes = "test note 9" }.Execute();
        }
    }
}