using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace DS.Showdown.ShowdownGameSvc
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IGameService
    {
        [OperationContract]
        string GetData(string value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here
        [OperationContract]
        string[] GetStandings();

        [OperationContract]
        TeamRecord[] GetLeagueStandings();

        [OperationContract]
        string[] PlayGame(string home, string away);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

    [DataContract]
    public class TeamRecord
    {
        string teamName = "";
        int wins = 0;
        int losses = 0;

        [DataMember]
        public int Wins
        {
            get { return wins; }
            set { wins = value; }
        }

        [DataMember]
        public int Losses
        {
            get { return losses; }
            set { losses = value; }

        }
        [DataMember]
        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }
    }
}
