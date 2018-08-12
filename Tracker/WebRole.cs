using Microsoft.Web.Administration;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace Tracker
{
    public class WebRole : RoleEntryPoint
    {
        public override bool OnStart()
        {
            using (var serverManager = new ServerManager())
            {
                var config = serverManager.GetApplicationHostConfiguration();

                var requestFilteringSection = config.GetSection("system.webServer/security/requestFiltering");

                var denyQueryStringSequencesCollection =
                    requestFilteringSection.GetCollection("denyQueryStringSequences");

                if (denyQueryStringSequencesCollection.Count == 0)
                {
                    var addElement = denyQueryStringSequencesCollection.CreateElement("add");
                    addElement["sequence"] = @"transport=serverSentEvents";
                    denyQueryStringSequencesCollection.Add(addElement);

                    var addElement1 = denyQueryStringSequencesCollection.CreateElement("add");
                    addElement1["sequence"] = @"transport=longPolling";
                    denyQueryStringSequencesCollection.Add(addElement1);

                    var addElement2 = denyQueryStringSequencesCollection.CreateElement("add");
                    addElement2["sequence"] = @"transport=foreverFrame";
                    denyQueryStringSequencesCollection.Add(addElement2);
                }

                serverManager.CommitChanges();
            }

            return base.OnStart();
        }
    }
}