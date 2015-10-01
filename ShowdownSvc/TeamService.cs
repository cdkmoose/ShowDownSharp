using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using DS.Showdown.DbLibrary;

namespace DS.Showdown.ShowdownSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class TeamService : ITeamService
    {
        public string[] GetStandings()
        {
            string sql = "select team_name, wins, losses from teams join team_record on team_id order by wins desc;";
            List<string> res = new List<string>();

            DataTable tbl = DbLibrary.DbUtils.GetDataTable(sql);

            foreach (DataRow team in tbl.Rows)
            {
                string foo = String.Format("{0:-20} {1:3} - {2:3}", team["team_name"], team["wins"], team["losses"]);
                res.Add(foo);
            }

            return res.ToArray();
        }

        public string GetData(string value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
