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

uniform highp float u_time;


void main()
{
    gl_Position = u_mvp * a_pos;
	v_normal = a_normal;
	v_texcoordFoam = vec2(a_uv0.x, a_uv0.y - u_time * 0.03 * 0.3) * 0.6 * 4.0 + (vec2(cos(a_uv0.x * 51.0 + u_time * 0.3), sin((a_uv0.x + a_uv0.y) * 42.0 + u_time * 0.25)) * 0.02);
}
