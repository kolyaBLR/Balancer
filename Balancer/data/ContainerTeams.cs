using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Balancer.data
{
    class ContainerTeams
    {
        private double Accuraty = 0.05;
        public List<Team> Teams = new List<Team>();

        /// <summary>
        /// Метод поиска пар перебором
        /// </summary>
        /// <param name="team"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Team SearchPairs(Team team, out int index)
        {
            index = 0;
            foreach (Team item in Teams)
            {
                if (item != team && item.IsPairs(team, Accuraty))
                {
                    return item;
                }
                index++;
            }
            return null;
        }

        /// <summary>
        /// Поиск пар
        /// </summary>
        /// <param name="team"></param>
        /// <param name="write"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        public bool TreatmentTeam(Team team, StreamWriter write, ref int i)
        {
            if (team != null)
            {
                Team localTeam = SearchPairs(team, out int j);
                if (localTeam != null)
                {
                    Print(write, string.Format("{0} {1}", team.Id, localTeam.Id));
                    if (i < j)
                        j--;
                    Teams.RemoveAt(i);
                    Teams.RemoveAt(j);
                    return true;
                }
            }
            return false;
        }

        public void Print(StreamWriter write, string line)
        {
            write.WriteLine(line);
        }
    }
}
