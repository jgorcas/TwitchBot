namespace Microsoft.Data.Sqlite
{
    public static class SQliteExtension
    {
        public static int GetInt32(this SqliteDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.GetInt32(ordinal);
        }

        public static long? GetNullableInt64(this SqliteDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(ordinal))
                return null;
            return reader.GetInt64(ordinal);
        }

        public static string GetString(this SqliteDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return reader.GetString(ordinal);
        }

        public static DateTime? GetNullableDateTime(this SqliteDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(ordinal))
                return null;
            return DateTime.Parse(reader.GetString(ordinal));
        }

        public static DateTime GetDateTime(this SqliteDataReader reader, string columnName)
        {
            var ordinal = reader.GetOrdinal(columnName);
            return DateTime.Parse(reader.GetString(ordinal));
        }
    }
}
