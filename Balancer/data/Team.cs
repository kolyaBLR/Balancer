using System;
using System.Collections.Generic;
using System.Linq;

namespace Balancer.data
{
    class Team
    {
        public Team(int id, int rating)
        {
            Id = id;
            Rating = rating;
        }

        public int Id { get; private set; }
        public double Rating { get; private set; }

        /// <summary>
        /// Дополнительный множитель в формуле поиска
        /// который расширяет разброс рейтингов при долгом поиске
        /// </summary>
        private double WaitingTime = 1;
        private void IncrementWaitingTime()
        {
            WaitingTime += 0.05;
        }

        /// <summary>
        /// Если разница рейтингов меньше чем указанный процент от минимального рейтинга то true
        /// Так же действует множительно обхватываемого процента относительно времени ожидания
        /// </summary>
        /// <param name="team"></param>
        /// <param name="Accuracy"></param>
        /// <returns></returns>
        public bool IsPairs(Team team, double Accuracy)
        {
            IncrementWaitingTime();
            double min = team.Rating < Rating ? team.Rating : Rating;
            double difference = Math.Abs(team.Rating - Rating);
            return difference < min * Accuracy * WaitingTime;
        }
    }
}
