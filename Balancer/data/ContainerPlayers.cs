using System.Collections.Generic;
using System.IO;

namespace Balancer.data
{
    class ContainerPlayers
    {
        private StreamReader TeamRead;

        public IDictionary<int, int> Players = new Dictionary<int, int>();

        /// <summary>
        /// Инициализация контейнера для игроков
        /// </summary>
        /// <param name="teamsPath">Путь к файлу комманд</param>
        /// <param name="playersPath">Путь к файлу игроков</param>
        public ContainerPlayers(string teamsPath, string playersPath)
        {
            TeamRead = new StreamReader(teamsPath);
            Players = GetPlayers(playersPath);
        }

        ~ContainerPlayers()
        {
            TeamRead.Close();
        }

        /// <summary>
        /// ВЫвод одной команды из файла
        /// </summary>
        private Team NextTeam
        {
            get
            {
                Team team = null;
                if (!TeamRead.EndOfStream)
                {
                    string teamStr = TeamRead.ReadLine();
                    string[] teamArray = teamStr.Split();
                    int id = int.Parse(teamArray[0]);
                    int rating = 0;
                    for (int i = 1; i < teamArray.Length; i++)
                    {
                        int playerId = int.Parse(teamArray[i]);
                        rating += Players[playerId];
                        Players.Remove(playerId);
                    }
                    team = new Team(id, rating);
                }
                return team;
            }
        }

        /// <summary>
        /// Вывод всей команды из файла
        /// </summary>
        public List<Team> Teams
        {
            get
            {
                List<Team> teams = new List<Team>();
                Team team = null;
                do
                {
                    team = NextTeam;
                    if (team != null)
                        teams.Add(team);
                } while (team != null);
                return teams;
            }
        }

        /// <summary>
        /// Вывод всех игроков из файла
        /// </summary>
        /// <param name="playersPath"></param>
        /// <returns></returns>
        private IDictionary<int, int> GetPlayers(string playersPath)
        {
            using (StreamReader read = new StreamReader(playersPath))
            {
                while (!read.EndOfStream)
                {
                    string playerStr = read.ReadLine();
                    string[] playerArray = playerStr.Split();
                    int id = int.Parse(playerArray[0]);
                    int rating = int.Parse(playerArray[1]);
                    Players.Add(id, rating);
                }
            }
            return Players;
        }
    }
}
