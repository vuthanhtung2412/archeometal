using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

/// <summary>
/// Gestion des tags décrivant les objets archéologiques
/// </summary>
public class Tag
{

    string tagName;
    string parentName;

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
    static bool associateObjectToTag(int idObj, string tagName)
    {
        return false;
    }

    /// <summary>
    /// Créer un nouveau Tag
    /// </summary>
    /// <param name="tagName">Nom du nouveau Tag</param>
    /// <param name="parentName">Nom du tag parent (peut être NULL)</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    static bool createNewTag(string tagName, string parentName)
    {
        return false;
    }

    /// <summary>
    /// Déclaration d'une relation de paternité entre deux Tags
    /// </summary>
    /// <param name="parentName">Nom du Tag parent</param>
    /// <param name="childName">Nom du Tag enfant</param>
    /// <returns>True si et seulement si tout s'est déroulé correctement</returns>
    static bool declareTagPaternity(string parentName, string? childName)
    {
        return false;
    }
}

