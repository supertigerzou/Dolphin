using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Dolphin.Data.Tests
{
    public static class EntityFrameworkExtensions
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

            var entityAdapterType = entityAssemly.GetType(
                "System.Data.Entity.Core.EntityClient.Internal.IEntityAdapter");

            var entityAdapter = ((IServiceProvider)EntityProviderFactory.Instance).GetService(entityAdapterType);
            entityAdapterType.GetProperty("Connection").SetValue(entityAdapter, ctx.Connection);
            var ctorParams = new[]
                        {
                            ctx.ObjectStateManager,
                            entityAdapter,
                            null
                        };

            var updateTranslator = Activator.CreateInstance(updateTranslatorType, ctorParams);

            var produceCommandsMethod = updateTranslatorType
                .GetMethod("ProduceCommands", BindingFlags.Instance | BindingFlags.NonPublic);
            var updateCommands = produceCommandsMethod.Invoke(updateTranslator, null);

            var dbCommands = new List<DbCommand>();

            foreach (var o in (IEnumerable)updateCommands)
            {
                if (functionUpdateCommandType.IsInstanceOfType(o))
                {
                    var mDbCommandField = functionUpdateCommandType.GetField(
                        "m_dbCommand", BindingFlags.Instance | BindingFlags.NonPublic);

                    if (mDbCommandField != null) dbCommands.Add((DbCommand)mDbCommandField.GetValue(o));
                }
                else if (dynamicUpdateCommandType.IsInstanceOfType(o))
                {
                    var createCommandMethod = dynamicUpdateCommandType.GetMethod(
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
            foreach (var command in dbCommands)
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