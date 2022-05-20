using UnityEngine;
using System.Data;

namespace bdd_ep
{
    public class ObjectArcheo
    {

        public GameObject me;
        public int id;
        public string id_excavation;
        public string name;
        public int view1,view2,view3;
        public string description;
        public int toy_obj_radius;
        public string toy_obj_color;

        
        /// <summary>
        /// Création d'un ObjectArcheo
        /// </summary>
        /// <param name="id_excavation">L'identifiant de l'objet extrait, donné par les archéologues</param>
        /// <param name="name">Le nom de l'objet (nom aussi du GameObject)</param>
        /// <param name="position">La position initiale de l'objet</param>
        /// <param name="rotation">La rotation appliquée à l'objet</param>
        /// <param name="view1">1 si l'objet est dans le dépot sur la première vue</param>
        /// <param name="view2">1 si l'objet est dans le dépot sur la seconde vue</param>
        /// <param name="view3">1 si l'objet est dans le dépot sur la troisième vue</param>
        /// <param name="description">description de l'objet</param>
        /// <param name="toy_obj_radius">Le diamètre du toy object (le cas échéant)</param>
        /// <param name="toy_obj_color">La couleur du toy object (le cas échéant)</param>
        public ObjectArcheo(int id,string id_excavation,string name,Vector3 position,Vector3 rotation,int view1,int view2,int view3,
            string description,int toy_obj_radius,string toy_obj_color){
            this.id = id;
            this.id_excavation = id_excavation;
            this.view1 = view1;
            this.view2 = view2;
            this.view3 = view3;
            this.description = description;
            this.toy_obj_radius = toy_obj_radius;
            this.toy_obj_color = toy_obj_color;
            this.me = GameObject.Find(name);
            this.me.transform.position = position;
            this.me.transform.rotation = Quaternion.Euler(rotation);
        }

        // <summary>
        /// Récupération des caractéristiques des objets archéologiques ayant le tag
        /// </summary>
        /// <param name="tag">Le tag à chercher</param>
        ObjectArcheo[] getByTag(Tag tag)
        {
            var result = Database.DataReader($"SELECT id_obj FROM tag_map WHERE tag_name = `${tag}`");
            ObjectArcheo[] res = new ObjectArcheo[result.Rows.Count];
            for (int i = 0; i < result.Rows.Count; i++)
            {
                res[i] = getByID((int)result.Rows[i]["id_obj"]);
            }
            return res;
        }

        // <summary>
        /// Récupération des caractéristiques des objets archéologiques présents sur une vue
        /// </summary>
        /// <param name="view">La vue à scanner</param>
        ObjectArcheo[] getByView(int view)
        {
            var result = Database.DataReader($"SELECT id_obj FROM obj WHERE view${view} = 1");
            ObjectArcheo[] res = new ObjectArcheo[result.Rows.Count];
            for (int i = 0; i < result.Rows.Count; i++)
            {
                res[i] = getByID((int)result.Rows[i]["id_obj"]);
            }
            return res;
        }

        // <summary>
        /// Récupération des caractéristiques d'un objet archéologique
        /// </summary>
        /// <param name="nom">Le nom de l'objet</param>
        ObjectArcheo getByNom(string nom)
        {
            return getByID((int)Database.DataReader($"SELECT id_obj FROM obj " +
                                               $"WHERE name = `${nom}`").Rows[0]["id_obj"]);
        }

        /// <summary>
        /// Récupération des caractéristiques d'un objet archéologique
        /// </summary>
        /// <param name="id">L'Id de l'objet, la clé dans la Base de données</param>
        ObjectArcheo getByID(int id)
        {
            var extract = Database.DataReader($"SELECT * FROM obj WHERE id_obj = ${id}");

            if(extract.Rows.Count != 1){
                return null;
            }else{
                id_excavation = extract.Rows[0]["id_obj_excavation"].ToString();
                name = extract.Rows[0]["name"].ToString();
                Vector3 position = new Vector3((float)extract.Rows[0]["init_position_x"],
                    (float)extract.Rows[0]["init_position_y"], (float)extract.Rows[0]["init_position_z"]);
                Vector3 rotation = new Vector3((float)extract.Rows[0]["init_rotation_x"],
                    (float)extract.Rows[0]["init_rotation_y"], (float)extract.Rows[0]["init_rotation_z"]);
                view1 = (int)extract.Rows[0]["view1"];
                view2 = (int)extract.Rows[0]["view2"];
                view3 = (int)extract.Rows[0]["view3"];
                description = extract.Rows[0]["description"].ToString();
                toy_obj_radius = (int)extract.Rows[0]["toy_obj_radius"];
                toy_obj_color = extract.Rows[0]["toy_obj_color"].ToString();
                return new ObjectArcheo(id, id_excavation, name, position, rotation, view1, view2, view3, description,
                    toy_obj_radius, toy_obj_color);
            }

        }

