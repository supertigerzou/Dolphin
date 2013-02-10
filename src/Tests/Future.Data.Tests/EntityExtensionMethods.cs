using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.EntityClient.Internal;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Future.Data.Tests
{
    public static class CustomExtensions
    {
        private const string EntityAssemblyName = "EntityFramework";

        public static string ToTraceString(this IQueryable query)
        {
            var toTraceStringMethod = query.GetType().GetMethod("ToTraceString");

            return toTraceStringMethod != null ? toTraceStringMethod.Invoke(query, null).ToString() : "";
        }

        public static string ToTraceString(this ObjectContext ctx)
        {
            var entityAssemly = Assembly.Load(EntityAssemblyName);

            var updateTranslatorType = entityAssemly.GetType(
                "System.Data.Entity.Core.Mapping.Update.Internal.UpdateTranslator");

            var functionUpdateCommandType = entityAssemly.GetType(
                "System.Data.Entity.Core.Mapping.Update.Internal.FunctionUpdateCommand");

            var dynamicUpdateCommandType = entityAssemly.GetType(
                "System.Data.Entity.Core.Mapping.Update.Internal.DynamicUpdateCommand");

            var entityAdapter = (IEntityAdapter)((IServiceProvider)EntityProviderFactory.Instance).GetService(typeof(IEntityAdapter));
            entityAdapter.Connection = ctx.Connection;
            var ctorParams = new object[]
                        {
                            ctx.ObjectStateManager,
                            entityAdapter,
                            null
                        };

            object updateTranslator = Activator.CreateInstance(updateTranslatorType, ctorParams);

            MethodInfo produceCommandsMethod = updateTranslatorType
                .GetMethod("ProduceCommands", BindingFlags.Instance | BindingFlags.NonPublic);
            object updateCommands = produceCommandsMethod.Invoke(updateTranslator, null);

            var dbCommands = new List<DbCommand>();

            foreach (object o in (IEnumerable)updateCommands)
            {
                if (functionUpdateCommandType.IsInstanceOfType(o))
                {
                    FieldInfo mDbCommandField = functionUpdateCommandType.GetField(
                        "m_dbCommand", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (mDbCommandField != null) dbCommands.Add((DbCommand)mDbCommandField.GetValue(o));
                }
                else if (dynamicUpdateCommandType.IsInstanceOfType(o))
                {
                    MethodInfo createCommandMethod = dynamicUpdateCommandType.GetMethod(
                        "CreateCommand", BindingFlags.Instance | BindingFlags.NonPublic);

                    var methodParams = new object[]
                    {
                        new Dictionary<int, object>()
                    };

                    dbCommands.Add((DbCommand)createCommandMethod.Invoke(o, methodParams));
                }
                else
                {
                    throw new NotSupportedException("Unknown UpdateCommand Kind");
                }
            }

            var traceString = new StringBuilder();
            foreach (DbCommand command in dbCommands)
            {
                traceString.AppendLine("=============== BEGIN COMMAND ===============");
                traceString.AppendLine();

                traceString.AppendLine(command.CommandText);
                foreach (DbParameter param in command.Parameters)
                {
                    traceString.AppendFormat("{0} = {1}", param.ParameterName, param.Value);
                    traceString.AppendLine();
                }

                traceString.AppendLine();
                traceString.AppendLine("=============== END COMMAND ===============");
            }

            return traceString.ToString();
        }
    }
}