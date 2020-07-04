using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.EnterpriseTask
{
    class Enterprise
    {
        private readonly Guid guid;
        public Guid Guid { get; }

        public Enterprise(Guid guid)
        {
            this.guid = guid;
        }

        public string Name { set; get; }

        private string inn;
        public string Inn
        {
            get { return inn; }
            set
            {
                if (value.Length != 10 || !value.All(z => char.IsDigit(z)))
                    throw new ArgumentException();
                Inn = inn;
            }
        }


        public DateTime EstablishDate { get; set; }

        //public DateTime getEstablishDate()
        //{
        //    return establishDate;
        //}

        //public void setEstablishDate(DateTime establishDate)
        //{
        //    this.establishDate = establishDate;
        //}

        private TimeSpan activeTimeSpan;
        public TimeSpan ActiveTimeSpan { get { return activeTimeSpan; } set { if ((DateTime.Now - EstablishDate).Seconds < 0) throw new ArgumentException(); activeTimeSpan = value; } }
        public TimeSpan GetActiveTimeSpan()
        {
            return DateTime.Now - EstablishDate;
        }

        public double GetTotalTransactionsAmount()
        {
            DataBase.OpenConnection();
            var amount = 0.0;
            foreach (Transaction t in DataBase.Transactions().Where(z => z.EnterpriseGuid == guid))
                amount += t.Amount;
            return amount;
        }
    }
}