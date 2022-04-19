using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

namespace bdd_ep
{
    public class accesBDD : MonoBehaviour
    {

        //Récupération d'un objet par son identifiant 
        objectArcheo getByID(int id)
        {
            return null;
        }

        //Récupération des objets lié à un tag
        objectArcheo[] getByTag(Tag tag)
        {
            return null;
        }

        //Récupération de tous les objets visibles une vue
        objectArcheo[] getByView(int view)
        {
            return null;
        }

        //Récupération d'un objet grâce à son nom
        objectArcheo getByNom(string nom)
        {
            return null;
        }

    }
}