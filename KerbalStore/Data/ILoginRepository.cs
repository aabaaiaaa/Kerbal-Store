using System.Collections.Generic;
using KerbalStore.Data.Entities;

namespace KerbalStore.Data
{
    public interface ILoginRepository
    {
        Login Login(Login creds);
    }
}