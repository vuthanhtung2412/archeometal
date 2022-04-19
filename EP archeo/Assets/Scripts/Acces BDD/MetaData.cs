using System.Data;

namespace bdd_ep
{

    /// <summary>
    /// Gestion des métadonnées décrivant un objet archéologique
    /// </summary>
    public class MetaData
    {
        public string name;
        public int idObj;
        public string typeName;
        public string uri;
        public float relativePositionX;
        public float relativePositionY;
        public float relativePositionZ;

        /// <summary>
        /// Création d'une métadonnée
        /// </summary>
        /// <param name="name">Le nom de la métadonnée</param>
        /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée</param>
        /// <param name="typeName">Type de la métadonnée</param>
        /// <param name="uri">Chemin vers la métadonnée</param>
        /// <param name="relativePositionX">Position relative X de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <param name="relativePositionY">Position relative X de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <param name="relativePositionZ">Position relative X de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        public MetaData(string name, int idObj, string typeName, string uri, float relativePositionX,
            float relativePositionY, float relativePositionZ)
        {
            this.name = name;
            this.idObj = idObj;
            this.typeName = typeName;
            this.uri = uri;
            this.relativePositionX = relativePositionX;
            this.relativePositionY = relativePositionY;
            this.relativePositionZ = relativePositionZ;
        }

        /// <summary>
        /// Créer une nouvelle métadonnée
        /// </summary>
        /// <param name="name">Le nom de la métadonnée</param>
        /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée</param>
        /// <param name="typeName">Type de la métadonnée</param>
        /// <param name="uri">Chemin vers la métadonnée</param>
        /// <param name="relativePositionX">Position relative X de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <param name="relativePositionY">Position relative Y de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <param name="relativePositionZ">Position relative Z de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <returns>La nouvelle métadonnée crée</returns>
        public static MetaData CreateNewMetaData(string name, int idObj, string typeName, string uri,
            float relativePositionX,
            float relativePositionY, float relativePositionZ)
        {
            var metaData = new MetaData(name, idObj, typeName, uri, relativePositionX, relativePositionY,
                relativePositionZ);
            Database.DataWriter(
                $"INSERT INTO metadata (name, id_obj, type_name, uri, relative_position_x, relative_position_y, relative_position_z) VALUES ('{metaData.name}',{metaData.idObj},'{metaData.typeName}','{metaData.uri}',{metaData.relativePositionX},{metaData.relativePositionY},{metaData.relativePositionZ});");
            return metaData;
        }

        /// <summary>
        /// Changer la position relative de la métadonnée
        /// </summary>
        /// <param name="name">Le nom de la métadonnée (clé dans la base de données)</param>
        /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée (clé dans la base de données)</param>
        /// <param name="newRelativePositionX">Nouvelle position relative X de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <param name="newRelativePositionY">Nouvelle position relative Y de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <param name="newRelativePositionZ">Nouvelle position relative Z de la métadonnée (par rapport au centre de gravité de l'objet)</param>
        /// <returns>La métadonnée dont les champs ont été modifiés. Retourne NULL si un problème est survenu.</returns>
        public static MetaData ChangePosition(string name, int idObj, float newRelativePositionX,
            float newRelativePositionY,
            float newRelativePositionZ)
        {
            var data = Database.DataReader($"SELECT * FROM metadata WHERE name = '{name}' AND id_obj = '{idObj}'");
            // Si on n'a pas obtenu un unique résultat : une erreur est survenue
            if (data.Rows.Count != 1)
            {
                return null;
            }

            // Création d'un objet avec le résultat de la requête à la base de données
            var metadata = new MetaData((string)(data.Rows[0]["name"]), (int)(data.Rows[0]["id_obj"]),
                (string)(data.Rows[0]["type_name"]),
                (string)(data.Rows[0]["uri"]), (float)((double)data.Rows[0]["relative_position_x"]),
                (float)((double)data.Rows[0]["relative_position_y"]),
                (float)((double)data.Rows[0]["relative_position_z"]));
            // Changement des positions relatives
            metadata.relativePositionX = newRelativePositionX;
            metadata.relativePositionY = newRelativePositionY;
            metadata.relativePositionZ = newRelativePositionZ;

            //Mise à jour de la métadonnée
            Database.DataWriter(
                $"UPDATE metadata SET relative_position_x = '{metadata.relativePositionX}', relative_position_y = '{metadata.relativePositionY}', relative_position_z = '{metadata.relativePositionZ}' WHERE name = '{metadata.name}' AND id_obj = '{metadata.idObj}';");
            return metadata;
        }

