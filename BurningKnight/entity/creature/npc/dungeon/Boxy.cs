using BurningKnight.assets.items;
using BurningKnight.entity.component;
using BurningKnight.entity.item;
using BurningKnight.entity.item.stand;
using Lens.entity;
using Microsoft.Xna.Framework;

namespace BurningKnight.entity.creature.npc.dungeon {
	public class Boxy : DungeonShopNpc {
		public override void AddComponents() {
			base.AddComponents();
			
			AlwaysActive = true;
			Width = 18;
			Height = 24;
			Flips = false;
			
			AddComponent(new AnimationComponent("boxy"));
		}

		public override string GetId() {
			return ShopNpc.Boxy;
		}

		public static void Place(Vector2 where, Area area) {
			where.Y -= 16;
			
			var snek = new Snek();
			area.Add(snek);
			snek.BottomCenter = where;
			
			var pool = Items.GeneratePool(Items.GetPool(ItemPool.Boxy));

			for (var i = -1; i < 2; i++) {
				var stand = new ShopStand();
				area.Add(stand);
				stand.Center = where + new Vector2((stand.Width + 4) * i, 4 + stand.Height);

				var id = Items.GenerateAndRemove(pool, null, true);
				stand.SetItem(Items.CreateAndAdd(id, area), null);
			}
		}
	}
}