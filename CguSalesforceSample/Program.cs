using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CguSalesforceConnector.Security;
using CguSalesforceConnector;

namespace CguSalesforceSample
{
        // sample class used for queries 
        public class SFCase
        {
            public string id { get; set; }              // internal ID Number for the case which isn't shown on the case form
            public int CaseNumber { get; set; }         // notice i have CaseNumber which I can only read (not write)
            public string Subject { get; set; }
            public string Description { get; set; }
            public decimal? Rank__c { get; set; }       // note the __c for a custom field in SF -- it's nullable
        }

        // sample class used to update -- not i can't have ID or CaseNumber in it because they are read-only fields
        public class SFCaseUpdate
        {
            public string Subject { get; set; }
            public string Description { get; set; }
            public decimal? Rank__c { get; set; }
        }


        class Program
        {
            static void Main(string[] args)
            {

                // See the SalesforceSetupWalkthrough.doc for information on how to configure your SalesForce account

                // from your setup >> create >> apps >> connected apps settings in SalesForce
                const string sfdcConsumerKey = "3MVG9Y6d_Btp4xp4TTfFOaMzmXtEbo_IFXR3.CXyKsFOnlQnN3zdMKM0DHZFTIf5noog9.cP7xSD7X1MwtA.Z";
                const string sfdcConsumerSecret = "6105525243886775936";

                // your user credentials in salesforce
                const string sfdcUserName = "gprabakaran73@gmail.com.dev";
                const string sfdcPassword = "123prabha";

                // your security token form salesforce.  Name >> My Settings >> Personal >>  Reset My Security Token
                const string sfdcToken = "sI7CFyUKKQ2NEjqhGh4fQFqU";

                var client = new CguSalesforceConnector.SalesforceClient();
                var authFlow = new UsernamePasswordAuthenticationFlow(sfdcConsumerKey, sfdcConsumerSecret, sfdcUserName, sfdcPassword + sfdcToken);

                // all actions should be in a try-catch - i'll just do the authentication one for an example
                try
                {
                    client.Authenticate(authFlow);
                }
                catch (CguSalesforceConnector.SalesforceException ex)
                {
                    Console.WriteLine("Authentication failed: {0} : {1}", ex.Error, ex.Message);
                }


                // query records
                var records = client.Query<SFCase>("SELECT id FROM Account");
                foreach (var r in records)
                {
                    Console.WriteLine("Query Records {0}:", r.id);
                }

                Console.WriteLine("Hit Enter to Exit");
                Console.ReadKey();
            }
        }
}

