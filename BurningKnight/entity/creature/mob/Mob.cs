using System;
using BurningKnight.entity.buff;
using BurningKnight.entity.component;
using BurningKnight.entity.events;
using Lens.entity;

namespace BurningKnight.entity.creature.mob {
	public class Mob : Creature {
		public Entity Target;
		
		public override void AddComponents() {
			base.AddComponents();
			
			AddTag(Tags.Mob);
		}

		public override void Update(float dt) {
			base.Update(dt);

			if (Target == null) {
				FindTarget();
			}
		}

		public override bool HandleEvent(Event e) {
			if (e is BuffAddedEvent add && add.Buff is CharmedBuff || e is BuffRemovedEvent del && del.Buff is CharmedBuff) {
				// If old target even was a thing, it was from wrong category
				FindTarget();
			}
			
			return base.HandleEvent(e);
		}

		protected void FindTarget() {
			var targets = Area.Tags[GetComponent<BuffsComponent>().Has<CharmedBuff>() ? Tags.Mob : Tags.Player];
			var closestDistance = Single.MaxValue;
			Entity closest = null;
			
			foreach (var target in targets) {
				// todo: check only in the room
				var d = target.DistanceTo(this);

				if (d < closestDistance) {
					closestDistance = d;
					closest = target;
				}
			}

			if (Target != closest) {
				HandleEvent(new MobTargetChange {
					Mob = this,
					New = closest,
					Old = Target 
				});
			}			
			
			// Might be null, thats ok
			Target = closest;
		}
	}
}