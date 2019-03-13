using BurningKnight.assets;
using Lens.entity;
using Lens.graphics;
using Lens.util.math;
using Microsoft.Xna.Framework;

namespace BurningKnight.entity.fx {
	public class ChasmFx : Entity {
		private static TextureRegion region;
		
		private Vector2 scale;
		private Color color;
		private float vx;
		private float vy;
		private float t;
		private float life;
		private byte targetAlpha;
		
		public override void Init() {
			base.Init();

			if (region == null) {
				region = CommonAse.Fx.GetSlice("rect");
			}
			
			AlwaysActive = true;

			var v = Random.Float(0.5f, 1f);
			color = new Color(v, v, v, 0);
			scale = new Vector2(Random.Float(0.15f, 0.3f));
			targetAlpha = (byte) Random.Int(32, 150);
			life = Random.Float(0.5f, 2f);
			vx = Random.Float(-1f, 1f) * 5;
			vy = Random.Float(0.5f, 2f) * -5;
		}

		public override void Update(float dt) {
			base.Update(dt);

			t += dt;

			X += vx * dt;
			Y += vy * dt;

			if (t < life - 0.5f) {
				if (color.A < targetAlpha) {
					color.A += (byte) (dt * 60);
				}
			} else {
				color.A -= (byte) (dt * 60);

				if (color.A <= 0) {
					Done = true;
				}
			}
		}

		public override void Render() {
			Graphics.Color = color;
			Graphics.Render(region, Position, 0, region.Center, scale);
			Graphics.Color = ColorUtils.WhiteColor;
		}
	}
}