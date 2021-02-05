using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataGrabberConfig.Utility
{
    public static class DataHelper
    {


        // Converts List to DataTable
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            DataTable table = new DataTable();
            try
            {
                PropertyDescriptorCollection properties =
                    TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    table.Rows.Add(row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                // LogWriter log = new LogWriter("Exception in ToDataTable in DataHelper; Message:" + ex.Message);
            }
            return table;
        }


        // Helps to generate sql scripts when the SQL input parameter is of table-type
        public static string BuildCreateTableSql(this DataTable table)
        {
            string tableName = string.IsNullOrEmpty(table.TableName) ? "SqlTempTable" : table.TableName;

            StringBuilder sql = new StringBuilder();
            StringBuilder alterSql = new StringBuilder();

            try
            {

                sql.AppendFormat("IF OBJECT_ID ('tempdb..[#{0}]') IS NOT NULL DROP TABLE [#{0}]; \n\n", tableName);
                sql.AppendFormat("CREATE TABLE [#{0}] (", tableName);

                // Column names
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    string columnType = table.Columns[i].DataType.ToString().ToUpper();

                    sql.AppendFormat("\n\t[{0}]", table.Columns[i].ColumnName);

                    switch (columnType)
                    {
                        case "SYSTEM.INT16":
                            sql.Append(" smallint");
                            break;
                        case "SYSTEM.INT32":
                            sql.Append(" int");
                            break;
                        case "SYSTEM.INT64":
                            sql.Append(" bigint");
                            break;
                        case "SYSTEM.DATETIME":
                            sql.Append(" datetime");
                            break;
                        case "SYSTEM.STRING":
                            sql.AppendFormat(" nvarchar({0})", "max");
                            break;
                        case "SYSTEM.SINGLE":
                            sql.Append(" single");
                            break;
                        case "SYSTEM.DOUBLE":
                            sql.Append(" double");
                            break;
                        case "SYSTEM.DECIMAL":
                            sql.AppendFormat(" decimal(18, 6)");
                            break;
                        default:
                            sql.AppendFormat(" nvarchar({0})", table.Columns[i].MaxLength);
                            break;
                    }


                    sql.Append(",");
                }

                sql.Append("\n);\n\n");

                // Table body data


                foreach (DataRow row in table.Rows)
                {
                    sql.AppendFormat("INSERT INTO [#{0}] VALUES", tableName);

                    sql.Append("(");
                    foreach (DataColumn col in table.Columns)
                    {
                        sql.AppendFormat("'{0}',", row[col.ColumnName].ToString());
                    }
                    sql.Length--;
                    sql.Append(")\n");
                }

                // Remove last comma
                sql.Length--; //sql.Remove(sql.Length - 1, 1);

                sql.Append("\n\n DECLARE @table AS [dbo].CM_WS_Whitespace_ProjectScope_Details; \n");
                sql.AppendFormat("\n INSERT INTO @table \n SELECT * FROM [#{0}]\n", tableName);



            }
            catch (Exception ex)
            {
                throw ex;
                //LogWriter log = new LogWriter("Exception in BuildCreateTableSql in DataHelper; Message:" + ex.Message);
            }

            return sql.ToString();
        }

        // Adds a check on Data Reader
        public static T CustomGetValue<T>(this SqlDataReader reader, string Name)
        {
            bool isString = (typeof(T) == typeof(string));
            T result = isString ? (T)(object)string.Empty : default(T);
            try
            {
                bool hasColumn = (reader.GetSchemaTable().Select("ColumnName = '" + Name + "'").Count() == 1);

                if (hasColumn)
                {
                    if (reader[Name] != DBNull.Value)
                    {
                        if (isString)
                        {
                            string value = Convert.ToString(reader[Name]);
                            result = (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
                        }
                        else
                        {
                            result = (T)(object)Convert.ChangeType(reader[Name], typeof(T));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //LogWriter log = new LogWriter("Exception in CustomGetValue in DataHelper; Message:" + ex.Message);
            }
            finally
            {

            }
            return result;
        }

        // Clone any object
        public static T Clone<T>(this T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }

        // Custom Serialize -- with no formatting, ignoring null values
        public static string JsonSerialize<T>(T objectData)
        {
            string result = string.Empty;
            if (objectData != null)
            {
                result = JsonConvert.SerializeObject(objectData,
                                 Formatting.None,
                                 new JsonSerializerSettings
                                 {
                                     NullValueHandling = NullValueHandling.Ignore,
                                     DefaultValueHandling = DefaultValueHandling.Ignore
                                 });
            }
            return result;
        }

        // Generates GUID
        public static string GenerateID(bool onlyNumbers = false)
        {
            if (onlyNumbers)
                return Guid.NewGuid().ToString("N");
            else
                return Guid.NewGuid().ToString();
        }



        // split by separator and removes empty entries
        public static string[] SplitAndRemoveEmpty(this string text, char separator)
        {
            return text.Split(separator)?.Where(v => !string.IsNullOrEmpty(v))?.ToArray();
        }

        public static string[] GetValueByKeyAndSeperator(this string Text, string Key)
        {
            string[] result = new string[] { };

            try
            {
                var keyValuePairs = Text.Split(new string[] { "|||" }, StringSplitOptions.None)?.Where(v => !string.IsNullOrEmpty(v))?.ToList();
                keyValuePairs.ForEach((item) =>
                {
                    string[] keyValuePair = item.Trim().Split(new string[] { "|^|" }, StringSplitOptions.None);

                    if (keyValuePair.Length > 0)
                    {
                        string key = keyValuePair.FirstOrDefault();
                        string value = keyValuePair.LastOrDefault();

                        if (key == Key)
                        {
                            result = value.Trim().SplitAndRemoveEmpty(';');
                        }
                    }

                });
            }
            catch (Exception ex)
            {
                throw ex;
                //LogWriter log = new LogWriter("error occurred in DataHelper() - GetValueByKeyAndSeperator()" + ex.Message);
            }

            return result;
        }
    }

    public static class KeyValuePair
    {
        public static KeyValuePair<K, V> Create<K, V>(K key, V value)
        {
            return new KeyValuePair<K, V>(key, value);
        }
    }
}
