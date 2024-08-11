using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FootballStadiumSimulator
{
    public enum Leg
    {
        Right,
        Left
    }

    public class Player : IComparable<Player>, ITraining
    {
        public string Name { get; set; }
        public int T_Shirt_Number { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }
        public Leg R_L_Leg { get; set; }
        public bool HasBall { get; set; }

        public void PassBall(Player receiver)
        {
            if (HasBall)
            {
                HasBall = false;
                receiver.HasBall = true;
                Console.WriteLine($"{Name} passed the ball to {receiver.Name}");
            }
            else
            {
                Console.WriteLine($"{Name} does not have the ball to pass.");
            }
        }

        public void ShootBall()
        {
            if (HasBall)
            {
                HasBall = false;
                Console.WriteLine($"{Name} shoots the ball towards the goal!");
            }
            else
            {
                Console.WriteLine($"{Name} does not have the ball to shoot.");
            }
        }

        public int CompareTo(Player other)
        {
            return Age.CompareTo(other.Age);
        }

        public void DoTraining()
        {
            Console.WriteLine($"{Name} is training.");
        }

        public override string ToString()
        {
            return $"Player: {Name}, T-Shirt Number: {T_Shirt_Number}, Team: {Team}, Age: {Age}, Leg: {R_L_Leg}, Has Ball: {HasBall}";
        }
    }

    public abstract class Person
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Person: {Name}";
        }
    }

    public sealed class Coach : Person
    {
        public string Team { get; set; }

        public void TrainPlayer(Player player)
        {
            Console.WriteLine($"{Name} is training {player.Name}");
        }

        public override string ToString()
        {
            return $"Coach: {Name}, Team: {Team}";
        }
    }

    public class Stadium
    {
        public static string Name { get; set; }
        public static long Capacity { get; set; }

        public static void HostMatch(Team homeTeam, Team awayTeam)
        {
            Console.WriteLine($"Match between {homeTeam.Name} and {awayTeam.Name} is being hosted at {Name}");
        }
        public override string ToString()
        {
            return $"Stadium: {Name}, Capacity: {Capacity}";
        }
    }



    public class Team
    {
        public string Name { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public Coach Coach { get; set; }

        public void AddPlayer(Player player)
        {
            Players.Add(player);
            Console.WriteLine($"{player.Name} has been added to {Name}");
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
            Console.WriteLine($"{player.Name} has been removed from {Name}");
        }

        public override string ToString()
        {
            return $"Team: {Name}, Coach: {Coach.Name}, Players: {Players.Count}";
        }
    }

    public class Ball
    {
        public Player Possessor { get; set; }
        public bool IsInPlay { get; set; }

        public override string ToString()
        {
            return $"Ball Possessor: {Possessor.Name}, Is In Play: {IsInPlay}";
        }
    }

    public class Match
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }

        static Match()
        {
            Console.WriteLine("The match starts on time!");
        }

        public void StartMatch()
        {
            Console.WriteLine($"Match between {HomeTeam.Name} and {AwayTeam.Name} has started.");
        }

        public void EndMatch()
        {
            Console.WriteLine($"Match between {HomeTeam.Name} and {AwayTeam.Name} has ended.");
           
            Random rand = new Random();
            int winner = rand.Next(2);
            if (winner == 0)
            {
                Console.WriteLine($"{HomeTeam.Name} wins!");
            }
            else
            {
                Console.WriteLine($"{AwayTeam.Name} wins!");
            }
        }

        public override string ToString()
        {
            return $"Match: {HomeTeam.Name} vs {AwayTeam.Name}";
        }
    }

    public interface ITraining
    {
        void DoTraining();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Example usage
            Player player1 = new Player { Name = "Leo Messi", T_Shirt_Number = 10, Team = "Team A", Age = 35, R_L_Leg = Leg.Left, HasBall = true };
            Player player2 = new Player { Name = "Zidan", T_Shirt_Number = 5, Team = "Team A", Age = 37, R_L_Leg = Leg.Right, HasBall = false };

            Coach coach = new Coach { Name = "Coach A", Team = "Team A" };

            Team teamA = new Team { Name = "Team A", Coach = coach };
            teamA.AddPlayer(player1);
            teamA.AddPlayer(player2);

            Stadium.Name = "National Stadium";
            Stadium.Capacity = 50000;

            Match match = new Match { HomeTeam = teamA, AwayTeam = new Team { Name = "Team B" } };
            match.StartMatch();

            player1.PassBall(player2);
            player2.ShootBall();

            coach.TrainPlayer(player1);

            match.EndMatch();

            Console.WriteLine(teamA);
            Console.WriteLine(player1);
            Console.WriteLine(coach);
            
            Console.WriteLine(match);
        }
    }
}
