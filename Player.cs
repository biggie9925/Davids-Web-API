namespace Davids_Web_API
{
    //class structure for local API Tom, Jerry, Mickey Mouse etc... Fav_player_name is assigned from external API 
    public class Player
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Avg_points { get; set; }
        public int Avg_rebs { get; set; }
        public int Avg_asts { get; set; }
        public string Fav_player_name { get; set; } = string.Empty;

    }

    //gets details from external API (first name and last name)
    public class Rootobject
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }

    }
}