        /// <summary>
        /// Changer le chemin d'accès à la métadonnée
        /// </summary>
        /// <param name="name">Le nom de la métadonnée (clé dans la base de données)</param>
        /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée (clé dans la base de données)</param>
        /// <param name="newPath">Nouveau chemin d'accès à la métadonnée</param>
        /// <returns>La métadonnée dont le chemin a été modifié</returns>
        public static MetaData ChangePath(string name, int idObj, string newPath)
        {
            var data = Database.DataReader($"SELECT * FROM metadata WHERE name = '{name}' AND id_obj = '{idObj}'");
            // Si on n'a pas obtenu un unique résultat : une erreur est survenue
            if (data.Rows.Count != 1)
            {
                return null;
            }

            // Création d'un objet avec le résultat de la requête à la base de données
            var metadata = new MetaData((string)(data.Rows[0]["name"]), ((int)data.Rows[0]["id_obj"]),
                (string)(data.Rows[0]["type_name"]),
                (string)(data.Rows[0]["uri"]), (float)((double)data.Rows[0]["relative_position_x"]),
                (float)((double)data.Rows[0]["relative_position_y"]),
                (float)((double)data.Rows[0]["relative_position_z"]));

            //Changement du chemin d'accès
            metadata.uri = newPath;

            // Mise à jour de la métadonnée
            Database.DataWriter(
                $"UPDATE metadata SET uri = '{metadata.uri}' WHERE name = '{metadata.name}' AND id_obj = '{metadata.idObj}';");

            return metadata;
        }

        /// <summary>
        /// Changer le nom de la métadonnée
        /// </summary>
        /// <param name="name">Le nom de la métadonnée (clé dans la base de données)</param>
        /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée (clé dans la base de données)</param>
        /// <param name="newName">Nouveau nom de la métadonnée (attention, ce nom deviendra clé)</param>
        /// <returns>La métadonnée dont le nom a été modifié</returns>
        public static MetaData ChangeName(string name, int idObj, string newName)
        {
            var data = Database.DataReader($"SELECT * FROM metadata WHERE name = '{name}' AND id_obj = '{idObj}'");
            // Si on n'a pas obtenu un unique résultat : une erreur est survenue
            if (data.Rows.Count != 1)
            {
                return null;
            }

            // Création d'un objet avec le résultat de la requête à la base de données
            var metadata = new MetaData((string)(data.Rows[0]["name"]), (int)(data.Rows[0]["id_obj"]),
                (string)(data.Rows[0]["type_name"]),
                (string)(data.Rows[0]["uri"]), (float)((double)data.Rows[0]["relative_position_x"]),
                (float)((double)data.Rows[0]["relative_position_y"]),
                (float)((double)data.Rows[0]["relative_position_z"]));

            //Changement du chemin d'accès
            var oldName = metadata.name;
            metadata.name = newName;

            // Mise à jour de la métadonnée
            Database.DataWriter(
                $"UPDATE metadata SET name = '{metadata.name}' WHERE name = '{oldName}' AND id_obj = '{metadata.idObj}';");

            return metadata;
        }

        /// <summary>
        /// Importer un ensemble de types de métadonnées à partir d'un fichier CSV
        /// </summary>
        /// <param name="pathToCSV">Chemin vers le fichier CSV</param>
        /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
        public static bool ImportCSVToDataBase(string pathToCSV)
        {
            // TODO
            return false;
        }

        /// <summary>
        /// Ajouter un nouveau type de métadonnées
        /// </summary>
        /// <param name="typeName">Nom du type de la métadonnée</param>
        public static void addMetaDataType(string typeName)
        {
            // Insertion uniquement si l'enregistrement n'est pas déjà présent dans la table
            if ((Database.DataReader($"SELECT * FROM metadata_type WHERE type_name = '{typeName}';").Rows.Count) == 0)
            {
                Database.DataWriter($"INSERT INTO metadata_type (type_name) VALUES ('{typeName}');");
            }
        }

        /// <summary>
        /// Récupération de tous les types de métadonnées
        /// </summary>
        /// <returns>Tous les types de métadonnées présents dans la base de données</returns>
        public static string[] getAllMetaDataTypes()
        {
            var allMetaDataDataTable = Database.DataReader("SELECT * FROM metadata_type;");
            var allMetaDataArray = new string[allMetaDataDataTable.Rows.Count];

            // Insertion de chaque type dans un tableau
            for (int i = 0 ; i<allMetaDataArray.Length ; i++){
                allMetaDataArray[i] = allMetaDataDataTable.Rows[i]["type_name"].ToString();
            }
            return allMetaDataArray;
        }
    }
}