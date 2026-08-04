#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;

varying   vec4 v_pos;

uniform   vec4 u_color;
uniform mediump vec2 u_radius;

void main()
{
	float len = length(v_pos);
	highp float maxRadius = len - u_radius.y;
	// Outer circle
	if (maxRadius < 0.0) {// inner ring
		float tmaxRadius = maxRadius * 0.01;
		tmaxRadius = 1.0 - (1.0 - tmaxRadius) * (1.0 - tmaxRadius);
		maxRadius = (maxRadius + (tmaxRadius * 0.8)) * 0.5;
		maxRadius = max(maxRadius, -0.8);
	}
	else {
// outer ring
		maxRadius = min(maxRadius, 1.0);
		maxRadius = 1.0 - (1.0 - maxRadius) * (1.0 - maxRadius);
	}

	highp float minRadius = 1.0;
	// Calculate inner circle
	if (u_radius.x > 0.0) {
		minRadius = len - u_radius.x;
		if (minRadius > 0.0) {
			float tminRadius = minRadius * 0.1;
			tminRadius = 1.0 - (1.0 - tminRadius) * (1.0 - tminRadius);
			minRadius = (minRadius + (tminRadius * 0.8)) * 0.5;

			minRadius = min(minRadius, 1.0);
		}
		else {
			minRadius = 1.0 - (1.0 - minRadius) * (1.0 - minRadius);
			if (minRadius < -1.0) {
				minRadius = 1.0;
				maxRadius = 1.0;
			}
		}
	}

#ifdef SIMPLE
	gl_FragColor = vec4(0.7, 0.7, 0.7, u_color.a + (-min(abs(minRadius), abs(maxRadius)) * u_color.a));
#else
	gl_FragColor = vec4(u_color.r, u_color.g, u_color.b, u_color.a + (-min(abs(minRadius), abs(maxRadius)) * u_color.a));
#endif
}
