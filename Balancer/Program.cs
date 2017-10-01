using Balancer.data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Balancer
{
    class Program
    {
        private static string pathIn = "D:/Wargaming Forge Task/task_1_data";
        private static string pathOut = "D:/Nikolay_Bobrov_task_1_team_pairs";
        private static string[] testName = { "test_A", "test_B", "test_C", "test_D" };
        private static string teamsName = "teams.txt";
        private static string playersName = "players.txt";
        private static string pairsName = "pairs.txt";

        static void Main(string[] args)
        {
            for (int i = 0; i < testName.Length; i++)
            {
                string pathTeams = pathIn + "/" + testName[i] + "/" + teamsName;
                string pathPlayers = pathIn + "/" + testName[i] + "/" + playersName;
                string pathSavePairs = pathOut + "/" + testName[i] + "_" + pairsName;
                GenerateBalans(pathTeams, pathPlayers, pathSavePairs);
            }
        }

        public static void GenerateBalans(string pathTeams, string pathPlayers, string pathSavePairs)
        {
            ContainerPlayers Players = new ContainerPlayers(pathTeams, pathPlayers);
            using (StreamWriter write = new StreamWriter(pathSavePairs, false))
            {
                ContainerTeams teams = new ContainerTeams()
                {
                    Teams = Players.Teams
                };
                Team team = null;
                bool finish = false;
                do
                {
                    // поиск пар для основного массива
                    for (int i = 0; i < teams.Teams.Count; i++)
                    {
                        team = teams.Teams.ElementAt(i);
                        if (teams.TreatmentTeam(team, write, ref i))
                            i--;
                    }
                    if (teams.Teams.Count <= 1)
                        finish = true;
                    else
                    {
                        // поиск пар для остаточного отсортированного массива
                        var lastTeam = teams.Teams.OrderByDescending(t => t.Rating);
                        for (int i = 0; i < lastTeam.Count(); i++)
                        {
                            team = lastTeam.ElementAt(i);
                            if (teams.TreatmentTeam(team, write, ref i))
                                i--;
                        }
                    }
                    if (finish && teams.Teams.Count == 1)
                    {
                        teams.Print(write, teams.Teams[0].Id.ToString());
                        teams.Teams.RemoveAt(0);
                    }
                }
                while (!finish);
            }
        }
    }
}
