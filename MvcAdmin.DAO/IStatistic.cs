using MvcAdmin.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcAdmin.DAO
{
    public interface IStatistic
    {
        int GetAccountCount();
        int GetArticleCount();
        int GetColumnCount();
        int GetLinkCount();
        int GetBaseContenrCount();

    }
}
