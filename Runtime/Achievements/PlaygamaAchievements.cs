using PrimeGames.SDK.Common;
using Playgama;
using Playgama.Modules.Platform;
using System;
using System.Collections.Generic;
using Logger = PrimeGames.SDK.Common.Logger;

namespace PrimeGames.SDK.Playgama {

    [Provider(typeof(IAchievements))]
    public class PlaygamaAchievements : CommonAchievements {

        public PlaygamaAchievements() {
            SetInitialized();
        }

        protected override void GetLeaderboardImpl(string boardId, Action<Leaderboard> onLeaderboard) {
            Bridge.leaderboards.GetEntries(boardId, (isSuccess, dictionary) => {
                Logger.CreateText(this, $"get leaderboard ({boardId}) status ({isSuccess})");
                Leaderboard leaderboard = new();
                if (isSuccess == false) {
                    onLeaderboard(leaderboard);
                    return;
                }
                List<PlayerScore> playerScores = new();
                foreach (Dictionary<string, string> player in dictionary) {
                    PlayerScore playerScore = new() {
                        displayName = player["name"],
                        position = int.Parse(player["rank"]),
                        score = int.Parse(player["score"]),
                        profilePictureUrl = player["photo"]
                    };
                    playerScores.Add(playerScore);
                }
                leaderboard.players = playerScores.ToArray();
                onLeaderboard(leaderboard);
            });
        }

        protected override void GetScoreImpl(string boardId, Action<int> onScore) {
            Bridge.leaderboards.GetEntries(boardId, (isSuccess, dictionary) => {
                Logger.CreateText(this, $"get score ({boardId}) status ({isSuccess})");
                if (isSuccess == false || dictionary.Count == 0) {
                    onScore(0);
                    return;
                }
                string playerId = Bridge.player.id;
                foreach (Dictionary<string, string> player in dictionary) {
                    if (player["id"] == playerId) {
                        onScore(int.Parse(player["score"]));
                        return;
                    }
                }
                onScore(0);
            });
        }

        protected override void HappyTimeImpl() {
            Bridge.platform.SendMessage(PlatformMessage.PlayerGotAchievement);
        }

        protected override void SetScoreImpl(string boardId, int score) {
            Bridge.leaderboards.SetScore(boardId, score, (isSuccess) => {
                Logger.CreateText(this, $"set score status ({isSuccess})");
            });
        }

        protected override void UnlockImpl(string achievementId) {
            Bridge.achievements.Unlock(achievementId, (isSuccess) => {
                Logger.CreateText(this, $"unlock achievement ({achievementId}) status ({isSuccess})");
            });
        }

    }

}
