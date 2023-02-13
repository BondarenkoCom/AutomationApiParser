using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoParser.Interfaces
{
    public interface IGoogleSheetsDriver
    {
        string PushToGoogleSheets(string host, string ranking, string reviewBody, string dataTime, string author);

        string GetDataFromGoogleSheets();
    }
}
