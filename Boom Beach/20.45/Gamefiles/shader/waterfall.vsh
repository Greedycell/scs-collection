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

varying vec3 v_normal;
varying vec2 v_texcoordFoam;
varying vec2 v_normalUv;
varying float v_blendWeight;

uniform highp float u_time;
uniform mediump float u_1DivLevelWidth;
uniform mediump float u_1DivLevelHeight;


void main()
{
    gl_Position = u_mvp * a_pos;
	v_normal = a_normal;
	vec2 v_texcoordMap = vec2(a_pos.x * u_1DivLevelWidth * 0.1 + 0.5, a_pos.y * u_1DivLevelHeight * -0.1 + 0.5);
	v_texcoordFoam = vec2(a_uv0.x, a_uv0.y - u_time * 0.03 * 0.3) * 0.6 * 4.0 + (vec2(cos(a_uv0.x * 51.0 + u_time * 0.3), sin((a_uv0.x + a_uv0.y) * 42.0 + u_time * 0.25)) * 0.02);
	v_normalUv = v_texcoordMap * 30.0;
	v_blendWeight = (sin(length(v_texcoordFoam + v_normalUv) * 50.0) + 1.0) * 0.5;
	v_normalUv += sin(u_time + v_normalUv.x * 4.0) * 0.01;
}
