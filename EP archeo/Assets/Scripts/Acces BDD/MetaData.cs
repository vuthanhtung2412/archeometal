using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

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
    public MetaData(string name, int idObj, string typeName, string uri, float relativePositionX, float relativePositionY, float relativePositionZ)
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
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static bool CreateNewMetaData(string name, int idObj, string typeName, string uri, float relativePositionX, float relativePositionY, float relativePositionZ)
    {
        return false;
    }

    /// <summary>
    /// Changer la position relative de la métadonnée
    /// </summary>
    /// <param name="name">Le nom de la métadonnée (clé dans la base de données)</param>
    /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée (clé dans la base de données)</param>
    /// <param name="newRelativePositionX">Nouvelle position relative X de la métadonnée (par rapport au centre de gravité de l'objet)</param>
    /// <param name="newRelativePositionY">Nouvelle position relative Y de la métadonnée (par rapport au centre de gravité de l'objet)</param>
    /// <param name="newRelativePositionZ">Nouvelle position relative Z de la métadonnée (par rapport au centre de gravité de l'objet)</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static bool ChangePosition(string name, int idObj, float newRelativePositionX, float newRelativePositionY,
        float newRelativePositionZ)
    {
        return false;
    }

    /// <summary>
    /// Changer le chemin d'accès à la métadonnée
    /// </summary>
    /// <param name="name">Le nom de la métadonnée (clé dans la base de données)</param>
    /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée (clé dans la base de données)</param>
    /// <param name="newPath">Nouveau chemin d'accès à la métadonnée</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static bool ChangePath(string name, int idObj, string newPath)
    {
        return false;
    }

    /// <summary>
    /// Changer le nom de la métadonnée
    /// </summary>
    /// <param name="name">Le nom de la métadonnée (clé dans la base de données)</param>
    /// <param name="idObj">L'identifiant de l'objet associé à la métadonnée (clé dans la base de données)</param>
    /// <param name="newName">Nouveau nom de la métadonnée (attention, ce nom deviendra clé)</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static bool ChangeName(string name, int idObj, string newName)
    {
        return false;
    }

    /// <summary>
    /// Importer un ensemble de métadonnées à partir d'un fichier CSV
    /// </summary>
    /// <param name="pathToCSV">Chemin vers le fichier CSV</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static bool ImportCSVToDataBase(Uri pathToCSV)
    {
        return false;
    }

    /// <summary>
    /// Ajouter un nouveau type de métadonnées
    /// </summary>
    /// <param name="typeName">Nom du type de la métadonnée</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static bool addMetaDataType(string typeName)
    {
        return false;
    }

    /// <summary>
    /// Récupération de tous les types de métadonnées
    /// </summary>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    public static string[] getAllMetaDataTypes()
    {
        return null;
    }
}