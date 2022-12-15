using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Nomad.Settings;
using Revit.Elements;
using DM = RevitServices.Persistence.DocumentManager;
using TM = RevitServices.Transactions.TransactionManager;

namespace Parameters
{
    public static class Elements
    {
        public static bool SetElementName(
            IEnumerable<Element> elements,
            IEnumerable<string> data)
        {
            bool result = false;

            try
            {
                AppSettings.InitTraceDiagnostic();

                var doc = DM.Instance.CurrentDBDocument;
                TM.Instance.EnsureInTransaction(doc);

                // map the elements with the data
                IEnumerable<(Element, string)> dataset = elements.Zip(data, (Element e, string d) => (e, d));

                foreach ((Element e, string d) in dataset)
                {
                    e.InternalElement.Name = d;
                }

                result = true;
                TM.Instance.TransactionTaskDone();
            }

            catch (Exception exc)
            {
                result = false;
                Trace.TraceError($"message: {exc.Message}; data: {exc.Data}; stack trace: {exc.StackTrace}");
                TM.Instance.ForceCloseTransaction();
            }

            finally
            {
                Trace.Flush();
            }

            return result;
        }
    }
}