        /// <summary>
        /// Récupération des caractéristiques de tous les objets stockés
        /// </summary>
        public static ObjectArcheo[] getAll()
        {
            var extract = Database.DataReader($"SELECT * FROM obj");
            ObjectArcheo[] tab = new ObjectArcheo[extract.Rows.Count];
            for (int i = 0; i < extract.Rows.Count; i++)
            {
                int id = int.Parse(extract.Rows[i]["id_obj"].ToString()); // Oui c'est immonde, mais je sais pas comment faire autrement avec cette structure
                string id_excavation = extract.Rows[i]["id_obj_excavation"].ToString();
                string name = extract.Rows[i]["name"].ToString();
                Vector3 position = new Vector3((float)extract.Rows[i]["init_position_x"],
                    (float)extract.Rows[i]["init_position_y"], (float)extract.Rows[i]["init_position_z"]);
                Vector3 rotation = new Vector3((float)extract.Rows[i]["init_rotation_x"],
                    (float)extract.Rows[i]["init_rotation_y"], (float)extract.Rows[i]["init_rotation_z"]);
                int view1 = int.Parse(extract.Rows[i]["view1"].ToString());
                int view2 = int.Parse(extract.Rows[i]["view2"].ToString());
                int view3 = int.Parse(extract.Rows[i]["view3"].ToString());
                string description = extract.Rows[i]["description"].ToString();
                int toy_obj_radius = 0;
                int.TryParse(extract.Rows[i]["toy_obj_radius"].ToString(),out toy_obj_radius);
                string toy_obj_color = extract.Rows[i]["toy_obj_color"].ToString();
                tab[i] = new ObjectArcheo(id, id_excavation, name, position, rotation, view1, view2, view3, description,
                    toy_obj_radius, toy_obj_color);
            }
            return tab;
        }

        //Pour savoir si l'objet est un 'toy Object' ou pas, c'est à dire si on dispose du modèle 3D ou si l'on affiche simplement une forme
        bool isToyObject()
        {
            if (this.name == "toy_obj") {
                return true;
            }else {
                return false;
            }
        }

        /// <summary>
        /// modification de l'id de la fouille
        /// </summary>
        /// <param name="id">L'identifiant de l'objet extrait, donné par les archéologues</param>
        void setFouilleID(string id)
        {
            this.id_excavation = id;
            Database.DataWriter(
                $"UPDATE obj SET id_obj_excavation = `${id}` WHERE id_obj = ${this.id}");
        }

        /// <summary>
        /// modification du nom de l'objet
        /// </summary>
        /// <param name="nom">Le nouveau nom</param>
        void setNom(string nom)
        {
            this.name = nom;
            this.me = GameObject.Find(nom);
            Database.DataWriter(
                $"UPDATE obj SET name = `${nom}` WHERE id_obj = ${this.id}");
        }

        /// <summary>
        /// modification de la position et de la rotation initiale
        /// </summary>
        /// <param name="position">La position initiale ajustée</param>
        /// <param name="rotation">La rotation initiale ajustée</param>
        void setInitialPosition(Vector3 position, Vector3 rotation)
        {
            this.me.transform.position = position;
            this.me.transform.rotation = Quaternion.Euler(rotation);
            Database.DataWriter(
                $"UPDATE obj SET init_position_x = ${position[0]}," +
                $"init_position_y = ${position[1]},init_position_z = ${position[2]}" +
                $" WHERE id_obj = ${this.id}");
            Database.DataWriter(
                $"UPDATE obj SET init_rotation_x = ${rotation[0]}," +
                $"init_rotation_y = ${rotation[1]},init_rotation_z = ${rotation[2]}" +
                $" WHERE id_obj = ${this.id}");
        }

        /// <summary>
        /// Précision des vues ou l'objet est visible
        /// </summary>
        /// <param name="views">le tableau des 3 vues (1 si l'objet y est visible, 0 sinon)</param>
        void setVues(int[] views)
        {
            this.view1 = views[0];
            this.view2 = views[1];
            this.view3 = views[2];
            Database.DataWriter(
                $"UPDATE obj SET view1 = ${this.view1}, view2 = ${this.view2}, " +
                $"view3 = ${this.view3} WHERE id_obj = ${this.id}");
        }

        /// <summary>
        /// modification de la description
        /// </summary>
        /// <param name="text">la nouvelle description</param>
        void setDescription(string text)
        {
            this.description = text;
            Database.DataWriter(
                $"UPDATE obj SET description = ${this.description} WHERE id_obj = ${this.id}");
        }

    }
}