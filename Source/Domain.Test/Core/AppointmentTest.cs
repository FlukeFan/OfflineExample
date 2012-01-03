using System;
using Domain.Core;
using Domain.Util;
using NUnit.Framework;

namespace Domain.Test.Core
{
    public class AppointmentBuilder : Builder<Appointment>
    {
        public static readonly int DefaultClientId = 1234;
        public static readonly DateTime DefaultVisitDate = new DateTime(2009, 08, 07);
        public static readonly string DefaultNotes = "test notes";

        public AppointmentBuilder()
        {
            With(s => s.ClientId, DefaultClientId);
            With(s => s.VisitDate, DefaultVisitDate);
            With(s => s.Notes, DefaultNotes);
        }
    }

    [TestFixture]
    public class AppointmentTest : DomainTestBase
    {
        [Test]
        public void CreateNew()
        {
            var newAppointment =
                new Appointment.Create()
                    {
                        ClientId = 123,
                        VisitDate = new DateTime(2008, 07, 06, 05, 04, 03),
                        Notes = "appointment notes",
                    }
                    .Execute();

            AssertIsPersisted(newAppointment);
            Assert.That(newAppointment.ClientId, Is.EqualTo(123));
            Assert.That(newAppointment.VisitDate, Is.EqualTo(new DateTime(2008, 07, 06)));
            Assert.That(newAppointment.Notes, Is.EqualTo("appointment notes"));
        }

        [Test]
        public void FetchFuture()
        {
            var now = new DateTime(2007, 01, 01, 01, 01, 01);
            Registry.NowUtc = () => now;

            var a1 = new AppointmentBuilder().With(a => a.VisitDate, new DateTime(2009, 01, 01)).BuildAndPersist();
            var a2 = new AppointmentBuilder().With(a => a.VisitDate, new DateTime(2006, 01, 01)).BuildAndPersist();
            var a3 = new AppointmentBuilder().With(a => a.VisitDate, new DateTime(2008, 01, 01)).BuildAndPersist();
            var a4 = new AppointmentBuilder().With(a => a.VisitDate, new DateTime(2007, 01, 01)).BuildAndPersist();

            var appointments = new Appointment.FetchFuture().Execute();

            Assert.That(appointments.Count, Is.EqualTo(3));
            Assert.That(appointments[0], Is.EqualTo(a4));
            Assert.That(appointments[1], Is.EqualTo(a3));
            Assert.That(appointments[2], Is.EqualTo(a1));
        }
    }
}
