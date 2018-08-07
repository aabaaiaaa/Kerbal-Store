using System.Collections.Generic;
using KerbalStore.Data.Entities;

namespace KerbalStore.Data
{
    public interface ITicketRepository
    {
        void SubmitTicketRequest(string who, string partRequested);
    }
}