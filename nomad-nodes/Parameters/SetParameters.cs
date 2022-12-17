using System;
using System.Collections.Generic;
using System.Diagnostics;
using Nomad.Settings;
using Parameters.Internal;
using Revit.Elements;
using DM = RevitServices.Persistence.DocumentManager;
using TM = RevitServices.Transactions.TransactionManager;

namespace Parameters
{
    public static class SetParameters
    {
        public static bool SetParameterValue(
            IEnumerable<Element> elements,
            string parameterName,
            object data,
            DataOfType dataOfType)
        {
            bool result = false;

            try
            {
                AppSettings.InitTraceDiagnostic();

                var doc = DM.Instance.CurrentDBDocument;
                TM.Instance.EnsureInTransaction(doc);

                // initialize updating data
                ParameterUpdater.SetParameterValue(elements, parameterName, data, dataOfType);

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
        public static bool SetParameterValues(
            IEnumerable<Element> elements,
            string parameterName,
            IEnumerable<object> data,
            DataOfType dataOfType)
        {
            bool result = false;

            try
            {
                AppSettings.InitTraceDiagnostic();

                var doc = DM.Instance.CurrentDBDocument;
                TM.Instance.EnsureInTransaction(doc);

                // initialize updating data
                ParameterUpdater.SetParameterValues(elements, parameterName, data, dataOfType);

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
