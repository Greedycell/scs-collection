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

varying   vec2 v_texcoord0;
varying   vec2 v_foamUv;

uniform float u_time;
uniform vec2 u_shadowPos;

void main()
{
	gl_Position = u_mvp * a_pos;
	v_texcoord0 = a_uv0;
	v_foamUv = (v_texcoord0 * 4.0) + vec2(-0.1, 0.05) * u_time * 0.5;
	v_foamUv += u_shadowPos * 0.01;
}
