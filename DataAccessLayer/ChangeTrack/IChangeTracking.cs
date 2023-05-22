using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ChangeTrack
{
    public interface IChangeTracking
    {
        public void ContactChangesPush();
        public void EventChangesPush();
    }
}
