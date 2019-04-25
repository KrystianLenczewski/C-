using System;
using System.Collections.Generic;
using System.Globalization;
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
        public List<Player> GetPlayers()
        {
            return players;
        }
        public Team(Team d1)
        {
            name = d1.name;
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
        private int id2;

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

        public Referee(string name, string surname, int id2)
        {
            this.name = name;
            this.surname = surname;
            this.id2 = id2;
        }
    }
    

    public class Game
        {
        protected Team team1, team2;
        protected int points1, points2;
        protected Referee main_referee;
        protected int id;
        public Team GetD1()
        {
            return team1;

        }
        public Team GetD2()
        {
            return team2;

        }
        public int GetPoints1()
        {
            return points1;
        }
        public int GetPoints2()
        {
            return points2;
        }
        public Referee Get_main_referee()
        {
            return main_referee;
        }
        public int GetId()
        {
            return id;
        }
        public Game() { }
        public void SetPoints1(int point1)
        {
            points1 = point1;
        }
        public void SetPoints2(int point2)
        {
            points2= point2;
        }
        public void SetReferee(Referee r1)
        {
            main_referee = new Referee(r1);
        }


    }
    public class Game_Volleyball:Game
    {
        private List<Referee> referees_pom = new List<Referee>();
        public Game_Volleyball(Team t1,Team t2)
        {
            team1 = new Team(t1);
            team2 = new Team(t2);
            points1 = 0;
            points2 = 0;
        }
        public void SetPomReferee(Referee r1,Referee r2)
        {
            referees_pom.Add(new Referee(r1));
            referees_pom.Add(new Referee(r2));
        }
    }
    public class Game_Dodgeball : Game
    {
        public Game_Dodgeball(Team t1,Team t2)
        {
            team1 = new Team(t1);
            team2 = new Team(t2);
            points1 = 0;
            points2 = 0;
        }
    }
    public class Game_Tug_Of_War:Game
    {
        public Game_Tug_Of_War(Team t1,Team t2)
        {
            team1 = new Team(t1);
            team2= new Team(t2);
            points1 = 0;
            points2 = 0;
        }
    }
    public class Competition//main class which is responsible for all game durning tournaments
    {
        private List<Team> teams = new List<Team>();
        private List<Referee> referees = new List<Referee>();
        private List<Game_Volleyball> game_divisional_volleyball = new List<Game_Volleyball>();
        private List<Game_Tug_Of_War> game_divisional_tug_of_war = new List<Game_Tug_Of_War>();
        private List<Game_Dodgeball> game_divisional_dodgeball= new List<Game_Dodgeball>();

        private Random rnd = new Random();
        public void addTeam(Team d)
        {
            Team d1 = new Team(d);
            teams.Add(d1);
        }
        public void showTeams()
        {
            StreamWriter sw = new StreamWriter("teams.txt");
            foreach (var a in teams)
            {
                Console.WriteLine(a.GetName());
            }
            foreach (var a in teams)
            {
                sw.WriteLine(a.GetName());
            }
            sw.Close();
        }
        public void addReferee(Referee s1) // dodanie sędziego do zawodów
        {

            
            Referee s2 = new Referee(s1);
            referees.Add(s2);
        }

    }
    class Program
    {
        static void Main(string[] args)
        {

            Competition beach = new Competition();
            StreamReader sr = new StreamReader("sedziowie.txt");
            StreamReader dru = new StreamReader("druzyny.txt");
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberGroupSeparator = " ";
            string line;
            while ((line = sr.ReadLine()) != null)
            {

                string[] s = line.Split(null);
                string imie = s[0];
                string nazwisko = s[1];
                int d = int.Parse(s[2], nfi);
                

            }
            string line1;
            while ((line1 = dru.ReadLine()) != null)
            {


                Team k1 = new Team(line1);

                

            }
            sr.Close();
            dru.Close();

            int wybor;
            do
            {

                Console.WriteLine("\n\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("                    MENU");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-------------MANAGE REFEREES--------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("1.Add referee");
                Console.WriteLine("2.Remove referee");
                Console.WriteLine("3.Show all referees");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("------------MANAGE TEAMS--------------");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("4.Add team");
                Console.WriteLine("5.Remove team");
                Console.WriteLine("6.Show teams");
                Console.WriteLine("7.Creating league matches");
                Console.WriteLine("8.Show score league matches");
                Console.WriteLine("9.Creating semi-finals and finals ");
                Console.WriteLine("10.Displaying the results of finals and semi-finals");
                Console.WriteLine("11.Table of Team Outcome Points ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("PRESS 0 TO EXIT FROM THE PROGRAM");
                Console.ResetColor();
                Console.WriteLine("Choice:");
                wybor = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (wybor)
                {
                    case 1:
                        string name;
                        string surname;
                        string id;
                        int id2;

                        Console.WriteLine("Give name of referee ");
                        name = Console.ReadLine();
                        Console.WriteLine("Give surname of referee: ");
                        surname = Console.ReadLine();
                        Console.WriteLine("Give id referee: ");
                        id = Console.ReadLine();

                      

                        id2 = int.Parse(id);
                        
                            Referee s11 = new Referee(name, surname, id2);
                        beach.addReferee(s11);

                        break;

                }


            } while (wybor != 0);






        
    


    Console.ReadLine();
        }
    }
}
