using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Util;

namespace Domain.Core
{
    public class Appointment : Entity
    {
        protected Appointment() { }

        public int ClientId { get; protected set; }
        public DateTime VisitDate { get; protected set; }
        public string Notes { get; protected set; }

        public class Create : Command<Appointment>
        {
            public int ClientId;
            public DateTime VisitDate;
            public string Notes;

            internal override Appointment InternalExecute()
            {
                var appointment =
                    new Appointment()
                    {
                        ClientId = ClientId,
                        VisitDate = VisitDate.Date,
                        Notes = Notes,
                    };

                return Repository.Save(appointment);
            }
        }

        public class FetchFuture : Command<IList<Appointment>>
        {
            internal override IList<Appointment> InternalExecute()
            {
                return
                    Repository.Query<Appointment>()
                        .Where(a => a.VisitDate >= Registry.NowUtc().Date)
                        .OrderBy(a => a.VisitDate)
                        .ToList();
            }
        }
    }
}
