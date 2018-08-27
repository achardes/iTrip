using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;

namespace iTrip
{
    public class ConstantManager
    {
        private static ConstantManager instance;

        public static ConstantManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ConstantManager();
                }
                return instance;
            }
        }


        public int InitialTraveledDistance = 10060;
        public List<ListItem> Notes { get; set; }
        public List<string> BivouacTags { get; set; }
        public List<string> SpendingTypes { get; set; }
        public List<string> Countries { get; set; }
        public List<string> EventTypes { get; set; }
        public List<string> BivouacTypes { get; set; }
        public List<string> Currencies { get; set; }
        public List<string> WeatherKinds { get; set; }
        public string BingMapKey { get; set; }

        private ConstantManager()
        {
            BingMapKey = "At4WOXTTxj78u295HiwxdaF4v0_v73pHFve-hdktYdWAVdCI9M7bUHjWyVCkoDTE";

            Notes = new List<ListItem>()
            {
                new ListItem() { Key = "1", Text = "★" },
                new ListItem() { Key = "2", Text = "★★" },
                new ListItem() { Key = "3", Text = "★★★" },
                new ListItem() { Key = "4", Text = "★★★★" },
                new ListItem() { Key = "5", Text = "★★★★★" },
            };

            BivouacTags = new List<string>()
            {
                "BBC",
                "Bel environnement",
                "Bon accueil",
                "Commerces",
                "Douches chaudes",
                "Douches froides",
                "Dump",
                "Eau non potable",
                "Eau potable",
                "Electricité",
                "Gaz",
                "Internet / Wifi",
                "Laverie",
                "Piscine",
                "Restauration",
                "Sanitaires",
                "Table",
                "Telephone"
            };

            SpendingTypes = new List<string>() {
                "-- Type --",
                "Alimentation: Nourriture",
                "Alimentation: Restaurant",
                "Communication: Internet",
                "Communication: Téléphonie",
                "Logistique: Dump",
                "Logistique: Laverie",
                "Logistique: Plein d'eau",
                "Logistique: Retrait Argent",
                "Nuitée: Bivouac",
                "Nuitée: Camping",
                "Nuitée: Hôtel",
                "Santé: Assurance santé",
                "Santé: Hôpital",
                "Santé: Médecin",
                "Santé: Pharmacie",
                "Shopping: Divers",
                "Shopping: Marché / Mall",
                "Taxe / Visa",
                "Transport: Avion",
                "Transport: Ferrie / Bateau",
                "Transport: Parking",
                "Transport: Péage",
                "Transport: Amende",
                "Transport: Taxi",
                "Transport: Transport en commun",
                "Véhicule: Assurance",
                "Véhicule: Carburant",
                "Véhicule: Entretien / Révision",
                "Véhicule: Gaz",
                "Véhicule: Pneumatiques",
                "Véhicule: Réparation",
                "Véhicule: Taxe / Import"
            };

            #region Countries

            Countries = new List<string>() {
                "-- Country --",
                "USA: Alabama",
                "USA: Alaska",
                "USA: Arizona",
                "USA: Arkansas",
                "USA: Californie",
                "USA: Caroline du Nord",
                "USA: Caroline du Sud",
                "USA: Colorado",
                "USA: Connecticut",
                "USA: Dakota du Nord",
                "USA: Dakota du Sud",
                "USA: Delaware",
                "USA: Floride",
                "USA: Géorgie",
                "USA: Hawaï",
                "USA: Idaho",
                "USA: Illinois",
                "USA: Iowa",
                "USA: Indiana",
                "USA: Kansas",
                "USA: Kentucky",
                "USA: Louisiane",
                "USA: Maine",
                "USA: Maryland",
                "USA: Massachusetts",
                "USA: Michigan",
                "USA: Minnesota",
                "USA: Mississippi",
                "USA: Missouri",
                "USA: Montana",
                "USA: Nebraska",
                "USA: Nevada",
                "USA: New Hampshire",
                "USA: New Jersey",
                "USA: New York",
                "USA: Nouveau-Mexique",
                "USA: Ohio",
                "USA: Oklahoma",
                "USA: Oregon",
                "USA: Pennsylvanie",
                "USA: Rhode Island",
                "USA: Tennessee",
                "USA: Texas",
                "USA: Utah",
                "USA: Vermont",
                "USA: Virginie",
                "USA: Virginie occidentale",
                "USA: Washington",
                "USA: Wisconsin",
                "USA: Wyoming",

                "Canada: Île-du-Prince-Édouard",
                "Canada: Yukon",
                "Canada: Territoires du Nord-Ouest",
                "Canada: Terre-Neuve-et-Labrador",
                "Canada: Saskatchewan",
                "Canada: Québec",
                "Canada: Ontario",
                "Canada: Nunavut",
                "Canada: Nouvelle-Écosse",
                "Canada: Nouveau-Brunswick",
                "Canada: Manitoba",
                "Canada: Colombie-Britannique",
                "Canada: Alberta",

                "Mexique: Aguascalientes",
                "Mexique: Basse - Californie",
                "Mexique: Basse - Californie-du-Sud",
                "Mexique: Campeche",
                "Mexique: Chiapas",
                "Mexique: Chihuahua",
                "Mexique: Coahuila",
                "Mexique: Colima",
                "Mexique: Durango",
                "Mexique: Guanajuato",
                "Mexique: Guerrero",
                "Mexique: Hidalgo",
                "Mexique: Jalisco",
                "Mexique: Mexico",
                "Mexique: Michoacán",
                "Mexique: Morelos",
                "Mexique: Nayarit",
                "Mexique: Nuevo León",
                "Mexique: Oaxaca",
                "Mexique: Puebla",
                "Mexique: Querétaro de Arteaga",
                "Mexique: Quintana Roo",
                "Mexique: San Luis Potosí",
                "Mexique: Sinaloa",
                "Mexique: Sonora",
                "Mexique: Tabasco",
                "Mexique: Tamaulipas",
                "Mexique: Tlaxcala",
                "Mexique: Veracruz",
                "Mexique: Yucatán",
                "Mexique: Zacatecas",

                "Belize",
                "Guatemala",
                "El Salvador",
                "Honduras",
                "Nicaragua",
                "Costa-Rica",
                "Panama",
                "Colombie",
                "Equateur",
                "Perou",
                "Bolivie",
                "Paraguay",
                "Uruguay",

                "Brezil: Acre",
                "Brezil: Alagoas",
                "Brezil: Amapá",
                "Brezil: Amazonas",
                "Brezil: Bahia",
                "Brezil: Ceará",
                "Brezil: District fédéral",
                "Brezil: Espírito Santo",
                "Brezil: Goiás",
                "Brezil: Maranhão",
                "Brezil: Mato Grosso",
                "Brezil: Mato Grosso do Sul",
                "Brezil: Minas Gerais",
                "Brezil: Pará",
                "Brezil: Paraíba",
                "Brezil: Paraná",
                "Brezil: Pernambuco",
                "Brezil: Piauí",
                "Brezil: Rio de Janeiro",
                "Brezil: Rio Grande do Norte",
                "Brezil: Rio Grande do Sul",
                "Brezil: Rondônia",
                "Brezil: Roraima",
                "Brezil: Santa Catarina",
                "Brezil: São Paulo",
                "Brezil: Sergipe",
                "Brezil: Tocantins",

                "Chili: Région d'Arica et Parinacota",
                "Chili: Région de Tarapacá",
                "Chili: Région d’Antofagasta",
                "Chili: Région d’Atacama",
                "Chili: Région de Coquimbo",
                "Chili: Région de Valparaiso",
                "Chili: Région métropolitaine de Santiago",
                "Chili: Région du Libertador General Bernardo O’Higgins",
                "Chili: Région du Maule",
                "Chili: Région du Biobío",
                "Chili: Région d’Araucanie",
                "Chili: Région des Fleuves",
                "Chili: Région des Lacs",
                "Chili: Région Aisén del General Carlos Ibáñez del Campo",
                "Chili: Région de Magallanes et de l’Antarctique chilien",

                "Argentine: Ville fédérale de Buenos Aires",
                "Argentine: Province de Buenos Aires",
                "Argentine: Catamarca",
                "Argentine: Chaco",
                "Argentine: Chubut",
                "Argentine: Córdoba",
                "Argentine: Corrientes",
                "Argentine: Entre Ríos",
                "Argentine: Formosa",
                "Argentine: Jujuy",
                "Argentine: La Pampa",
                "Argentine: La Rioja",
                "Argentine: Mendoza",
                "Argentine: Misiones",
                "Argentine: Neuquén",
                "Argentine: Río Negro",
                "Argentine: Salta",
                "Argentine: San Luis",
                "Argentine: San Juan",
                "Argentine: Santa Cruz",
                "Argentine: Santa Fe",
                "Argentine: Santiago del Estero",
                "Argentine: Terre de Feu, Antarctique et Îles de l'Atlantique Sud",
                "Argentine: Tucumán",
            };

            #endregion

            EventTypes = new List<string>() {
                "Divers: Waypoint",
                "Divers: Autre",
                "Divers: Passage frontière",
                "Divers: Rencontre autres Voyageurs",
                "Divertissement: Loisirs",
                "Divertissement: Parc d'Attractions",
                "Divertissement: Spectacle",
                "Divertissement: Sport",
                "Divertissement: Zoo",
                "Incident: Chasser emplacement",
                "Incident: Contravention",
                "Incident: Contrôle Police",
                "Incident: Panne",
                "Incident: Pot-de-Vin refusé",
                "Incident: Problème de santé",
                "Site Culturel: Architecture",
                "Site Culturel: Autre",
                "Site Culturel: Métier / Artisanat",
                "Site Culturel: Musée / Exposition",
                "Site Culturel: Site Historique",
                "Site Culturel: Ville",
                "Site Naturel: Animaux observés",
                "Site Naturel: Autre",
                "Site Naturel: Parc d'Etat",
                "Site Naturel: Parc Municipal",
                "Site Naturel: Parc National",
                "Site Naturel: Plongée",
                "Site Naturel: Randonnée",
                "Site Naturel: Réserve"
            };
            WeatherKinds = new List<string>() {
                "-- Weather --",
                "Ensoleillé",
                "Nuages et Soleil",
                "Ciel voilé",
                "Eclaircies",
                "Très nuageux",
                "Brouillard",
                "Venteux",
                "Pluie faible",
                "Pluie éparse",
                "Pluie / Averse",
                "Pluie Forte",
                "Orage",
                "Neige",
                "Tempête",
                "Grêle"

            };

            BivouacTypes = new List<string>() {
                "-- Type --",
                "Sauvage",
                "Aire Stationnment CC",
                "Camping",
                "Parking: Walmart",
                "Parking: HomeDépôt",
                "Parking: Station",
                "Parking: Hôtel",
                "Parking: Autres",
                "Voie Publique",
                "Habitant"
            };
        }
    }
}
