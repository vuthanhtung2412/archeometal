using UnityEngine;
using System.Data;
using Mono.Data.Sqlite;
using System.IO;

namespace bdd_ep
{
    public class objectArcheo
    {

        public GameObject me;

        //Pour savoir si l'objet est un 'toy Object' ou pas, c'est à dire si on dispose du modèle 3D ou si l'on affiche simplement une forme
        bool isToyObject()
        {
            return false;
        }

        //Modification de l'ID de fouille
        void setFouilleID(string id)
        {

        }

        //modifictation du nom de l'objet
        void setNom(string id)
        {

        }

        //Règle la potition et les rotations de l'objet
        void setInitialPosition(Transform pos)
        {

        }

        //Pour préciser les vues sur lesquelles apparait l'objet
        void setVues(int[] views)
        {

        }

        //Pour modifier la descritpion de l'objet
        void setDescription(string text)
        {

        }

    }
}