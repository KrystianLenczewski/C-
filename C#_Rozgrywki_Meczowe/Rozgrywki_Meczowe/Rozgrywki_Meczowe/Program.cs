using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rozgrywki_Meczowe
{
    public class Player
    {
        private string name;
        private string surname;
        public string GetName()
        {
            return name;
        }
        public string GetSurname()
        {
            return surname;
        }
    }
    public class Team
    {
        private string name;
        private int pointsVolleyball;
        private int pointsDodgeball;
        private int pointsTugofwar;
        private List<Player> players = new List<Player>();
        public void addPlayer(Player z1)
        {
            players.Add(z1);
        }
        public string GetName()
        {
            return name;
        }
        public int GetPointsVolleyball()
        {
            return pointsVolleyball;
        }
        public int GetPointsDodgeball()
        {
            return pointsDodgeball;
        }
        public int GetPointsTugofwar()
        {
            return pointsTugofwar;
        }
        public List<Player>GetPlayers()
        {
            return players;
        }
        public Team(Team d1)
        {
            name=d1.name;
            pointsTugofwar = d1.pointsTugofwar;
            pointsDodgeball = d1.pointsDodgeball;
            pointsVolleyball = d1.pointsVolleyball;
            players = d1.players;
        }
        public Team(string name_)
        {
            name = name_;
            pointsVolleyball = 0;
            pointsTugofwar = 0;
            pointsDodgeball = 0;

        }
    }
    public class Referee
    {
        private string name;
        private string surname;
        private int id;
        public string GetName()
        {
            return name;
        }
        public string GetSurname()
        {
            return surname;
        }
        public int GetId()
        {
            return id;
        }
        public Referee(Referee r1)
        {
            name = r1.name;
            surname = r1.surname;
            id = r1.id;
        }

    }
    public class Competition//main class which is responsible for all game durning tournaments
    {
        private List<Team> teams = new List<Team>();
        private List<Referee> referees = new List<Referee>();
        private Random rnd = new Random();
        public void addTeam(Team d)
        {
            Team d1 = new Team(d);
            teams.Add(d1);
        }
        public void showTeams()
        {
            StreamWriter sw = new StreamWriter("teams.txt");
            foreach(var a in teams)
            {
                Console.WriteLine(a.GetName());
            }
            foreach(var a in teams)
            {
                sw.WriteLine(a.GetName());
            }
            sw.Close();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {




            Console.ReadLine();
        }
    }
}
