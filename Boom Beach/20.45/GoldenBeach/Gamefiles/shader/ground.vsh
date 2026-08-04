#ifdef GL_ES
#else
#define highp
#define mediump
#define lowp
#endif
uniform   mat4 u_mvp;

attribute vec4 a_pos;
attribute vec3 a_color;
attribute vec2 a_uv0;
attribute vec2 a_uv1;

varying   vec3 v_color;
varying   vec2 v_texcoord0;
varying   vec2 v_texcoord1;
varying   vec2 v_texcoordMap;

uniform mediump float u_1DivLevelWidth;
uniform mediump float u_1DivLevelHeight;

void main()
{
	gl_Position = u_mvp * a_pos;
	v_color = a_color;
	v_texcoord0 = a_uv0;
	v_texcoord1 = a_uv1;
	v_texcoordMap = vec2(a_pos.x * u_1DivLevelWidth, a_pos.y * u_1DivLevelHeight);
}
