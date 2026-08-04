using System;
using System.Timers;

using ClashofClans.Database;
using ClashofClans.Protocol.Messages.Server.Battle;

namespace ClashofClans.Logic.Manager
{
	public class GameMatchmakingManager
	{
		public System.Timers.Timer searchTimer { get; set; }
		private Device device;

		public void Init(Device dvc)
		{
			if (searchTimer != null)
				searchTimer.Stop();

			device = dvc;
			FindEnemy();
		}
		public void InitV2(Device dvc)
		{
			if (searchTimer != null)
				searchTimer.Stop();

			device = dvc;
			FindEnemyV2();
		}
		private void Destruct()
		{
			device = null;
			searchTimer.Stop();
		}
		private async void FindEnemy()
		{
			Player enemy = await PlayerDb.GetRandomCachedPlayer(device.Player);

			if (enemy == null)
			{
				InitSearchTimer(device.Player.Home.Battle);
			}
			/*else if (enemy.Home.AllianceInfo.Id == device.Player.Home.AllianceInfo.Id)
			{
				if (searchTimer == null)
					InitSearchTimer(device.Player.Home.Battle);
			}*/
			else
			{
				device.Player.Home.Battle.SetEnemyData(enemy);

				await new EnemyHomeDataMessage(device)
				{
					Enemy = enemy,
					NextButton = true
				}.SendAsync();
			}
		}
		private async void FindEnemyV2()
		{
			Player enemy = await PlayerDb.GetRandomCachedPlayer(device.Player);

			if (enemy == null)
			{
				InitSearchTimerV2(device.Player.Home.Battle);
			}
			/*else if (enemy.Home.AllianceInfo.Id == device.Player.Home.AllianceInfo.Id)
			{
				if (searchTimer == null)
					InitSearchTimerV2(device.Player.Home.Battle);
			}*/
			else
			{
				device.Player.Home.Battle.SetEnemyData(enemy);

				await new Village2AttackAvatarDataMessage(device)
				{
					Enemy = enemy
				}.SendAsync();
			}
		}
		private void InitSearchTimer(Battle battle)
		{
			searchTimer = new System.Timers.Timer(2000);
			searchTimer.Elapsed += SearchForPlayer;
			searchTimer.AutoReset = true;
			searchTimer.Enabled = true;
		}
		private void InitSearchTimerV2(Battle battle)
		{
			searchTimer = new System.Timers.Timer(2000);
			searchTimer.Elapsed += SearchForPlayerV2;
			searchTimer.AutoReset = true;
			searchTimer.Enabled = true;
		}
		private async void SearchForPlayer(Object source, ElapsedEventArgs e)
		{
			Player enemy = await PlayerDb.GetRandomCachedPlayer(device.Player);
			if (enemy != null && enemy.Home.AllianceInfo.Id != device.Player.Home.AllianceInfo.Id)
			{
				device.Player.Home.Battle.SetEnemyData(enemy);

				await new EnemyHomeDataMessage(device)
				{
					Enemy = enemy,
					NextButton = true
				}.SendAsync();

				Destruct();
			}
		}
		private async void SearchForPlayerV2(Object source, ElapsedEventArgs e)
		{
			Player enemy = await PlayerDb.GetRandomCachedPlayer(device.Player);
			if (enemy != null && enemy.Home.AllianceInfo.Id != device.Player.Home.AllianceInfo.Id)
			{
				device.Player.Home.Battle.SetEnemyData(enemy);

				await new Village2AttackAvatarDataMessage(device)
				{
					Enemy = enemy
				}.SendAsync();

				Destruct();
			}
		}
	}
}