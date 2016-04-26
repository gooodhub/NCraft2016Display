using NCraftDisplay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCraftDisplay.Data
{
    public interface IRepository
    {
        ScoreBoard GetScores();
    }
}
