#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif
uniform   mat4 u_mvp;

attribute vec4 a_pos;
attribute vec3 a_normal;
attribute vec2 a_uv0;

varying vec2 v_texcoord;
varying vec2 v_mapcoord0;
varying vec2 v_mapcoord1;
varying float v_phase0;
varying float v_phase1;
varying float v_noise;
varying vec3 v_texweight;

uniform float u_time;

float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

void main()
{
	gl_Position = u_mvp * a_pos;
	vec2 tmpCoord = vec2(a_pos.y + a_pos.z, a_pos.x - a_pos.z);
	v_mapcoord0 = tmpCoord * 0.025;
	v_mapcoord1 = tmpCoord * 0.025 + vec2(0.25, -0.25) * 0.5;
	v_texcoord = a_uv0;
	//v_noise = rand(vec2(int(v_mapcoord0.x), int(v_mapcoord0.y)));
	v_noise = rand(a_pos.xy);
	v_phase0 = fract(u_time);
	v_phase1 = fract(u_time + 0.5);

	float temp = v_noise * 3.0;
	if (temp < 1.0) {
		v_texweight.x = temp;
		v_texweight.y = 1.0 - v_texweight.x;
		v_texweight.z = 0.0;
	}
	else if (temp < 2.0) {
		v_texweight.x = 0.0;
		v_texweight.y = temp - 1.0;
		v_texweight.z = 1.0 - v_texweight.y;
	}
	else {
		v_texweight.x = temp - 2.0;
		v_texweight.y = 0.0;
		v_texweight.z = 1.0 - v_texweight.x;
	}
}