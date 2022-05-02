using System.Data;
using System.Collections.Generic;


namespace bdd_ep
{

    /// <summary>
    /// Managa metadata
    /// </summary>
    public class MetaData
    {
        public string _name;
        public int _idObj;
        public string _typeName;
        public string _uri;
        public float _relativePositionX;
        public float _relativePositionY;
        public float _relativePositionZ;

        /// <summary>
        /// Create new metadata
        /// </summary>
        /// <param name="name">Metadata name</param>
        /// <param name="idObj">Id of the object associate to the metadata</param>
        /// <param name="typeName">Metadata type</param>
        /// <param name="uri">Path to metadata</param>
        /// <param name="relativePositionX">Metadata relative position X (with regard to object center of gravity)</param>
        /// <param name="relativePositionY">Metadata relative position Y (with regard to object center of gravity)</param>
        /// <param name="relativePositionZ">Metadata relative position Z (with regard to object center of gravity)</param>
        private MetaData(string name, int idObj, string typeName, string uri, float relativePositionX,
            float relativePositionY, float relativePositionZ)
        {
            this._name = name;
            this._idObj = idObj;
            this._typeName = typeName;
            this._uri = uri;
            this._relativePositionX = relativePositionX;
            this._relativePositionY = relativePositionY;
            this._relativePositionZ = relativePositionZ;
        }

        /// <summary>
        /// Create new metadata
        /// </summary>
        /// <param name="name">Metadata name</param>
        /// <param name="idObj">Id of the object associate to the metadata</param>
        /// <param name="typeName">Metadata type</param>
        /// <param name="uri">Path to metadata</param>
        /// <param name="relativePositionX">Metadata relative position X (with regard to object center of gravity)</param>
        /// <param name="relativePositionY">Metadata relative position Y (with regard to object center of gravity)</param>
        /// <param name="relativePositionZ">Metadata relative position Z (with regard to object center of gravity)</param>
        /// <returns>New metadata created</returns>
        public static MetaData CreateNewMetaData(string name, int idObj, string typeName, string uri,
            float relativePositionX,
            float relativePositionY, float relativePositionZ)
        {
            var metaData = new MetaData(name, idObj, typeName, uri, relativePositionX, relativePositionY,
                relativePositionZ);
            Database.DataWriter(
                $"INSERT INTO metadata (name, id_obj, type_name, uri, relative_position_x, relative_position_y, relative_position_z) VALUES ('{metaData._name}',{metaData._idObj},'{metaData._typeName}','{metaData._uri}',{metaData._relativePositionX},{metaData._relativePositionY},{metaData._relativePositionZ});");
            return metaData;
        }

        /// <summary>
        /// Change metadata relative position
        /// </summary>
        /// <param name="name">Metadata name (Key in the database)</param>
        /// <param name="idObj">Id of the object associate to the metadata (Key in the database)</param>
        /// <param name="newRelativePositionX">New metadata relative position X (with regard to object center of gravity)</param>
        /// <param name="newRelativePositionY">New metadata relative position Y (with regard to object center of gravity)</param>
        /// <param name="newRelativePositionZ">New metadata relative position Z (with regard to object center of gravity)</param>
        /// <returns>Metadata updated. Return null if something went wrong.</returns>
        #nullable enable
        public static MetaData? ChangePosition(string name, int idObj, float newRelativePositionX,
            float newRelativePositionY,
            float newRelativePositionZ)
        {
            var data = Database.DataReader($"SELECT * FROM metadata WHERE name = '{name}' AND id_obj = '{idObj}'");
            // If we do not have a signe result : a problem occured
            if (data.Rows.Count != 1)
            {
                return null;
            }

            var metadata = new MetaData((string) (data.Rows[0]["name"]), (int)(data.Rows[0]["id_obj"]),
                (string) (data.Rows[0]["type_name"]),
                (string) (data.Rows[0]["uri"]), (float) (double)(data.Rows[0]["relative_position_x"]),
                (float) (double)(data.Rows[0]["relative_position_y"]),
                (float) (double)(data.Rows[0]["relative_position_z"]));
            // Change relative positions
            metadata._relativePositionX = newRelativePositionX;
            metadata._relativePositionY = newRelativePositionY;
            metadata._relativePositionZ = newRelativePositionZ;

            // Update in database
            Database.DataWriter(
                $"UPDATE metadata SET relative_position_x = '{metadata._relativePositionX}', relative_position_y = '{metadata._relativePositionY}', relative_position_z = '{metadata._relativePositionZ}' WHERE name = '{metadata._name}' AND id_obj = '{metadata._idObj}';");
            return metadata;
        }

