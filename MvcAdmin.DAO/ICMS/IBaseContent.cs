using MvcAdmin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAdmin.DAO
{
    public interface IBaseContent
    {
        bool AddBaseContent(MBaseContent m);

        bool DelBaseContent(string ids);

        bool UpdBaseContent(MBaseContent m);

        MBaseContent GetBaseContent(string name);
        
        MBaseContent GetBaseContent(int id);

        List<MBaseContent> GetBaseContentList();
    }
}
