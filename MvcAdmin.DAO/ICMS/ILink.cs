using MvcAdmin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAdmin.DAO
{
    public interface ILink
    {
        bool AddLink(MLink m);

        bool DelLink(string ids);

        bool UpdLink(MLink m);

        MLink GetLink(int id);
        
        List<MLink> GetLinkList();
    }
}