        /// <summary>
        /// Change path to metadata
        /// </summary>
        /// <param name="name">Metadata name (Key in the database)</param>
        /// <param name="idObj">Id of the object associate to the metadata (Key in the database)</param>
        /// <param name="newPath">New path</param>
        /// <returns>Metadata with the new path. Return null if something went wrong.</returns>
        public static MetaData? ChangePath(string name, int idObj, string newPath)
        {
            var data = Database.DataReader($"SELECT * FROM metadata WHERE name = '{name}' AND id_obj = '{idObj}'");
            // If we do not have a signe result : a problem occured
            if (data.Rows.Count != 1)
            {
                return null;
            }

            var metadata = new MetaData((string) (data.Rows[0]["name"]), (int)(data.Rows[0]["id_obj"]),
                (string) (data.Rows[0]["type_name"]),
                (string) (data.Rows[0]["uri"]), (float) (double)(data.Rows[0]["relative_position_x"]),
                (float) (double)(data.Rows[0]["relative_position_y"]),
                (float) (double)(data.Rows[0]["relative_position_z"]));

            // Change path
            metadata._uri = newPath;

            // Update metadata
            Database.DataWriter(
                $"UPDATE metadata SET uri = '{metadata._uri}' WHERE name = '{metadata._name}' AND id_obj = '{metadata._idObj}';");

            return metadata;
        }

        /// <summary>
        /// Change the name of a metadata
        /// </summary>
        /// <param name="name">Metadata name (Key in the database)</param>
        /// <param name="idObj">Id of the object associate to the metadata (Key in the database)</param>
        /// <param name="newName">New name (caution, this name will become key)</param>
        /// <returns>Metadata with the new name. Return null if something went wrong.</returns>
        public static MetaData? ChangeName(string name, int idObj, string newName)
        {
            var data = Database.DataReader($"SELECT * FROM metadata WHERE name = '{name}' AND id_obj = '{idObj}'");
            // If we do not have a signe result : a problem occured
            if (data.Rows.Count != 1)
            {
                return null;
            }

            var metadata = new MetaData((string) (data.Rows[0]["name"]), (int)(data.Rows[0]["id_obj"]),
                (string) (data.Rows[0]["type_name"]),
                (string) (data.Rows[0]["uri"]), (float) (int)(data.Rows[0]["relative_position_x"]),
                (float) (int)(data.Rows[0]["relative_position_y"]),
                (float) (int)(data.Rows[0]["relative_position_z"]));

            // Change name
            var oldName = metadata._name;
            metadata._name = newName;

            // Update of metadata
            Database.DataWriter(
                $"UPDATE metadata SET name = '{metadata._name}' WHERE name = '{oldName}' AND id_obj = '{metadata._idObj}';");

            return metadata;
        }

        /// <summary>
        /// Add a new metadata type
        /// </summary>
        /// <param name="typeName">Name of the new metadata type</param>
        public static void AddMetaDataType(string typeName)
        {
            if ((Database.DataReader($"SELECT * FROM metadata_type WHERE type_name = '{typeName}';").Rows.Count) == 0)
            {
                Database.DataWriter($"INSERT INTO metadata_type (type_name) VALUES ('{typeName}');");
            }
        }

        /// <summary>
        /// Get all metadata types
        /// </summary>
        /// <returns>All metadata types in the database</returns>
        public static string[] GetAllMetaDataTypes()
        {
            var allMetaDataDataTable = Database.DataReader("SELECT * FROM metadata_type;");
            var allMetaDataList = new List<string>();
            // Insert each type in a list
            foreach (DataRow type in allMetaDataDataTable.Rows)
            {
                var typeInString = type["type_name"].ToString();
                // Insert in the list only if string is not null
                if (typeInString != null)
                    allMetaDataList.Add(typeInString);
            }

            return allMetaDataList.ToArray();
        }

        /// <summary>
        /// Get the path to the metadata
        /// </summary>
        /// <param name="idObj">Integer identifying the object</param>
        /// <returns>Path to the metadata</returns>
        public static string GetPathToMetaData(int idObj)
        {
            var uriInDataTable = Database.DataReader($"SELECT uri FROM metadata WHERE id_obj = {idObj};");
            foreach (DataRow row in uriInDataTable.Rows)
            {
                var uri = row["uri"].ToString();
                return uri ?? "";
            }

            return "";
        }
    }
}