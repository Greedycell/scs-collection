#ifdef GL_ES
precision mediump float;
#else
#define highp
#define mediump
#define lowp
#endif

uniform sampler2D texture0;
uniform sampler2D texture1;

varying vec2 v_texcoord;
varying vec2 v_mapcoord0;
varying vec2 v_mapcoord1;
varying float v_phase0;
varying float v_phase1;
varying float v_noise;
varying vec3 v_texweight;

uniform float u_time;

void main()
{
	float uvLoop = 3.0;
	vec3 param = texture2D(texture0, v_texcoord).rgb;
	vec2 flowVector = (param.gr * 2.0 - 1.0) * uvLoop;
	//flowVector.y = -flowVector.y;

	vec4 tex0 = texture2D(texture1, v_mapcoord0 - flowVector * v_phase0);
	vec4 tex1 = texture2D(texture1, v_mapcoord1 - flowVector * v_phase1);
	
	float halfCycle = 0.5;
	float flowLerp = abs(halfCycle - v_phase0) / halfCycle;
	//gl_FragColor = mix(tex0, tex1, flowLerp);

	vec3 pos = mix(tex0.rgb, tex1.rgb, flowLerp) * v_texweight;
	
	float red = pos.r + pos.g + pos.b;
	
	//float minValue = 0.0 + (sin(u_time) + 1.0) * 0.3;
	float minValue = param.b * param.b * 0.5;

	red = max(0.0, red + minValue);
	if (red > 1.0) {
		float delta = (red - 1.0) * 0.3;
		if (delta > 0.1) delta = 0.1;
		red = 1.0 - delta;
	}
	red = (red * red);// - 0.1;

	float green = pow(red, 4.0);
	float blue = 0.0;

	gl_FragColor = vec4(red, green, blue, 1.0);
	//gl_FragColor = mix(gl_FragColor, vec4(param.b), 0.5);
	gl_FragColor = mix(gl_FragColor, vec4(223.0 / 255.0, 88.0 / 255.0, 47.0 / 255.0, 1.0), param.b * param.b);
}
