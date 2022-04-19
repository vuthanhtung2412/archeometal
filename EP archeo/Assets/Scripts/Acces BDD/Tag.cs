namespace bdd_ep
{

    /// <summary>
    /// Gestion des tags décrivant les objets archéologiques
    /// </summary>
    public class Tag
    {
        public string tagName;
        public string parentName;

        /// <summary>
        /// Création d'un Tag
        /// </summary>
        /// <param name="tagName">Nom du tag</param>
        /// <param name="parentName">Nom du tag parent (peut être NULL)</param>
        public Tag(string tagName, string parentName)
        {
            this.tagName = tagName;
            this.parentName = parentName;
        }

        /// <summary>
        /// Associer un objet archéologique à un Tag
        /// </summary>
        /// <param name="idObj">Identifiant de l'objet archéologique</param>
        /// <param name="tagName">Nom du tag</param>
        /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
        public static bool associateObjectToTag(int idObj, string tagName)
        {
            if ((Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{tagName}';").Rows.Count == 0) ||
                (Database.DataReader($"SELECT * FROM obj WHERE id_obj = '{idObj}';").Rows.Count == 0) || (Database
                    .DataReader($"SELECT * FROM tag_map WHERE tag_name = '{tagName}' AND id_obj='{idObj}';").Rows
                    .Count != 0)) return false;
            Database.DataWriter($"INSERT INTO tag_map (tag_name, id_obj) VALUES ('{tagName}','{idObj}');");
            return true;
        }

        /// <summary>
        /// Créer un nouveau Tag
        /// </summary>
        /// <param name="tagName">Nom du nouveau Tag</param>
        /// <param name="parentName">Nom du tag parent (peut être NULL)</param>
        /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
        public static bool createNewTag(string tagName, string parentName)
        {
            // Si le tag existe déjà : on n'insert rien
            if (Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{tagName}';").Rows.Count != 0) return false;
            if (parentName == "")
            {
                Database.DataWriter($"INSERT INTO tag (tag_name) VALUES ('{tagName}');");
            }
            else
            {
                Database.DataWriter($"INSERT INTO tag (tag_name, parent_tag) VALUES ('{tagName}','{parentName}');");
            }

            return true;
        }

        /// <summary>
        /// Déclaration d'une relation de paternité entre deux Tags
        /// </summary>
        /// <param name="parentName">Nom du Tag parent</param>
        /// <param name="childName">Nom du Tag enfant</param>
        /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
        public static bool declareTagPaternity(string parentName, string childName)
        {
            // L'ascendant ou le descendant n'existe pas dans la base de donnée : on ne fait rien
            if (Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{childName}';").Rows.Count == 0 ||
                Database.DataReader($"SELECT * FROM tag WHERE tag_name = '{parentName}';").Rows.Count == 0)
                return false;
            Database.DataWriter($"UPDATE tag SET parent_tag = '{parentName}' WHERE tag_name = '{childName}';");
            return true;
        }
    }
}